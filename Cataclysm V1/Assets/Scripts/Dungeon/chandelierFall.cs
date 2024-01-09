using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chandelierFall : MonoBehaviour
{
    public Animator chandelier;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            chandelier.SetTrigger("chandelierTrigger");
        }
    }
}
