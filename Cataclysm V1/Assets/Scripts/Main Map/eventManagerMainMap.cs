using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class eventManagerMainMap : MonoBehaviour
{
    //SCRIPT REFERENCES

    

    public PlayerLook playerLookScript;

    public GameObject pickUpText;

    private GameObject lookingAtFar;
    private GameObject lookingAtClose;

    public GameObject wizardBook;
    public GameObject readingBackground;
    private bool isBookOpen;

    public GameObject Gabriel;


    private void Update()
    {
        if (playerLookScript != null)
        {
            // Access the HitInfo property from the PlayerLook script
            RaycastHit hitInfoClose = playerLookScript.HitInfoClose;


            // Now you can use hitInfo in this script

            if (hitInfoClose.collider != null)
            {
                lookingAtClose = hitInfoClose.transform.gameObject;

                //PUZZLE 1
                if (hitInfoClose.transform.gameObject.name == "Dungeon Door")
                {
                    pickUpText.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        SceneManager.LoadScene("Dungeon");

                    }
                }
                else if (hitInfoClose.transform.gameObject.tag == "Door")
                {
                    GameObject door = hitInfoClose.transform.gameObject;
                    Animator doorAnim = door.GetComponent<Animator>();
                    pickUpText.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        if (doorAnim.GetCurrentAnimatorStateInfo(0).IsName("doorOpen"))
                        {
                            //close
                            /*doorAnim.ResetTrigger("doorOpen");
                            doorAnim.SetTrigger("doorClose");*/
                        }
                        if (doorAnim.GetCurrentAnimatorStateInfo(0).IsName("doorClose"))
                        {
                            //open
                            doorAnim.ResetTrigger("doorClose");
                            doorAnim.SetTrigger("doorOpen");
                            door.GetComponent<BoxCollider>().isTrigger = true;
                        }
                    }
                }
                else if (hitInfoClose.transform.gameObject.name == "Wizard Book")
                {
                    pickUpText.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {

                        isBookOpen = !isBookOpen;

                        if (isBookOpen)
                        {
                            //open book
                            InputManager.disabled = true;
                            readingBackground.SetActive(true);
                            wizardBook.SetActive(true);
                            pickUpText.SetActive(false);
                            Gabriel.SetActive(true);
                        }
                        else
                        {
                            //close book
                            InputManager.disabled = false;
                            readingBackground.SetActive(false);
                            wizardBook.SetActive(false);
                            pickUpText.SetActive(true);
                        }
                    }
                }
                else pickUpText.SetActive(false);
            }
            else pickUpText.SetActive(false);
        }
    }
               
}
