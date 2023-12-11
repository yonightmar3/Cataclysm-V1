using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eventManager : MonoBehaviour
{
    public PlayerLook playerLookScript;
    //PUZZLE ROOM 1
    private Vector3 librarySpawn = new Vector3(-14f, 1f, 70f);
    public GameObject libraryWall;
    private bool rune1 = false;

    //MAZE
    public GameObject player;
    public GameObject pillar1, pillar2, collapse2, collapse3;
    public GameObject pickUpText;
    private GameObject lookingAt;




    //public Camera cam;
    [SerializeField] private Animator secretDoor;

    //jail
    GameObject jailWall;
    public GameObject jailCultist;

    private void Start()
    {
        //jail room
        jailWall = GameObject.Find("jailWall");


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
        if (playerLookScript != null)
        {
            // Access the HitInfo property from the PlayerLook script
            RaycastHit hitInfo = playerLookScript.HitInfo;

            // Now you can use hitInfo in this script
            if (hitInfo.collider != null)
            {
                lookingAt = hitInfo.transform.gameObject;
                // Do something with hitInfo
                //Debug.Log("Hit object: " + hitInfo.collider.gameObject.name);
                if (hitInfo.transform.gameObject.name == "jailTrigger3")
                {
                    jailCultist.SetActive(true);
                }
                if (hitInfo.transform.gameObject.name == "mazeTrigger2")
                {
                    collapse2.SetActive(false);
                    collapse3.SetActive(false);
                    pillar1.SetActive(true);
                    pillar2.SetActive(true);
                }

                //PUZZLE 1
                if (hitInfo.transform.gameObject.name == "libraryKey")
                {
                    pickUpText.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        lookingAt.SetActive(false);
                        pickUpText.SetActive(false);
                        libraryWall.SetActive(false);
                        player.transform.position = librarySpawn;
                        Debug.Log("player teleported");

                    }
                }
                else if (hitInfo.transform.gameObject.name == "Pillar1")
                {
                    pickUpText.SetActive(true);
                    //if (Input.GetKeyDown(KeyCode.E))
                    //{
                        rune1 = true;
                        Debug.Log("Rune 1");
                    //}  
                }/*
                if (hitInfo.transform.gameObject.name == "Rune2")
                {
                    pickUpText.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        if (rune1 == true)
                        {
                            rune2 = true;
                        }
                        else
                        {
                            rune1 = false;
                        }
                    }
                }
                if (hitInfo.transform.gameObject.name == "Rune3")
                {
                    pickUpText.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        if (rune1 == true && rune2 == true)
                        {
                            rune3 = true;
                        }
                        else
                        {
                            rune1 = false;
                            rune2 = false;
                        }
                    }
                }
                if (hitInfo.transform.gameObject.name == "Rune4")
                {
                    pickUpText.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        if (rune1 == true && rune2 == true && rune3 == true)
                        {
                            rune4 = true;
                        }
                        else
                        {
                            rune1 = false;
                            rune2 = false;
                            rune3 = false;
                        }
                    }
                }*/

                else pickUpText.SetActive(false);
            }

            /*if (rune4 == true)
            {
                Debug.Log("works");
            }*/
            //else Debug.Log(":(");
        }

        //WHEN RAYCAST
        /*Vector3 cameraDirection = cam.transform.forward;
        Physics.Raycast(cam.transform.position, cameraDirection, out RaycastHit hitInfo, 20f);
        if (hitInfo.transform != null)
        {
            // Check if the hit object is the one you're looking for
            if (hitInfo.transform.gameObject.name == "jailTrigger3")
            {
                Debug.Log("walking cultist");
                jailCultist.SetActive(true);
            }
        }*/
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

    void puzzle1mechanics()
    {

    }


}
