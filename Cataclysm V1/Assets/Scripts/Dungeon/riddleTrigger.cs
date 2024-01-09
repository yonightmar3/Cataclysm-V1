using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class riddleTrigger : MonoBehaviour
{
    public Animator riddle;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            riddle.SetTrigger("riddleTrigger");
        }
    }
}
