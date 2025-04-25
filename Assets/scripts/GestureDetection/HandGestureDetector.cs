using System.Collections.Generic;
using Mediapipe.Tasks.Components.Containers;

public static class HandGestureDetector
{
    public static bool IsThumbsUp(IReadOnlyList<NormalizedLandmark> landmarks)
    {
        if (landmarks.Count < 21) return false;

        // Thumb check
        var thumbTip = landmarks[4];
        var thumbIp = landmarks[3];
        bool thumbUp = thumbTip.y < thumbIp.y;

        // Index finger
        var indexTip = landmarks[8];
        var indexPip = landmarks[6];
        bool indexClosed = indexTip.y > indexPip.y;

        // Middle finger
        var middleTip = landmarks[12];
        var middlePip = landmarks[10];
        bool middleClosed = middleTip.y > middlePip.y;

        // Ring finger
        var ringTip = landmarks[16];
        var ringPip = landmarks[14];
        bool ringClosed = ringTip.y > ringPip.y;

        // Pinky finger
        var pinkyTip = landmarks[20];
        var pinkyPip = landmarks[18];
        bool pinkyClosed = pinkyTip.y > pinkyPip.y;

        return thumbUp && indexClosed && middleClosed && ringClosed && pinkyClosed;
    }

    public static bool IsVictory(IReadOnlyList<NormalizedLandmark> landmarks)
    {
        if (landmarks.Count < 21) return false;

        // Index and middle fingers up
        var indexTip = landmarks[8];
        var indexPip = landmarks[6];
        bool indexUp = indexTip.y < indexPip.y;

        var middleTip = landmarks[12];
        var middlePip = landmarks[10];
        bool middleUp = middleTip.y < middlePip.y;

        // Thumb, ring, pinky closed
        var thumbTip = landmarks[4];
        var thumbIp = landmarks[3];
        bool thumbClosed = thumbTip.y > thumbIp.y;

        var ringTip = landmarks[16];
        var ringPip = landmarks[14];
        bool ringClosed = ringTip.y > ringPip.y;

        var pinkyTip = landmarks[20];
        var pinkyPip = landmarks[18];
        bool pinkyClosed = pinkyTip.y > pinkyPip.y;

        return indexUp && middleUp && thumbClosed && ringClosed && pinkyClosed;
    }

    private enum Finger { Index, Middle, Ring, Pinky }


        #region New Gestures
        public static bool IsOpenHand(IReadOnlyList<NormalizedLandmark> landmarks)
        {
            if (landmarks.Count < 21) return false;
            
            return IsFingerExtended(landmarks, Finger.Index) &&
                IsFingerExtended(landmarks, Finger.Middle) &&
                IsFingerExtended(landmarks, Finger.Ring) &&
                IsFingerExtended(landmarks, Finger.Pinky) &&
                IsThumbExtended(landmarks);
        }

        public static bool IsClosedHand(IReadOnlyList<NormalizedLandmark> landmarks)
        {
            if (landmarks.Count < 21) return false;

            return !IsFingerExtended(landmarks, Finger.Index) &&
                !IsFingerExtended(landmarks, Finger.Middle) &&
                !IsFingerExtended(landmarks, Finger.Ring) &&
                !IsFingerExtended(landmarks, Finger.Pinky) &&
                IsThumbExtended(landmarks);
        }

        public static bool IsFist(IReadOnlyList<NormalizedLandmark> landmarks)
        {
            if (landmarks.Count < 21) return false;

            return !IsFingerExtended(landmarks, Finger.Index) &&
                !IsFingerExtended(landmarks, Finger.Middle) &&
                !IsFingerExtended(landmarks, Finger.Ring) &&
                !IsFingerExtended(landmarks, Finger.Pinky) &&
                !IsThumbExtended(landmarks);
        }
        #endregion

        #region Helper Methods
        private static bool IsFingerExtended(IReadOnlyList<NormalizedLandmark> landmarks, Finger finger)
        {
            int tipIndex, pipIndex;
            switch (finger)
            {
                case Finger.Index:
                    tipIndex = 8;
                    pipIndex = 6;
                    break;
                case Finger.Middle:
                    tipIndex = 12;
                    pipIndex = 10;
                    break;
                case Finger.Ring:
                    tipIndex = 16;
                    pipIndex = 14;
                    break;
                case Finger.Pinky:
                    tipIndex = 20;
                    pipIndex = 18;
                    break;
                default: return false;
            }
            return landmarks[tipIndex].y < landmarks[pipIndex].y;
        }

        private static bool IsThumbExtended(IReadOnlyList<NormalizedLandmark> landmarks)
        {
            return landmarks[4].y < landmarks[3].y; // Thumb tip vs IP joint
        }
        #endregion
    
}