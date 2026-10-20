
using UnityEngine;

public class StealthHazardGuard : MonoBehaviour
{
    public Transform player;
    public LayerMask obstructionMask;
    public LayerMask playerMask;

    public float viewRadius = 10f;
    [Range(0f, 360f)] public float viewAngle = 90f;
    public float closeRangeSphereRadius = 2.5f;

    public float sweepAngle = 30f;
    public float sweepSpeed = 2f;

    public bool randomizeRotationOffset = true;
    public float rotationTimeOffset = 0f;

    private float currentSweepTime = 0f;
    private Vector3 initialEuler;

    [SerializeField] private StealthManager stealthManager;
    private PlayerManager playerManager;

    private void Awake()
    {
        if (playerManager == null)
            playerManager = FindFirstObjectByType<PlayerManager>();
    }
    
    void Start()
    {
        initialEuler = transform.eulerAngles;

        if (randomizeRotationOffset)
        {
            currentSweepTime = Random.Range(0f, Mathf.PI * 2f);
        }
        else
        {
            currentSweepTime = rotationTimeOffset;
        }
    }

    void Update()
    {
        SweepLight();
        EvaluatePlayerDetection();
    }

    void SweepLight()
    {
        currentSweepTime += Time.deltaTime * sweepSpeed;
        float angleOffset = Mathf.Sin(currentSweepTime) * sweepAngle;

        transform.rotation = Quaternion.Euler(
            initialEuler.x + angleOffset,
            initialEuler.y,
            initialEuler.z
        );
    }

    void EvaluatePlayerDetection()
    {
        if (player == null)
            return;

        bool playerDetected = false;

        bool isInsideSphere = Physics.CheckSphere(
            transform.position,
            closeRangeSphereRadius,
            playerMask
        );

        if (isInsideSphere)
        {
            playerDetected = true;
        }
        else
        {
            Vector3 dirToPlayer = player.position - transform.position;
            float distanceToPlayer = dirToPlayer.magnitude;

            if (distanceToPlayer <= viewRadius)
            {
                float angleBetweenGuardAndPlayer = Vector3.Angle(
                    transform.forward,
                    dirToPlayer.normalized
                );

                if (angleBetweenGuardAndPlayer < viewAngle / 2f)
                {
                    if (!Physics.Raycast(
                        transform.position,
                        dirToPlayer.normalized,
                        distanceToPlayer,
                        obstructionMask))
                    {
                        playerDetected = true;
                    }
                }
            }
        }

        if (playerDetected)
        {
            TriggerAlertState();
        }
    }

    void TriggerAlertState()
    {
        if (stealthManager == null || playerManager == null)
            return;

        if (stealthManager.isStealth)
        {
            playerManager.Unstealth();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, viewRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, closeRangeSphereRadius);

        float halfAngle = viewAngle / 2f;
        Vector3 leftBoundary =
            Quaternion.Euler(0, -halfAngle, 0) * transform.forward;
        Vector3 rightBoundary =
            Quaternion.Euler(0, halfAngle, 0) * transform.forward;

        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(transform.position, leftBoundary * viewRadius);
        Gizmos.DrawRay(transform.position, rightBoundary * viewRadius);
    }
}
