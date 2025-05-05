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
        public List<string> CurrentGestures { get; private set; } = new List<string>();

        private void Update()
        {
            if (!string.IsNullOrEmpty(_statusToDisplay))
            {
                _textField.text = _statusToDisplay;
                _statusToDisplay = "";
            }
        }

        private List<string> DetectGestures(IReadOnlyList<Mediapipe.NormalizedLandmark> landmarks)
        {
            var gestures = new List<string>();

            if (landmarks == null || landmarks.Count < 21) return gestures;

            var thumbTip = landmarks[4];
            var thumbIp = landmarks[3];
            bool thumbUp = thumbTip.Y < thumbIp.Y;

            bool indexUp = landmarks[8].Y < landmarks[6].Y;
            bool middleUp = landmarks[12].Y < landmarks[10].Y;
            bool ringUp = landmarks[16].Y < landmarks[14].Y;
            bool pinkyUp = landmarks[20].Y < landmarks[18].Y;

            if (thumbUp && !indexUp && !middleUp && !ringUp && !pinkyUp)
            {
                gestures.Add("Thumbs Up");
            }
            if (!thumbUp && indexUp && middleUp && !ringUp && !pinkyUp)
            {
                gestures.Add("Victory");
            }
            if (indexUp && middleUp && ringUp && pinkyUp)
            {
                gestures.Add("Open Hand");
            }
            if (!indexUp && !middleUp && !ringUp && !pinkyUp && thumbUp)
            {
                gestures.Add("Closed Hand");
            }
            if (!indexUp && !middleUp && !ringUp && !pinkyUp && !thumbUp)
            {
                gestures.Add("Fist");
            }

            return gestures;
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

            if (result.handLandmarks != null && result.handLandmarks.Count > 0)
            {
                foreach (var hand in result.handLandmarks)
                {
                    if (hand.landmarks.Count >= 21)
                    {
                        var wrist = hand.landmarks[0];
                        var position = $"X: {wrist.x:0.##}, Y: {wrist.y:0.##}";

                        var protoLandmarks = ConvertToProto(hand.landmarks);
                        var gestures = DetectGestures(protoLandmarks);

                        CurrentGestures = gestures;  // Store detected gestures in the public property
                        status = $"Hand Position: {position}\nGestures: {(gestures.Count > 0 ? string.Join(", ", gestures) : "None")}";
                        _statusToDisplay = status;
                        break;
                    }
                }
            }

            if (status != _lastStatus)
            {
                //Debug.Log(status);
                _lastStatus = status;
            }
        }

        protected override IEnumerator Run()
        {
            Debug.Log($"Initializing Hand Landmarker with {config.NumHands} hands");

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
