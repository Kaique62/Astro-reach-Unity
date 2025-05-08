using Mediapipe.Unity.Sample.HandLandmarkDetection;
using UnityEngine;
using System.Collections.Generic;

public class HandOverlayDetector : MonoBehaviour
{
    public LayerMask detectionLayer;
    public float screenDistanceThreshold = 100f;
    public float maxAllowedVelocity = 3000f;
    public HandLandmarkerRunner gestureDetector;

    public RectTransform canvasRectTransform;
    public Vector3 landmarkOffset = Vector3.zero;
    public Vector3 positionOffset = Vector3.zero;
    public float constantZDistance = 299f;

    private List<Transform> middleMCPs = new List<Transform>();
    private GameObject currentDraggedObject;
    private bool isDragging = false;

    private Vector3 lastScreenPosition;
    private float lastTime;

    void Update()
    {
        FindHandLandmarks();

        foreach (var middleMCP in middleMCPs)
        {
            Vector3 handScreenPos = Camera.main.WorldToScreenPoint(middleMCP.position);

            if (currentDraggedObject != null && isDragging)
            {
                if (IsHandMovingTooFast(middleMCP))
                {
                    isDragging = false;
                    currentDraggedObject = null;
                    return;
                }

                FollowHandPosition(middleMCP);
            }
            else
            {
                TryStartDrag(middleMCP, handScreenPos);
            }
        }
    }

    void FindHandLandmarks()
    {
        var handList = GameObject.Find("Multi HandLandmarkList Annotation");
        if (handList == null) return;

        var pointLists = handList.GetComponentsInChildren<Transform>(true);
        middleMCPs.Clear();

        foreach (var child in pointLists)
        {
            if (child.name.StartsWith("Point List Annotation") && child.childCount >= 10)
            {
                middleMCPs.Add(child.GetChild(9)); // ponto 9 = Middle MCP
            }
        }
    }

    void TryStartDrag(Transform middleMCP, Vector3 handScreenPos)
    {
        if (Camera.main == null || gestureDetector == null) return;

        GameObject nave = GameObject.FindGameObjectWithTag("Nave");
        if (nave != null)
        {
            Vector3 naveScreenPos = Camera.main.WorldToScreenPoint(nave.transform.position);
            float distance = Vector2.Distance(handScreenPos, naveScreenPos);

            bool isClosedHand = gestureDetector._textField.text.Contains("Closed Hand");

            if (distance < screenDistanceThreshold && isClosedHand)
            {
                currentDraggedObject = nave;
                isDragging = true;
                lastScreenPosition = handScreenPos;
                lastTime = Time.time;
            }
            else if (!isClosedHand)
            {
                currentDraggedObject = null;
                isDragging = false;
            }
        }
    }

    bool IsHandMovingTooFast(Transform middleMCP)
    {
        Vector3 currentScreenPos = Camera.main.WorldToScreenPoint(middleMCP.position);
        float currentTime = Time.time;
        float deltaTime = currentTime - lastTime;
        if (deltaTime <= 0) return false;

        float velocity = Vector3.Distance(currentScreenPos, lastScreenPosition) / deltaTime;
        lastScreenPosition = currentScreenPos;
        lastTime = currentTime;

        return velocity > maxAllowedVelocity;
    }

    void FollowHandPosition(Transform middleMCP)
    {
        Vector3 handScreenPos = Camera.main.WorldToScreenPoint(middleMCP.position + landmarkOffset);
        handScreenPos += positionOffset;

        Vector2 canvasSize = canvasRectTransform.rect.size;
        handScreenPos.x = Mathf.Clamp(handScreenPos.x, 0, canvasSize.x);
        handScreenPos.y = Mathf.Clamp(handScreenPos.y, 0, canvasSize.y);

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(handScreenPos.x, handScreenPos.y, constantZDistance));
        currentDraggedObject.transform.position = worldPos;
    }
}
