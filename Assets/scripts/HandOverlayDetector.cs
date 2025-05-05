using Mediapipe.Unity.Sample.HandLandmarkDetection;
using UnityEngine;

public class HandOverlayDetector : MonoBehaviour
{
    public LayerMask detectionLayer;
    public float rayLength = 0.1f;

    [Tooltip("Fixed ray direction in world space (default: (0, 0, 1))")]
    public Vector3 rayDirection = new Vector3(0f, 0f, 1f);

    [Tooltip("Reference to HandLandmarkerRunner")]
    public HandLandmarkerRunner gestureDetectorScript;  // Drag the HandLandmarkerRunner script here

    private Transform middleMCP, ringMCP;

    void Update()
    {
        if (middleMCP == null || ringMCP == null)
            FindHandLandmarks();

        // Check gesture and draw rays only if "Closed Hand" gesture is detected
        if (middleMCP && ringMCP && IsHandGesture("Closed Hand"))
        {
            CheckRayHit(middleMCP, "Middle MCP");
            CheckRayHit(ringMCP, "Ring MCP");
        }
    }

    void FindHandLandmarks()
    {
        var handList = GameObject.Find("Multi HandLandmarkList Annotation");
        if (handList == null) return;

        var pointLists = handList.GetComponentsInChildren<Transform>(true);
        foreach (var child in pointLists)
        {
            if (child.name.StartsWith("Point List Annotation") && child.childCount >= 14)
            {
                middleMCP = child.GetChild(9);  // Middle MCP
                ringMCP = child.GetChild(13);   // Ring MCP
                break;
            }
        }
    }

    void CheckRayHit(Transform joint, string jointName)
    {
        Vector3 origin = joint.position;
        Vector3 dir = rayDirection.normalized;

        // Check if the ray hits the "Nave" object
        if (Physics.Raycast(origin, dir, out RaycastHit hit, rayLength, detectionLayer))
        {
            if (hit.collider.CompareTag("Nave"))
            {
                // If hand is closed and over a Nave object, log a warning
                if (IsHandGesture("Closed Hand"))
                {
                    Debug.LogWarning($"{jointName} is over a 'Nave' object while the hand is closed: {hit.collider.name}");
                }
            }
        }
    }

    bool IsHandGesture(string targetGesture)
    {
        if (gestureDetectorScript == null) return false;

        // Check if the gesture is detected
        return gestureDetectorScript.CurrentGestures.Contains(targetGesture);
    }
}
