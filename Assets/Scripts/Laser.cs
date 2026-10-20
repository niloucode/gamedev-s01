
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private StealthManager stealthManager;
    private PlayerManager playerManager;
    [SerializeField] public bool moving;

    private void Awake()
    {
        if (playerManager == null)
            playerManager = FindFirstObjectByType<PlayerManager>();

        if (stealthManager == null)
            stealthManager = FindFirstObjectByType<StealthManager>();
    }

    private void Update()
    {
        if (!moving)
            return;

        transform.position += -Vector3.forward * 20f * Time.deltaTime;

        if (transform.position.z < -18f)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (playerManager == null || !playerManager.CanReceiveDetection())
            return;

        PlayerStats stats = other.GetComponent<PlayerStats>();

        if (stats == null)
            return;

        // Start the round first if this is the first detection.
        if (stealthManager != null && stealthManager.isStealth)
            playerManager.Unstealth();

        // Unstealth may have changed the lifecycle state.
        if (!playerManager.gameRunning)
            return;

        stats.DecrementHP(1);
    }
}
