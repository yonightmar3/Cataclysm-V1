using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eventManager : MonoBehaviour
{
    private Animator animator;
    public Camera cam;
    [SerializeField] private Animator secretDoor;

    //jail
    GameObject jailWall;
    GameObject jailDoor;
    public GameObject jailCultist;

    private void Start()
    {
        animator = GetComponent<Animator>();

        //jail room
        jailWall = GameObject.Find("jailWall");
        jailDoor = GameObject.Find("jailDoor");


    }
    //JAIL CELL EVENTS
    //public GameObject jailWall;
    //public GameObject jailDoor;
    public GameObject jailTrigger3;
    

    //DOOR CHANGER
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "jailTrigger2")
        {
            Debug.Log("0.5 pauza");
            jailTrigger3.SetActive(true);
            //animator.SetTrigger("doorSlam");
            secretDoor.SetTrigger("doorSlam");
            jailWall.SetActive(false);
            //jailDoor.SetActive(true);

        }
    }

    private void Update()
    {
        Vector3 cameraDirection = cam.transform.forward;
        Physics.Raycast(cam.transform.position, cameraDirection, out RaycastHit hitInfo, 20f);
        if (hitInfo.transform != null)
        {
            // Check if the hit object is the one you're looking for
            if (hitInfo.transform.gameObject.name == "jailTrigger3")
            {
                Debug.Log("walking cultist");
                jailCultist.SetActive(true);
            }
        }
            /* RaycastHit hitInfo = playerLook.HitInfo;

             // Check if the raycast hit something
             if (hitInfo.transform != null)
             {
                 // Check if the hit object is the one you're looking for
                 if (hitInfo.transform.gameObject.name == "jailTrigger3")
                 {
                     Debug.Log("pauzica");
                 }
             }*/
        }


}
