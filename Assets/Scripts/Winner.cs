using UnityEngine;

public class Winner : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager;
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("GG You Win!!");
        }
    }
}
