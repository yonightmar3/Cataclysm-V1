using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prePortalTeleporter : MonoBehaviour
{
    public GameObject player;
    public GameObject otherPortal;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            otherPortal.SetActive(false);
        }
    }
}
