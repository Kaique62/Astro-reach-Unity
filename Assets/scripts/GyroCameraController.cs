using UnityEngine;

public class GiroscopioCamera : MonoBehaviour
{
    private Gyroscope giroscopio;
    private bool giroscopioAtivo;
    private Quaternion ajuste;
    private Quaternion offset = Quaternion.identity;

    void Start()
    {
        if (SystemInfo.supportsGyroscope)
        {
            giroscopio = Input.gyro;
            giroscopio.enabled = true;
            giroscopioAtivo = true;

            ajuste = Quaternion.Euler(90f, 0f, 0f);
            ResetarRotacao(); // Reseta ao iniciar, se quiser
        }
        else
        {
            Debug.Log("Giroscópio não disponível neste dispositivo.");
        }
    }

    void Update()
    {
        if (giroscopioAtivo)
        {
            Quaternion rotacaoGiroscopio = giroscopio.attitude;
            Quaternion rotacaoConvertida = new Quaternion(-rotacaoGiroscopio.x, -rotacaoGiroscopio.y, rotacaoGiroscopio.z, rotacaoGiroscopio.w);

            // Aplica a rotação relativa ao offset atual
            transform.localRotation = ajuste * offset * rotacaoConvertida;
        }
    }

    public void ResetarRotacao()
    {
        if (!giroscopioAtivo) return;

        Quaternion rotacaoAtual = new Quaternion(
            -giroscopio.attitude.x,
            -giroscopio.attitude.y,
            giroscopio.attitude.z,
            giroscopio.attitude.w
        );

        offset = Quaternion.Inverse(rotacaoAtual);
    }
}
