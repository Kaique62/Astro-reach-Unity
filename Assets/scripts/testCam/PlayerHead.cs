using UnityEngine;

public class TesteGiro : MonoBehaviour
{
    private Gyroscope giro;

    void Start()
    {
        if (SystemInfo.supportsGyroscope)
        {
            giro = Input.gyro;
            giro.enabled = true;
            Debug.Log("Giroscópio ativado!");
        }
        else
        {
            Debug.Log("Este dispositivo não suporta giroscópio.");
        }
    }

    void Update()
    {
        // Rotaciona o objeto conforme o giroscópio (modo bruto pra teste)
        transform.rotation = new Quaternion(-giro.attitude.x, -giro.attitude.y, giro.attitude.z, giro.attitude.w);
    }
}
