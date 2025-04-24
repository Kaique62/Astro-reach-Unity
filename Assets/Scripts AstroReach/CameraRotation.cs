using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public float rotationSpeed = 50f;

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal"); // Setas Esquerda/Direita
        float vertical = Input.GetAxis("Vertical");     // Setas Cima/Baixo

        // Rota��o horizontal (em torno do eixo Y)
        transform.Rotate(Vector3.up, horizontal * rotationSpeed * Time.deltaTime, Space.World);

        // Rota��o vertical (em torno do eixo X)
        transform.Rotate(Vector3.right, -vertical * rotationSpeed * Time.deltaTime, Space.Self);
    }
}
