using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class eventManager : MonoBehaviour
{
    //SCRIPT REFERENCES

    public PlayerLook playerLookScript;

    //RUNE PUZZLE ROOM
    public Transform librarySpawn;
    public GameObject libraryWall;
    private bool rune1 = false;
    private bool rune2 = false;
    private bool rune3 = false;
    private bool rune4 = false;
    public AudioSource runePressedSound;

    //ANIMATIONS
    public Animator rune1Animation, rune3Animation, rune4Animation;
    public Animator rune2Animation;
    private int runeCounter = 0;


    [SerializeField] private Animator descendingDoor;
    public GameObject puzzle1Door;

    private bool isBookOpen = false;
    private bool bookSeen = false;


    //MAZE
    public GameObject player;
    public GameObject pillar1, pillar2, collapse2, collapse3;
    public GameObject pickUpText;
    private GameObject lookingAtFar;
    private GameObject lookingAtClose;

    public GameObject collapse2Sound;
    public AudioSource collapse3Sound;

    //BOOKS
    public GameObject entropyBook;




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

    public GameObject jailTrigger3;
    

    //Jail Cell
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
            RaycastHit hitInfoFar = playerLookScript.HitInfoFar;
            RaycastHit hitInfoClose = playerLookScript.HitInfoClose;


            // Now you can use hitInfo in this script
            if (hitInfoFar.collider != null)
            {
                lookingAtFar = hitInfoFar.transform.gameObject;
                // Do something with hitInfo
                //Debug.Log("Hit object: " + hitInfo.collider.gameObject.name);
                if (hitInfoFar.transform.gameObject.name == "jailTrigger3")
                {
                    jailCultist.SetActive(true);
                }
                else pickUpText.SetActive(false);
            }
            if (hitInfoClose.collider != null)
            {
                lookingAtClose = hitInfoClose.transform.gameObject;

                //PUZZLE 1
                if (hitInfoClose.transform.gameObject.name == "libraryKey")
                {
                    pickUpText.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        StartCoroutine("teleportToLibrary");

                    }
                }
                else if (hitInfoClose.transform.gameObject.name == "Rune 1 Trigger")
                {
                    pickUpText.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        runeCounter++;
                        rune1 = true;
                        rune1Animation.SetTrigger("rune1Down");
                        Debug.Log("Rune 1");
                    }
                }
                else if (hitInfoClose.transform.gameObject.name == "Rune 2 Trigger")
                {
                    pickUpText.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        runeCounter++;

                        rune2Animation.SetTrigger("rune1Down");
                        //rune1Animation.SetTrigger("rune1Up");
                        if (rune1 == true)
                        {
                            rune2 = true;
                            rune2Animation.SetTrigger("rune2Down");
                            rune1Animation.SetTrigger("rune1Up");

                            Debug.Log("Rune 2");
                        }
                        else
                        {
                            rune1 = false;
                            Debug.Log("Rune 2 WRONG");
                        }
                    }
                }
                else if (hitInfoClose.transform.gameObject.name == "Rune 3 Trigger")
                {

                    pickUpText.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        runeCounter++;

                        rune3Animation.SetTrigger("rune1Down");
                        if (rune1 == true && rune2 == true)
                        {
                            Debug.Log("Rune 3");
                            rune3 = true;
                        }
                        else
                        {
                            Debug.Log("Rune 3 WRONG");
                            rune1 = false;
                            rune2 = false;
                        }
                    }
                }
                else if (hitInfoClose.transform.gameObject.name == "Rune 4 Trigger")
                {

                    pickUpText.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        runeCounter++;

                        rune4Animation.SetTrigger("rune1Down");
                        if (rune1 == true && rune2 == true && rune3 == true)
                        {
                            Debug.Log("Rune 4");
                            rune4 = true;
                        }
                        else
                        {
                            Debug.Log("Rune 4 WRONG");
                            rune1 = false;
                            rune2 = false;
                            rune3 = false;
                        }
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

                else if (hitInfoClose.transform.gameObject.tag == "DoorPortal")
                {
                    GameObject door = hitInfoClose.transform.gameObject;
                    Animator doorAnim = door.GetComponent<Animator>();
                    pickUpText.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        if (doorAnim.GetCurrentAnimatorStateInfo(0).IsName("doorOpen"))
                        {
                            //close
                            doorAnim.ResetTrigger("doorOpen");
                            doorAnim.SetTrigger("doorClose");
                        }
                        if (doorAnim.GetCurrentAnimatorStateInfo(0).IsName("doorClose"))
                        {
                            //open
                            doorAnim.ResetTrigger("doorClose");
                            doorAnim.SetTrigger("doorOpen");
                        }
                    }
                }
                //else if (hitInfoClose.transform.gameObject.name == "Entropy Book")
                //{
                /*pickUpText.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("book");

                    entropyBook.SetActive(true);
                    pickUpText.SetActive(false);
                    isBookOpen = true;
                }
            }
        }*/

                else pickUpText.SetActive(false);
                /*//bookSeen = false;

                if (isBookOpen == true)
                {
                    if (Input.GetKeyDown(KeyCode.Escape))
                    {

                        // Close the book
                        entropyBook.SetActive(false);
                        pickUpText.SetActive(true);
                        isBookOpen = false;
                        bookSeen = false;


                        // Toggle the book state
                        //isBookOpen = !isBookOpen;
                    }
                }*/



                /*                if (Input.GetKeyDown(KeyCode.F))
                                {
                                    Cursor.visible = false;
                                    entropyBook.SetActive(false);
                                }*/

                if (rune4 == false && runeCounter == 4)
                {
                    rune1Animation.SetTrigger("rune1Up");
                    rune2Animation.SetTrigger("rune1Up");
                    rune3Animation.SetTrigger("rune1Up");
                    rune4Animation.SetTrigger("rune1Up");
                    runeCounter = 0;
                }
                if (rune4 == true)
                {
                    Debug.Log("works");
                    descendingDoor.SetTrigger("puzzle1wallDescend");
                }

                //else Debug.Log(":(");
            }

        }
    }

    public void playRuneSound()
    {
        runePressedSound.Play();
    }
    IEnumerator teleportToLibrary()
    {
        InputManager.disabled = true;
        yield return new WaitForSeconds(0.1f);
        lookingAtClose.SetActive(false);
        pickUpText.SetActive(false);
        libraryWall.SetActive(false);
        player.transform.position = new Vector3(librarySpawn.transform.position.x, player.transform.position.y, librarySpawn.transform.position.z);
        Debug.Log("player teleported");
        yield return new WaitForSeconds(0.1f);
        InputManager.disabled = false;
    }


}
