using System.Collections;
using System.Collections.Generic;
using Mediapipe.Tasks.Vision.HandLandmarker;
using UnityEngine;
using UnityEngine.Rendering;
using Mediapipe;
using TMPro;

namespace Mediapipe.Unity.Sample.HandLandmarkDetection
{
    public class HandLandmarkerRunner : VisionTaskApiRunner<HandLandmarker>
    {
        [SerializeField] private HandLandmarkerResultAnnotationController _handLandmarkerResultAnnotationController;

        private Experimental.TextureFramePool _textureFramePool;
        private string _lastStatus = "";
        private string _statusToDisplay = "";
        private ImageSource _imageSource;

        [SerializeField] public TMP_Text _textField;

        public readonly HandLandmarkDetectionConfig config = new HandLandmarkDetectionConfig();

        // Public property to expose the current detected gestures
        public string CurrentGesture { get; private set; } = "None";

        private void Update()
        {
            if (!string.IsNullOrEmpty(_statusToDisplay))
            {
                _textField.text = _statusToDisplay;
                _statusToDisplay = "";
            }
        }

        private string DetectGesture(IReadOnlyList<Mediapipe.NormalizedLandmark> landmarks)
        {
            if (landmarks == null) return "None";

            // Get relevant landmarks
            var thumbTip = landmarks[4];
            var indexTip = landmarks[8];
            var middleTip = landmarks[12];
            var ringTip = landmarks[16];
            var pinkyTip = landmarks[20];
            
            var thumbIp = landmarks[3];
            var indexPip = landmarks[6];
            var middlePip = landmarks[10];
            var ringPip = landmarks[14];
            var pinkyPip = landmarks[18];

            // Check finger states (true = extended, false = bent)
            bool thumbExtended = thumbTip.Y < thumbIp.Y;
            bool indexExtended = indexTip.Y < indexPip.Y;
            bool middleExtended = middleTip.Y < middlePip.Y;
            bool ringExtended = ringTip.Y < ringPip.Y;
            bool pinkyExtended = pinkyTip.Y < pinkyPip.Y;

            // Open Hand gesture (all fingers extended)
            if (indexExtended && middleExtended && ringExtended && pinkyExtended)
            {
                return "Open Hand";
            }
            // Closed Hand gesture (all fingers closed)
            else if (!indexExtended && !middleExtended && !ringExtended && !pinkyExtended)
            {
                return "Closed Hand";
            }
            // Pointer gesture (index extended, others closed)
            else if (indexExtended && !middleExtended && !ringExtended && !pinkyExtended)
            {
                return "Pointer";
            }

            return "None";
        }

        private List<Mediapipe.NormalizedLandmark> ConvertToProto(List<Mediapipe.Tasks.Components.Containers.NormalizedLandmark> landmarks)
        {
            var protoList = new List<Mediapipe.NormalizedLandmark>(landmarks.Count);
            foreach (var l in landmarks)
            {
                var proto = new Mediapipe.NormalizedLandmark
                {
                    X = l.x,
                    Y = l.y,
                    Z = l.z
                };

                if (l.visibility.HasValue)
                    proto.Visibility = l.visibility.Value;

                if (l.presence.HasValue)
                    proto.Presence = l.presence.Value;

                protoList.Add(proto);
            }
            return protoList;
        }

        private void UpdateGestureStatus(HandLandmarkerResult result)
        {
            string status = "No hands detected";
            CurrentGesture = "None";

            if (result.handLandmarks != null && result.handLandmarks.Count > 0)
            {
                var hand = result.handLandmarks[0];
                if (hand.landmarks.Count >= 21)
                {
                    var wrist = hand.landmarks[0];
                    var position = $"X: {wrist.x:0.##}, Y: {wrist.y:0.##}";

                    var protoLandmarks = ConvertToProto(hand.landmarks);
                    var gesture = DetectGesture(protoLandmarks);

                    CurrentGesture = gesture;
                    status = $"Hand Position: {position}\nGesture: {gesture}";
                }
            }

            _statusToDisplay = status; // <-- sempre atualiza o texto

            if (status != _lastStatus)
            {
                _lastStatus = status;
            }
        }

