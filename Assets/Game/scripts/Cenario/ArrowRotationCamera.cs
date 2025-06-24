using UnityEngine;

public class RotacaoControlada : MonoBehaviour
{
    public float velocidadeRotacao = 100f; // Velocidade da rotação (ajuste conforme necessário)

    void Update()
    {
        // Pega o input das teclas de seta
        float rotacaoHorizontal = Input.GetAxis("Horizontal"); // -1 para esquerda, 1 para direita
        float rotacaoVertical = Input.GetAxis("Vertical"); // -1 para baixo, 1 para cima

        // Aplica a rotação ao objeto
        transform.Rotate(Vector3.up, rotacaoHorizontal * velocidadeRotacao * Time.deltaTime); // rotação lateral (esquerda/direita)
        transform.Rotate(Vector3.left, rotacaoVertical * velocidadeRotacao * Time.deltaTime); // rotação vertical (cima/baixo)
    }
}
