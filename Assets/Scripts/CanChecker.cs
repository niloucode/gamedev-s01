
using UnityEngine;

public class CanDetector : MonoBehaviour
{
    [SerializeField] private GameObject prefabToSpawn;
    [SerializeField] private Vector3 spawnPosition;
    [SerializeField] private Vector3 spawnRotation;

    private int cansInside = 0;
    private bool prefabSpawned = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Can"))
        {
            cansInside++;
            prefabSpawned = false;
            Debug.Log("HI");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Can"))
        {
            Debug.Log("BYE");
            cansInside = Mathf.Max(0, cansInside - 1);

            if (cansInside == 0 && !prefabSpawned)
            {
                Debug.Log("DAANG");
                GameObject spawned = Instantiate(
                    prefabToSpawn,
                    spawnPosition,
                    Quaternion.Euler(spawnRotation)
                );
                Debug.Log($"Spawned {spawned.name} at {spawnPosition}");

                prefabSpawned = true;
            }
        }
    }
}
