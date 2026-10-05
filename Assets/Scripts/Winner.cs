using UnityEngine;

public class Winner : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager;
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStats>().IncrementHP(999);
            Debug.Log("GG You Win!!");
            playerManager.Pause();
        }
    }
}
