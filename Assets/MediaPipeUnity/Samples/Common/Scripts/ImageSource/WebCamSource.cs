// Copyright (c) 2021 homuler
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#if UNITY_ANDROID
using UnityEngine.Android;
#endif

namespace Mediapipe.Unity
{
    public class WebCamSource : ImageSource
    {
        private const int _targetWidth = 320;
        private const int _targetHeight = 240;
        private const int _targetFrameRate = 24;
        
        private const string _TAG = nameof(WebCamSource);

        private readonly ResolutionStruct[] _defaultAvailableResolutions;

        public WebCamSource(int preferableDefaultWidth, ResolutionStruct[] defaultAvailableResolutions)
        {
            _defaultAvailableResolutions = defaultAvailableResolutions;
        }

        private static readonly object _PermissionLock = new object();
        private static bool _IsPermitted = false;

        private WebCamTexture _webCamTexture;
        private WebCamTexture webCamTexture
        {
            get => _webCamTexture;
            set
            {
                if (_webCamTexture != null)
                {
                    _webCamTexture.Stop();
                }
                _webCamTexture = value;
            }
        }

        public override int textureWidth => isPrepared ? webCamTexture.width : 0;
        public override int textureHeight => isPrepared ? webCamTexture.height : 0;

        public override bool isVerticallyFlipped => isPrepared && webCamTexture.videoVerticallyMirrored;
        public override bool isFrontFacing => isPrepared && (webCamDevice is WebCamDevice valueOfWebCamDevice) && valueOfWebCamDevice.isFrontFacing;
        public override RotationAngle rotation => !isPrepared ? RotationAngle.Rotation0 : (RotationAngle)webCamTexture.videoRotationAngle;

        private WebCamDevice? _webCamDevice;
        private WebCamDevice? webCamDevice
        {
            get => _webCamDevice;
            set
            {
                if (_webCamDevice is WebCamDevice valueOfWebCamDevice)
                {
                    if (value is WebCamDevice valueOfValue && valueOfValue.name == valueOfWebCamDevice.name)
                    {
                        // not changed
                        return;
                    }
                }
                else if (value == null)
                {
                    // not changed
                    return;
                }
                _webCamDevice = value;
                resolution = new ResolutionStruct(_targetWidth, _targetHeight, _targetFrameRate);
            }
        }
        public override string sourceName => (webCamDevice is WebCamDevice valueOfWebCamDevice) ? valueOfWebCamDevice.name : null;

        private WebCamDevice[] _availableSources;
        private WebCamDevice[] availableSources
        {
            get
            {
                if (_availableSources == null)
                {
                    _availableSources = WebCamTexture.devices;
                }

                return _availableSources;
            }
            set => _availableSources = value;
        }

        public override string[] sourceCandidateNames => availableSources?.Select(device => device.name).ToArray();

        public override ResolutionStruct[] availableResolutions
        {
            get
            {
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
                if (webCamDevice is WebCamDevice valueOfWebCamDevice) {
                    return valueOfWebCamDevice.availableResolutions.Select(resolution => new ResolutionStruct(resolution)).ToArray();
                }
#endif
                return new[] { new ResolutionStruct(_targetWidth, _targetHeight, _targetFrameRate) };
            }
        }

        public override bool isPrepared => webCamTexture != null;
        public override bool isPlaying => webCamTexture != null && webCamTexture.isPlaying;

        private IEnumerator Initialize()
        {
            yield return GetPermission();

            if (!_IsPermitted)
            {
                yield break;
            }

            if (webCamDevice != null)
            {
                yield break;
            }

            availableSources = WebCamTexture.devices;

            if (availableSources != null && availableSources.Length > 0)
            {
                webCamDevice = availableSources[0];
            }
        }

        private IEnumerator GetPermission()
        {
            lock (_PermissionLock)
            {
                if (_IsPermitted)
                {
                    yield break;
                }

#if UNITY_ANDROID
                if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
                {
                    Permission.RequestUserPermission(Permission.Camera);
                    yield return new WaitForSeconds(0.1f);
                }
#elif UNITY_IOS
                if (!Application.HasUserAuthorization(UserAuthorization.WebCam)) {
                    yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
                }
#endif

#if UNITY_ANDROID
                if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
                {
                    Debug.LogWarning("Not permitted to use Camera");
                    yield break;
                }
#elif UNITY_IOS
                if (!Application.HasUserAuthorization(UserAuthorization.WebCam)) {
                    Debug.LogWarning("Not permitted to use WebCam");
                    yield break;
                }
#endif
                _IsPermitted = true;

                yield return new WaitForEndOfFrame();
            }
        }

        public override void SelectSource(int sourceId)
        {
            if (sourceId < 0 || sourceId >= availableSources.Length)
            {
                throw new ArgumentException($"Invalid source ID: {sourceId}");
            }

            webCamDevice = availableSources[sourceId];
        }

        public override IEnumerator Play()
        {
            yield return Initialize();
            if (!_IsPermitted)
            {
                throw new InvalidOperationException("Not permitted to access cameras");
            }

            InitializeWebCamTexture();
            webCamTexture.Play();
            yield return WaitForWebCamTexture();
        }

        public override IEnumerator Resume()
        {
            if (!isPrepared)
            {
                throw new InvalidOperationException("WebCamTexture is not prepared yet");
            }
            if (!webCamTexture.isPlaying)
            {
                webCamTexture.Play();
            }
            yield return WaitForWebCamTexture();
        }

        public override void Pause()
        {
            if (isPlaying)
            {
                webCamTexture.Pause();
            }
        }

        public override void Stop()
        {
            if (webCamTexture != null)
            {
                webCamTexture.Stop();
            }
            webCamTexture = null;
        }

        public override Texture GetCurrentTexture() => webCamTexture;

        private void InitializeWebCamTexture()
        {
            Stop();
            if (webCamDevice is WebCamDevice valueOfWebCamDevice)
            {
                webCamTexture = new WebCamTexture(valueOfWebCamDevice.name, _targetWidth, _targetHeight, _targetFrameRate);
                return;
            }
            throw new InvalidOperationException("Cannot initialize WebCamTexture because WebCamDevice is not selected");
        }

        private IEnumerator WaitForWebCamTexture()
        {
            const int timeoutFrame = 2000;
            var count = 0;
            Debug.Log("Waiting for WebCamTexture to start");
            yield return new WaitUntil(() => count++ > timeoutFrame || webCamTexture.width > 16);

            if (webCamTexture.width <= 16)
            {
                throw new TimeoutException("Failed to start WebCam");
            }
        }
    }
}
