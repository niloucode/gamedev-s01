
using UnityEngine;
using UnityEngine.InputSystem;

public class BallSpawnController : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Transform playerCharacter;
    [SerializeField] private float launchForce = 80f;
    [SerializeField] private float spawnDistance = 1.5f;

    void Update()
    {
        if (Mouse.current != null &&
            Mouse.current.leftButton.wasPressedThisFrame)
        {
            SpawnBall();
        }
    }

    private void SpawnBall()
    {
        Vector3 spawnPosition =
            playerCharacter.position +
            playerCharacter.forward * spawnDistance;

        GameObject ball = Instantiate(
            ballPrefab,
            spawnPosition,
            playerCharacter.rotation
        );

        Rigidbody rb = ball.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.AddForce(
                playerCharacter.forward * launchForce,
                ForceMode.Impulse
            );
        }
    }
}
