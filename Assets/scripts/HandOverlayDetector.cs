using UnityEngine;

public class HandLandmarkManager : MonoBehaviour
{
    // Retorna o Transform do landmark (ponto) OU null se a mão não existir
    public Transform TryGetHandLandmarkPoint(int pointIndex)
    {
        // 1. Verifica se a mão está na cena
        Transform hand = GameObject.Find("Canvas/Hand")?.transform;
        if (hand == null) return null; // Mão não detectada

        // 2. Navega até o Point List Annotation (com verificações)
        Transform annotationLayer = hand.Find("AnnotationLayer");
        Transform multiHandList = annotationLayer?.Find("Multi HandLandmarkList Annotation");
        Transform handClone = multiHandList?.Find("HandLandmarkList Annotation(Clone)");
        Transform pointList = handClone?.Find("Point List Annotation");

        if (pointList == null || pointIndex < 0 || pointIndex >= pointList.childCount)
        {
            Debug.LogWarning("Landmark não disponível. Verifique índices ou hierarquia.");
            return null;
        }

        return pointList.GetChild(pointIndex);
    }

    // Exemplo de uso (seguro para Update)
    void Update()
    {
        Transform landmark = TryGetHandLandmarkPoint(8); // Ponta do dedo indicador
        if (landmark != null)
        {
            // Faz algo com o landmark (ex: mover um objeto)
            Debug.Log($"Posição do landmark: {landmark.position}");
        }
    }
}