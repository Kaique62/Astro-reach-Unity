using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]
public class NaveController : MonoBehaviour
{
    [Header("TextMeshPro UI Animation")]
    public Vector2 uiTextStartAnchoredPos = new Vector2(0, -100);
    public Vector2 uiTextEndAnchoredPos = new Vector2(0, 100);
    public float textMoveDuration = 1.5f;

    [Header("Victory Screen UI Animation")]
    public Vector2 imageStartAnchoredPos = new Vector2(0, -500);
    public Vector2 imageEndAnchoredPos = new Vector2(0, 0);
    public float imageMoveDuration = 2f;

    private TextMeshProUGUI avisoText;
    private RectTransform avisoRect;

    private Image victoryImage;
    private RectTransform victoryRect;

    private Image loseImage;
    private RectTransform loseRect;

    private Vector3 initialPosition;

    // Flags para controlar chamadas repetidas
    private bool canTriggerVictory = true;
    private bool canTriggerLose = true;

    private void Start()
    {
        Debug.Log("[NaveController] Script Initialized!");

        initialPosition = transform.position;

        // Buscar Feedback
        GameObject feedbackObj = GameObject.Find("PlayerHead/Player_UI_Canvas/Feedback");
        if (feedbackObj != null)
        {
            avisoText = feedbackObj.GetComponent<TextMeshProUGUI>();
            avisoRect = feedbackObj.GetComponent<RectTransform>();
            if (avisoText != null && avisoRect != null)
                avisoText.gameObject.SetActive(false);
        }

        // Buscar VictoryScreen
        GameObject victoryObj = GameObject.Find("PlayerHead/Player_UI_Canvas/VictoryScreen");
        if (victoryObj != null)
        {
            victoryImage = victoryObj.GetComponent<Image>();
            victoryRect = victoryObj.GetComponent<RectTransform>();
            if (victoryImage != null && victoryRect != null)
                victoryImage.gameObject.SetActive(false);
        }

        // Buscar LoseScreen (imagem de derrota)
        GameObject loseObj = GameObject.Find("PlayerHead/Player_UI_Canvas/LoseScreen");
        if (loseObj != null)
        {
            loseImage = loseObj.GetComponent<Image>();
            loseRect = loseObj.GetComponent<RectTransform>();
            if (loseImage != null && loseRect != null)
                loseImage.gameObject.SetActive(false);
        }

        // Collider
        Collider col = GetComponent<Collider>();
        if (col == null)
        {
            Debug.LogError("[NaveController] Nenhum Collider detectado!");
        }
        else if (!col.isTrigger)
        {
            Debug.LogWarning("[NaveController] O Collider não está como Trigger.");
        }

        // Rigidbody
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.isKinematic = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("[NaveController] OnTriggerEnter com: " + other.name);

        if (other.CompareTag("Planeta"))
        {
            if (canTriggerVictory)
            {
                canTriggerVictory = false;
                Debug.Log("[NaveController] Nave chegou ao planeta!");
                ShowVictoryScreen(); // Mostra a VictoryScreen
                StartCoroutine(ResetFlagsAfterDelay(10f));
            }
            else
            {
                Debug.Log("[NaveController] Vitória ignorada, já foi acionada recentemente.");
            }
        }
        else if (other.CompareTag("Asteroide"))
        {
            if (canTriggerLose)
            {
                canTriggerLose = false;
                Debug.Log("[NaveController] Colidiu com asteroide! Reposicionando...");
                transform.position = initialPosition;

                // Mostrar tela de derrota ao invés do feedbackText
                if (loseImage != null && loseRect != null)
                {
                    StartCoroutine(AnimateLoseScreen());
                }

                StartCoroutine(ResetFlagsAfterDelay(10f));
            }
            else
            {
                Debug.Log("[NaveController] Derrota ignorada, já foi acionada recentemente.");
            }
        }
    }

    /// <summary>
    /// Mostra texto animado, espera 1 segundo com texto, depois anima subir, então reinicia a fase chamando GerarCena().
    /// </summary>
    public void ShowUIAnimatedText(string texto)
    {
        if (avisoText != null && avisoRect != null)
        {
            StartCoroutine(ShowUIAnimatedTextCoroutine(texto));
        }
    }

    private IEnumerator ShowUIAnimatedTextCoroutine(string texto)
    {
        avisoText.text = texto;
        avisoText.gameObject.SetActive(true);
        avisoRect.anchoredPosition = uiTextStartAnchoredPos;

        // Espera 1 segundo com texto visível
        yield return new WaitForSeconds(1f);

        // Anima o texto subindo
        float elapsed = 0f;
        while (elapsed < textMoveDuration)
        {
            float t = elapsed / textMoveDuration;
            avisoRect.anchoredPosition = Vector2.Lerp(uiTextStartAnchoredPos, uiTextEndAnchoredPos, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        avisoRect.anchoredPosition = uiTextEndAnchoredPos;

        // Mantém texto por 1 segundo (opcional, pode ajustar)
        yield return new WaitForSeconds(1f);

        avisoText.gameObject.SetActive(false);

        // Chama GerarCena()
        CallGerarCena();
    }

    public void ShowVictoryScreen()
    {
        if (victoryImage != null && victoryRect != null)
        {
            StartCoroutine(AnimateVictoryScreen());
        }
    }

    private IEnumerator AnimateVictoryScreen()
    {
        ShowUIAnimatedText("Você Ganhou!");
        victoryImage.gameObject.SetActive(true);
        victoryRect.anchoredPosition = imageStartAnchoredPos;

        float elapsed = 0f;
        while (elapsed < imageMoveDuration)
        {
            float t = elapsed / imageMoveDuration;
            victoryRect.anchoredPosition = Vector2.Lerp(imageStartAnchoredPos, imageEndAnchoredPos, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        victoryRect.anchoredPosition = imageEndAnchoredPos;
    }

    private IEnumerator AnimateLoseScreen()
    {
        ShowUIAnimatedText("Você Perdeu!");
        loseImage.gameObject.SetActive(true);
        loseRect.anchoredPosition = imageStartAnchoredPos;

        float elapsed = 0f;
        while (elapsed < imageMoveDuration)
        {
            float t = elapsed / imageMoveDuration;
            loseRect.anchoredPosition = Vector2.Lerp(imageStartAnchoredPos, imageEndAnchoredPos, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        loseRect.anchoredPosition = imageEndAnchoredPos;
    }

    private void CallGerarCena()
    {
        GameObject proceduObj = GameObject.Find("Procedu");
        if (proceduObj == null)
        {
            Debug.LogError("[NaveController] Objeto 'Procedu' não encontrado!");
            return;
        }

        ProceduralSpawner spawner = proceduObj.GetComponent<ProceduralSpawner>();
        if (spawner == null)
        {
            Debug.LogError("[NaveController] Componente 'ProceduralSpawner' não encontrado no objeto 'Procedu'!");
            return;
        }

        spawner.GerarCena();
        Debug.Log("[NaveController] Método GerarCena() chamado com sucesso.");
    }

    private IEnumerator ResetFlagsAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        canTriggerVictory = true;
        canTriggerLose = true;
        Debug.Log("[NaveController] Flags de vitória e derrota resetadas.");
    }
}
