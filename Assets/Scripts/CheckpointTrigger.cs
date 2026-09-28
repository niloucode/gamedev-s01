using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Checkpoint Reached. Congrats!!");

            MovementController player = other.gameObject.GetComponent<MovementController>();
            player.SetRespawnPoint(other.transform.position);
        }
    }
}
