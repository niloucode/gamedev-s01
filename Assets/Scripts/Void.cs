using UnityEngine;

public class Void : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStats>().DecrementHP(999);
        }
    }
}
