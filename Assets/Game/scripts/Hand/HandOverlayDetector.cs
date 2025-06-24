using Mediapipe.Unity.Sample.HandLandmarkDetection;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;

public class HandOverlayDetector : MonoBehaviour
{
    public LayerMask detectionLayer;
    public float screenDistanceThreshold = 40f;
    public float maxAllowedVelocity = 30f;
    public HandLandmarkerRunner gestureDetector;

    public RectTransform canvasRectTransform;
    public Vector3 landmarkOffset = Vector3.zero;
    public Vector3 positionOffset = Vector3.zero;
    public float constantZDistance = 19f;

    public Image imagemCarregamentoBotao;
    public float tempoCarregamentoBotao = 3f;

    private List<Transform> middleMCPs = new List<Transform>();
    private GameObject currentDraggedObject;
    private bool isDragging = false;

    private Vector3 lastScreenPosition;
    private float lastTime;

    private GameObject currentHoveredButton;
    private Coroutine carregamentoBotaoCoroutine;
    private bool isButtonLoading = false;

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
                DetectAndHandleButtonInteraction(middleMCP, handScreenPos);
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

    // --- Nova lógica de detecção e interação com o botão de carregamento ---
    void DetectAndHandleButtonInteraction(Transform middleMCP, Vector3 handScreenPos)
    {
        // Usa um Raycast para detectar o botão na posição do Middle MCP
        Ray ray = Camera.main.ScreenPointToRay(handScreenPos);
        RaycastHit hit;

        // Assumimos que o Middle MCP é o ponto de interação principal.
        // O `detectionLayer` pode ser usado para filtrar apenas o que é interativo.
        if (Physics.Raycast(ray, out hit, 100f, detectionLayer)) // 100f é a distância máxima do raycast
        {
            if (hit.collider.CompareTag("Button"))
            {
                // Mão sobre o botão
                if (currentHoveredButton == null)
                {
                    // Começou a sobrepor o botão
                    currentHoveredButton = hit.collider.gameObject;
                    Debug.Log("Mão sobre o botão: " + currentHoveredButton.name);
                    StartButtonLoading();
                }
                // Se já estiver sobre o mesmo botão, a coroutine continua
            }
            else
            {
                // Mão sobre outro objeto ou saiu do botão
                StopButtonLoading();
                currentHoveredButton = null;
            }
        }
        else
        {
            // Mão não sobre nenhum objeto detectável
            StopButtonLoading();
            currentHoveredButton = null;
        }
    }

    void StartButtonLoading()
    {
        if (isButtonLoading) return; // Já está carregando
        
        isButtonLoading = true;
        imagemCarregamentoBotao.fillAmount = 0; // Garante que começa do zero
        carregamentoBotaoCoroutine = StartCoroutine(CarregarBotao());
    }

    void StopButtonLoading()
    {
        if (!isButtonLoading) return; // Não está carregando

        if (carregamentoBotaoCoroutine != null)
        {
            StopCoroutine(carregamentoBotaoCoroutine);
        }
        imagemCarregamentoBotao.fillAmount = 0; // Reseta o carregamento visual
        isButtonLoading = false;
    }

    IEnumerator CarregarBotao()
    {
        float tempoDecorrido = 0f;

        while (tempoDecorrido < tempoCarregamentoBotao && currentHoveredButton != null)
        {
            tempoDecorrido += Time.deltaTime;
            imagemCarregamentoBotao.fillAmount = tempoDecorrido / tempoCarregamentoBotao;
            yield return null; // Espera o próximo frame
        }

        // Verifica se o carregamento foi concluído e a mão ainda está sobre o botão
        if (currentHoveredButton != null && imagemCarregamentoBotao.fillAmount >= 0.99f) // Usar 0.99f para evitar problemas de float precision
        {
            ExecutarFuncaoDoBotao();
            imagemCarregamentoBotao.fillAmount = 0; // Reseta o carregamento após a execução
            isButtonLoading = false; // Sinaliza que o carregamento terminou
        }
        else // Carregamento interrompido (mão saiu)
        {
            imagemCarregamentoBotao.fillAmount = 0;
            isButtonLoading = false;
        }
    }

    void ExecutarFuncaoDoBotao()
    {
        Debug.Log("Função do botão de carregamento executada!");
        // --- COLOQUE A LÓGICA DA SUA FUNÇÃO AQUI ---
        // Exemplo: currentHoveredButton.GetComponent<Renderer>().material.color = Color.green;
        // Exemplo: SeuManagerDeCena.CarregarProximaFase();
    }
    // ---------------------------------------------------------------------------------------
}