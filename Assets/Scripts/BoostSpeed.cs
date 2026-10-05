using UnityEngine;

public class BoostSpeed : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<MovementController>().SpeedBoost();
            Debug.Log("Speeding");
            Destroy(gameObject);
        }
    }
}
