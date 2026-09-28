using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Death : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            MovementController player = other.gameObject.GetComponent<MovementController>();
            player.Respawn();
        }
    }
}
