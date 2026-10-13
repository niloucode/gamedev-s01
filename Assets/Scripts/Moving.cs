using UnityEngine;

public class Moving : MonoBehaviour
{
    private float moveSpeed = -2f;

    private void OnCollisionStay(Collision collision)
    {
        collision.transform.Translate(0, 0, moveSpeed * Time.deltaTime);
    }

}
