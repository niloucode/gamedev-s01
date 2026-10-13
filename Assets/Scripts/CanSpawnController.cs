using System.Collections;
using UnityEngine;

public class CanSpawnController : MonoBehaviour
{
    [SerializeField] private GameObject canPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float spawnInterval = 2f;

    private void Start()
    {
        StartCoroutine(SpawnCanCoroutine());
    }

    private IEnumerator SpawnCanCoroutine()
    {
        while (true)
        {
            SpawnCan();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnCan()
    {
        Instantiate(
            canPrefab,
            spawnPoint.position,
            spawnPoint.rotation
        );
    }
}