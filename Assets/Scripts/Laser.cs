using UnityEngine;

public class Laser : MonoBehaviour
{
    private void Update()
    {
        transform.position += -Vector3.forward * 20f * Time.deltaTime;

        if (transform.position.z < -18f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStats>().DecrementHP(1);
            Destroy(gameObject);
        }
    }
}
