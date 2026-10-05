using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] private float levitationSpeed = 3f;
    [SerializeField] private float levitationHeight = 0.5f;
    [SerializeField] private float rotationSpeed = 360f;

    private float startY;

    private void Start()
    {
        startY = transform.position.y;
    }

    private void Update()
    {
        float newY = startY + Mathf.Sin(Time.time * levitationSpeed) * levitationHeight;

        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }
}