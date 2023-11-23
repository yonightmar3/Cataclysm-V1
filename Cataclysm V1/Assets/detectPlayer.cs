using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectPlayer : MonoBehaviour
{
    [SerializeField] private Animator secretDoor;
    GameObject jailWall;
    public GameObject jailTrigger3;

    private void Start()
    {
        jailWall = GameObject.Find("jailWall");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            secretDoor.SetTrigger("doorSlam");
            jailWall.SetActive(false);
            jailTrigger3.SetActive(true);
        }
    }
}
