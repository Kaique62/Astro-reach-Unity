using UnityEngine;

public class GyroCameraController : MonoBehaviour
{
    private Gyroscope gyro;
    private bool gyroEnabled;
    private Quaternion rotationFix;

    void Start()
    {
        gyroEnabled = EnableGyro();
    }

    bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;

            // Corrige a rotação do giroscópio para o espaço da Unity
            rotationFix = new Quaternion(0, 0, 1, 0);
            return true;
        }
        return false;
    }

    void Update()
    {
        if (gyroEnabled)
        {
            // Aplica a rotação do giroscópio à câmera
            transform.localRotation = gyro.attitude * rotationFix;
        }
    }
}
