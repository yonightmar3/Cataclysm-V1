using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorSlam : MonoBehaviour
{
    [SerializeField] private Animator doubleDoorLeft;
    [SerializeField] private Animator doubleDoorRight;

    GameObject jailDoubleDoorRight;
    GameObject jailDoubleDoorLeft;

    private void Start()
    {
        jailDoubleDoorRight = GameObject.Find("jailDoubleDoorRight");
        jailDoubleDoorLeft = GameObject.Find("jailDoubleDoorLeft");


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            doubleDoorRight.SetTrigger("jailDoubleDoorRightSlam");
            doubleDoorLeft.SetTrigger("jailDoubleDoorLeftSlam");
        }
    }
}