        protected override IEnumerator Run()
        {
            Debug.Log($"Initializing Hand Landmarker with {config.NumHands} hands");

            // Force single hand detection
            config.NumHands = 1;

            yield return AssetLoader.PrepareAssetAsync(config.ModelPath);

            var options = config.GetHandLandmarkerOptions(
                config.RunningMode == Tasks.Vision.Core.RunningMode.LIVE_STREAM ? OnHandLandmarkDetectionOutput : null
            );
            taskApi = HandLandmarker.CreateFromOptions(options, GpuManager.GpuResources);
            _imageSource = ImageSourceProvider.ImageSource;

            yield return _imageSource.Play();

            if (!_imageSource.isPrepared)
            {
                Debug.LogError("Failed to start ImageSource");
                yield break;
            }

            _textureFramePool = new Experimental.TextureFramePool(
                _imageSource.textureWidth,
                _imageSource.textureHeight,
                TextureFormat.RGBA32,
                10
            );
            screen.Initialize(_imageSource);

            var transformationOptions = _imageSource.GetTransformationOptions();
            var flipHorizontally = transformationOptions.flipHorizontally;
            var flipVertically = transformationOptions.flipVertically;
            var imageProcessingOptions = new Tasks.Vision.Core.ImageProcessingOptions(
                rotationDegrees: (int)transformationOptions.rotationAngle
            );

            var result = HandLandmarkerResult.Alloc(options.numHands);
            var canUseGpuImage = SystemInfo.graphicsDeviceType == GraphicsDeviceType.OpenGLES3 &&
                                 GpuManager.GpuResources != null;
            using var glContext = canUseGpuImage ? GpuManager.GetGlContext() : null;

            while (true)
            {
                if (isPaused)
                {
                    yield return new WaitWhile(() => isPaused);
                    _lastStatus = "";
                }

                if (!_textureFramePool.TryGetTextureFrame(out var textureFrame))
                {
                    yield return new WaitForEndOfFrame();
                    continue;
                }

                Image image;
                switch (config.ImageReadMode)
                {
                    case ImageReadMode.GPU:
                        textureFrame.ReadTextureOnGPU(_imageSource.GetCurrentTexture(), flipHorizontally, flipVertically);
                        image = textureFrame.BuildGPUImage(glContext);
                        yield return new WaitForEndOfFrame();
                        break;
                    case ImageReadMode.CPU:
                        yield return new WaitForEndOfFrame();
                        textureFrame.ReadTextureOnCPU(_imageSource.GetCurrentTexture(), flipHorizontally, flipVertically);
                        image = textureFrame.BuildCPUImage();
                        textureFrame.Release();
                        break;
                    default:
                        var req = textureFrame.ReadTextureAsync(_imageSource.GetCurrentTexture(), flipHorizontally, flipVertically);
                        yield return new WaitUntil(() => req.done);
                        image = textureFrame.BuildCPUImage();
                        textureFrame.Release();
                        break;
                }

                switch (taskApi.runningMode)
                {
                    case Tasks.Vision.Core.RunningMode.IMAGE:
                        if (taskApi.TryDetect(image, imageProcessingOptions, ref result))
                        {
                            UpdateGestureStatus(result);
                            _handLandmarkerResultAnnotationController.DrawNow(result);
                        }
                        else
                        {
                            UpdateGestureStatus(default);
                            _handLandmarkerResultAnnotationController.DrawNow(default);
                        }
                        break;
                    case Tasks.Vision.Core.RunningMode.VIDEO:
                        if (taskApi.TryDetectForVideo(image, GetCurrentTimestampMillisec(), imageProcessingOptions, ref result))
                        {
                            UpdateGestureStatus(result);
                            _handLandmarkerResultAnnotationController.DrawNow(result);
                        }
                        else
                        {
                            UpdateGestureStatus(default);
                            _handLandmarkerResultAnnotationController.DrawNow(default);
                        }
                        break;
                    case Tasks.Vision.Core.RunningMode.LIVE_STREAM:
                        taskApi.DetectAsync(image, GetCurrentTimestampMillisec(), imageProcessingOptions);
                        break;
                }
            }
        }

        private void OnHandLandmarkDetectionOutput(HandLandmarkerResult result, Image image, long timestamp)
        {
            UpdateGestureStatus(result);
            _handLandmarkerResultAnnotationController.DrawLater(result);
        }
    }
}