using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyZone : MonoBehaviour
{
    public GameObject Enemy;
    public Transform Spawnpoint;

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Enemy.GetComponent<EnemyFollow>().isFollowing = false;
            Enemy.GetComponent<Rigidbody>().velocity = Vector3.zero;
            Enemy.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            Enemy.GetComponent<Rigidbody>().Sleep();
            Enemy.GetComponent<Rigidbody>().MovePosition(Spawnpoint.position);
            Enemy.transform.rotation = Spawnpoint.rotation;
        }
    }
}
