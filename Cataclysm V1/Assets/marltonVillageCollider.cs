using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class marltonVillageCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Marlton Entered");
            respawnMainMap.marlton = true;
        }
    }
}
