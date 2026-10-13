using UnityEngine;

public class Despawner : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball") ||
            collision.gameObject.CompareTag("Can"))
        {
            Destroy(collision.gameObject);
        }
    }
}
