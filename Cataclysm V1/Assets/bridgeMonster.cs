using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bridgeMonster : MonoBehaviour
{
    [SerializeField] private Animator seaMonster;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            seaMonster.SetTrigger("bridge");
        }
    }
}
