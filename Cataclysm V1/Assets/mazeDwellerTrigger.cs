using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mazeDwellerTrigger : MonoBehaviour
{
    public GameObject collapse1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            mazeDweller.playerSeen = true;
            collapse1.SetActive(false);
        }
    }
}
