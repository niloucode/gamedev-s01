using UnityEngine;

public class BoostHealth : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStats>().IncrementHP(2);
            Debug.Log("Healing");
            Destroy(gameObject);
        }
    }
}
