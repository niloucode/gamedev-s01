using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class MovementController : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private GameObject standingModel;
    [SerializeField] private GameObject crouchingModel;
    [SerializeField] private CapsuleCollider playerCollider;
    
    [SerializeField] private float moveSpeed = 5f;

    [SerializeField] private float sprintSpeedBoost = 2f;
    [SerializeField] private float powerupSpeedBoost = 2f;

    [SerializeField] private float rotationSpeed = 15f; // Added for smooth turning

    [SerializeField] private float normalFOV = 60f;
    [SerializeField] private float sprintFOVBoost = 15f;
    [SerializeField] private float powerupFOVBoost = 15f;

    private bool isSprinting;
    private bool isCrouching;
    private Vector2 moveInput;
    private Vector3 respawnPoint;
    private Rigidbody rb;

    private void Awake()
    {
        if (cameraTransform == null && Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        respawnPoint = transform.position;
    }

    public void SpeedBoost()
    {
        moveSpeed += powerupSpeedBoost;
        normalFOV += powerupFOVBoost;
        StartCoroutine(RemoveSpeedBoost());
    }

    private IEnumerator RemoveSpeedBoost()
    {
        yield return new WaitForSeconds(2f);
        moveSpeed -= powerupSpeedBoost;
        normalFOV -= powerupFOVBoost;
    }

    public void SetRespawnPoint(Vector3 position)
    {
        respawnPoint = position;
    }

    public void ResetPosition()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.position = respawnPoint;
    }

    private void Update()
    {
        isCrouching = Keyboard.current.leftCtrlKey.isPressed;
        if (!isCrouching)
        {
            isSprinting = Keyboard.current.leftShiftKey.isPressed;
        }

        standingModel.SetActive(!isCrouching);
        crouchingModel.SetActive(isCrouching);

        if (isCrouching)
        {
            playerCollider.height = 3f;
            playerCollider.center = new Vector3(0f, -0.3f, 0f);
        }
        else
        {
            playerCollider.height = 4.5f;
            playerCollider.center = Vector3.zero;
        }
    }
    
    void FixedUpdate()
    {

        if (cameraTransform == null) return;

        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        camForward.y = 0f;
        camRight.y = 0f;

        camForward.Normalize();
        camRight.Normalize();

        float speed = isSprinting && !isCrouching ? moveSpeed+sprintSpeedBoost : moveSpeed;

        playerCamera.fieldOfView = isSprinting ? normalFOV+sprintFOVBoost : normalFOV;

        Vector3 movement = (moveInput.x * camRight) + (moveInput.y * camForward);

        rb.linearVelocity = new Vector3(
            movement.x * speed,
            rb.linearVelocity.y,
            movement.z * speed
        );

        if (moveInput.sqrMagnitude > 0.001f)
        {
            playerManager.PlayerMovedAction();
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(movement),
                rotationSpeed * Time.fixedDeltaTime
            );
        }
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed && Mathf.Abs(rb.linearVelocity.y) < 0.001f)
        {
            rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
        }
    }
}