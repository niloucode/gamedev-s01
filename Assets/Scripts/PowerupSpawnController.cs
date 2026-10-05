using UnityEngine;

public class PowerupSpawnController : MonoBehaviour
{
    [SerializeField] private BoxCollider spawnArea;
    [SerializeField] private GameObject speedBoosterPrefab;
    [SerializeField] private GameObject hpBoosterPrefab;

    private void Start()
    {
        SpawnPowerUps(speedBoosterPrefab, 5);
        SpawnPowerUps(hpBoosterPrefab, 5);
    }

    private void SpawnPowerUps(GameObject prefab, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(prefab, GetRandomPosition(), Quaternion.identity);
        }
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 center = spawnArea.center;
        Vector3 size = spawnArea.size;

        float randomX = Random.Range(-size.x / 2f, size.x / 2f);
        float randomZ = Random.Range(-size.z / 2f, size.z / 2f);

        Vector3 localPosition = center + new Vector3(randomX, 0f, randomZ);

        return spawnArea.transform.TransformPoint(localPosition);
    }
}