using UnityEngine;

public class TransformController : MonoBehaviour
{
    [Header("Rotação")]
    public bool enableRotation = false;
    public Vector3 rotationAxis = new Vector3(0, 1, 0);
    public float rotationSpeed = 90f;

    [Header("Movimento Vertical (Subir e Descer)")]
    public bool enableVerticalMovement = false;
    public float movementAmplitude = 0.5f;
    public float movementSpeed = 1f;

    [Header("Escala")]
    public bool playScaleFadeIn = true;
    public float scaleFadeInDuration = 2f; // Tempo para escalar de 0 até o normal
    public bool loopScale = false;         // Se deve continuar oscilando após o fade
    public Vector3 loopScaleAmplitude = new Vector3(0.2f, 0.2f, 0.2f);
    public float loopScaleSpeed = 1f;

    private Vector3 startPosition;
    private Vector3 originalScale;
    private float timer;
    private float scaleFadeTimer = 0f;
    private bool fadeInDone = false;

    void Start()
    {
        startPosition = transform.position;
        originalScale = transform.localScale;
        if (playScaleFadeIn)
        {
            transform.localScale = Vector3.zero;
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (enableRotation)
        {
            transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);
        }

        if (enableVerticalMovement)
        {
            float offsetY = Mathf.Sin(timer * movementSpeed) * movementAmplitude;
            transform.position = new Vector3(startPosition.x, startPosition.y + offsetY, startPosition.z);
        }

        if (playScaleFadeIn && !fadeInDone)
        {
            scaleFadeTimer += Time.deltaTime;
            float t = Mathf.Clamp01(scaleFadeTimer / scaleFadeInDuration);
            float eased = Mathf.SmoothStep(0f, 1f, t); // Suaviza o crescimento
            transform.localScale = originalScale * eased;

            if (t >= 1f)
            {
                fadeInDone = true;
                scaleFadeTimer = 0f;
            }
        }
        else if (loopScale)
        {
            float scaleFactor = Mathf.Sin(timer * loopScaleSpeed);
            transform.localScale = originalScale + (loopScaleAmplitude * scaleFactor);
        }
    }
}
