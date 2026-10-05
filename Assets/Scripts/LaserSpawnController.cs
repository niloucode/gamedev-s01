using UnityEngine;
using System.Collections;

public class LaserSpawnController : MonoBehaviour
{
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float initialSpacing = 40f;

    private bool running = false;
    private Coroutine spawnCoroutine;

    public void StartLaserSpawn()
    {
        if (running) return;

        running = true;

        SpawnInitialLasers();
        spawnCoroutine = StartCoroutine(SpawnLasers());
    }

    public void StopLaserSpawn()
    {
        running = false;

        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }

        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    private void SpawnInitialLasers()
    {
        for (int i = -2; i <= 3; i++)
        {
            Vector3 spawnPosition =
                spawnPoint.position + Vector3.forward * (i * initialSpacing);

            Debug.Log(spawnPosition);
            SpawnLaser(spawnPosition);
        }
    }

    private IEnumerator SpawnLasers()
    {
        while (running)
        {
            float spawnDelay = Random.Range(1f, 2f);
            yield return new WaitForSeconds(spawnDelay);

            if (running){
                SpawnLaser(spawnPoint.position);
                SpawnLaser(spawnPoint.position);
                SpawnLaser(spawnPoint.position);
            }
        }
    }

    private void SpawnLaser(Vector3 spawnPosition)
    {
        float randomZ = Random.Range(0f, 360f);
        Quaternion rotation = Quaternion.Euler(0f, 0f, randomZ);

        float randomX = Random.Range(-5f, 5f);
        spawnPosition += Vector3.right * randomX;
        Instantiate(laserPrefab, spawnPosition, rotation,transform);
    }
}