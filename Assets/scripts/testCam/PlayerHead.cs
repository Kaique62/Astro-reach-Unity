using UnityEngine;

public class PlayerHead : MonoBehaviour
{
    public float gyroSensitivity = 50f;
    private Vector3 defaultPosition = new Vector3(0, 5, 0);

    void Start()
    {
        Input.gyro.enabled = true;
    }

    void Update()
    {
        // Mantém a posição fixa
        transform.position = defaultPosition;

        // Pega os dados do giroscópio
        Vector3 gyro = Input.gyro.rotationRateUnbiased;

        if (gyro != Vector3.zero)
        {
            // Aplica rotação com base no giroscópio
            float rotX = -gyro.x * Time.deltaTime * gyroSensitivity;
            float rotY = -gyro.y * Time.deltaTime * gyroSensitivity;

            transform.Rotate(new Vector3(rotX, rotY, 0), Space.Self);
        }
    }

    public void ResetRotation()
    {
        transform.rotation = Quaternion.identity;
    }

}
