using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectPlayer : MonoBehaviour
{
    [SerializeField] private Animator secretDoor;
/*    [SerializeField] private Animator doubleDoorLeft;
    [SerializeField] private Animator doubleDoorRight;*/


    GameObject jailWall;
    public GameObject jailTrigger4;
    public GameObject jailTrigger3;
    GameObject jailDoubleDoorRight;
    GameObject jailDoubleDoorLeft;


    private void Start()
    {
        jailWall = GameObject.Find("jailWall");
        jailDoubleDoorRight = GameObject.Find("jailDoubleDoorRight");
        jailDoubleDoorLeft = GameObject.Find("jailDoubleDoorLeft");

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            secretDoor.SetTrigger("doorSlam");
            jailWall.SetActive(false);
            jailTrigger3.SetActive(true);
            jailTrigger4.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
/*            doubleDoorRight.SetTrigger("jailDoubleDoorRightSlam");
            doubleDoorLeft.SetTrigger("jailDoubleDoorLeftSlam");*/

        }
    }
}
