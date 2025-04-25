using UnityEngine;
using System.Collections;
using System.Diagnostics;
using UnityEngine.UI;

public class CameraRotation : MonoBehaviour
{
    public float rotationSpeed = 50f;

    [Header("Configurações de Performance")]
    public bool mostrarPerformance = true;
    public int fontSize = 20;
    public Color textColor = Color.white;
    public Vector2 position = new Vector2(10, 10);

    private float fps;
    private float updateInterval = 0.5f;
    private float accum = 0f;
    private int frames = 0;
    private float timeleft;
    private string performanceText;
    private GUIStyle style = new GUIStyle();

    void Start()
    {
        timeleft = updateInterval;

        // Configura o estilo do texto
        style.fontSize = fontSize;
        style.normal.textColor = textColor;
        style.fontStyle = FontStyle.Bold;
    }

    void Update()
    {
        RotacionarCamera();
        CalcularPerformance();
    }

    void RotacionarCamera()
    {
        float horizontal = Input.GetAxis("Horizontal"); // Setas Esquerda/Direita
        float vertical = Input.GetAxis("Vertical");     // Setas Cima/Baixo

        // Rotação horizontal (em torno do eixo Y)
        transform.Rotate(Vector3.up, horizontal * rotationSpeed * Time.deltaTime, Space.World);

        // Rotação vertical (em torno do eixo X)
        transform.Rotate(Vector3.right, -vertical * rotationSpeed * Time.deltaTime, Space.Self);
    }

    void CalcularPerformance()
    {
        if (!mostrarPerformance) return;

        timeleft -= Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;
        frames++;

        // Atualiza a cada intervalo definido
        if (timeleft <= 0f)
        {
            fps = accum / frames;
            timeleft = updateInterval;
            accum = 0f;
            frames = 0;

            // Obtém uso de memória
            float ramUsage = Process.GetCurrentProcess().WorkingSet64 / (1024f * 1024f);

            // Formata o texto
            performanceText = string.Format("FPS: {0:0.}\nRAM: {1:0.} MB", fps, ramUsage);
        }
    }

    void OnGUI()
    {
        if (mostrarPerformance && !string.IsNullOrEmpty(performanceText))
        {
            GUI.Label(new Rect(position.x, position.y, 200, 50), performanceText, style);
        }
    }
}