using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class repeatingHallway : MonoBehaviour
{
    private GameObject player;
    public Transform leftWingTeleportLocation;
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger");
        if (other.tag == "Player")
        {
            StartCoroutine("leftWingTeleport");
        }
    }

    IEnumerator leftWingTeleport()
    {
        InputManager.disabled = true;
        yield return new WaitForSeconds(0.01f);
        player.transform.position = leftWingTeleportLocation.position;
        Debug.Log("player teleported");
        yield return new WaitForSeconds(0.01f);
        InputManager.disabled = false;
    }
}
