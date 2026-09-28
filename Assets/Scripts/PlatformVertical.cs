using System.Collections.Generic;
using UnityEngine;

public class PlatformVertical : MonoBehaviour
{
    [SerializeField]
    GameObject obj;

    [SerializeField]
    float moveSpeed = 2f;

    float heightLimit;

    bool playerInside;

    void Start()
    {
        heightLimit = obj.transform.position.y+6;
    }

    void Update()
    {
        if (playerInside && obj.transform.position.y < heightLimit)
        {
            obj.transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
        }
    }
}