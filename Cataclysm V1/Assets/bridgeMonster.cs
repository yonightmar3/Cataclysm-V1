using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bridgeMonster : MonoBehaviour
{
    [SerializeField] private GameObject seaMonster;


    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            seaMonster.SetActive(true);
        }
    }
}
