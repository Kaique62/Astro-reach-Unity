using UnityEngine;

[RequireComponent(typeof(Collider))]
public class NaveController : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("[NaveController] Script Initialized!");
        
        // Ensure this object has a collider
        Collider col = GetComponent<Collider>();
        if (col == null)
        {
            Debug.LogError("[NaveController] No Collider attached to this GameObject!");
        }
        else
        {
            Debug.Log("[NaveController] Collider detected: " + col.GetType().Name);
            if (!col.isTrigger)
                Debug.LogWarning("[NaveController] Collider is NOT set as Trigger. Trigger events won't be detected.");
        }

        // Check for Rigidbody
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogWarning("[NaveController] No Rigidbody found. Adding a kinematic Rigidbody for trigger detection.");
            rb = gameObject.AddComponent<Rigidbody>();
            rb.isKinematic = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("[NaveController] OnTriggerEnter detected with: " + other.name);

        if (other.gameObject.CompareTag("Planeta"))
        {
            Debug.Log("[NaveController] Nave Chegou ao Planeta!");
        }
        else if (other.gameObject.CompareTag("Asteroide"))
        {
            Debug.Log("[NaveController] Nave Colidiu com Asteroide!");
        }
        else
        {
            Debug.Log("[NaveController] Triggered with untagged or unknown object: " + other.tag);
        }
    }
}
