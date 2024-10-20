using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;



public class eventManager : MonoBehaviour
{
    //SCRIPT REFERENCES

    public static bool keyObtained = false;
    [SerializeField] private GameObject starvedKey;
    private GameObject[] starvedLights;

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

    private string starvedAgentTag = "StarvedAgent";




    //public Camera cam;
    [SerializeField] private Animator secretDoor;

    //jail
    GameObject jailWall;
    public GameObject jailCultist;

    private void Start()
    {
        //jail room
        jailWall = GameObject.Find("jailWall");

        starvedLights = GameObject.FindGameObjectsWithTag("StarvedLight");


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
/*        GameObject[] starvedGameObjects = GameObject.FindGameObjectsWithTag(starvedAgentTag);
        NavMeshAgent closestStarved = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject starvedGameObject in starvedGameObjects)
        {
            // Get the NavMeshAgent component attached to the enemy game object
            NavMeshAgent enemy = starvedGameObject.GetComponent<NavMeshAgent>();

            // Ensure the enemy has a NavMeshAgent component
            if (enemy != null)
            {
                // Calculate the distance between the enemy and the player
                float distanceToPlayer = Vector3.Distance(enemy.transform.position, player.transform.position);

                // Check if this enemy is closer to the player than the current closest enemy
                if (distanceToPlayer < closestDistance)
                {
                    closestStarved = enemy;
                    closestDistance = distanceToPlayer;
                }
            }
        }

        closestStarved.gameObject.SetActive(false);*/


        if (playerLookScript != null)
        {
            if (respawnDungeon.respawned)
            {
                starvedKey.SetActive(true);
            }
            // Access the HitInfo property from the PlayerLook script
            RaycastHit hitInfoFar = playerLookScript.HitInfoFar;
            RaycastHit hitInfoClose = playerLookScript.HitInfoClose;
            if (keyObtained)
            {
                Debug.Log("lights out");

                foreach (GameObject light in starvedLights)
                {
                    //light.SetActive(false);
                    light.SetActive(false);
                }
            }
            else
            {
                foreach (GameObject light in starvedLights)
                {
                    light.GetComponent<Light>().enabled = true;

                }
            }


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
                else if (lookingAtClose.name == "Starved Key")
                {
                    pickUpText.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        lookingAtClose.gameObject.SetActive(false);
                        Debug.Log("Key Obtained");
                        keyObtained = true;

                    }
                }
                else if (hitInfoClose.transform.gameObject.name == "Rune 4 Trigger")
                {
                    pickUpText.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        runeCounter++;
                        rune4 = true;
                        rune4Animation.SetTrigger("rune1Down");
                        Debug.Log("Rune 4");
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
                        if (rune4 == true)
                        {
                            rune2 = true;
                            rune2Animation.SetTrigger("rune2Down");
                            rune1Animation.SetTrigger("rune1Up");

                            Debug.Log("Rune 2");
                        }
                        else
                        {
                            rune4 = false;
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
                        if (rune4 == true && rune2 == true)
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
                else if (hitInfoClose.transform.gameObject.name == "Rune 1 Trigger")
                {

                    pickUpText.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        runeCounter++;

                        rune1Animation.SetTrigger("rune1Down");
                        if (rune4 == true && rune2 == true && rune3 == true)
                        {
                            Debug.Log("Rune 1");
                            rune1 = true;
                        }
                        else
                        {
                            Debug.Log("Rune 1 WRONG");
                            rune4 = false;
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
                    GameObject parentDoor = door.transform.parent.gameObject;
                    BoxCollider backDoor = parentDoor.GetComponent<BoxCollider>();
                    Animator doorAnim = door.GetComponent<Animator>();
                    pickUpText.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        if (doorAnim.GetCurrentAnimatorStateInfo(0).IsName("doorOpen"))
                        {
                            //close
                            backDoor.isTrigger = false;

                            doorAnim.ResetTrigger("doorOpen");
                            doorAnim.SetTrigger("doorClose");
                        }
                        if (doorAnim.GetCurrentAnimatorStateInfo(0).IsName("doorClose"))
                        {
                            //open
                            backDoor.isTrigger = true;

                            doorAnim.ResetTrigger("doorClose");
                            doorAnim.SetTrigger("doorOpen");
                        }
                    }
                }

                else if (hitInfoClose.transform.gameObject.tag == "DoorLocked")
                {
                    GameObject door = hitInfoClose.transform.gameObject;
                    GameObject parentDoor = door.transform.parent.gameObject;
                    BoxCollider backDoor = parentDoor.GetComponent<BoxCollider>();
                    Animator doorAnim = door.GetComponent<Animator>();
                    pickUpText.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E) && keyObtained == true)
                    {
                        if (doorAnim.GetCurrentAnimatorStateInfo(0).IsName("doorOpen"))
                        {
                            //close
                            backDoor.isTrigger = false;

                            doorAnim.ResetTrigger("doorOpen");
                            doorAnim.SetTrigger("doorClose");
                        }
                        if (doorAnim.GetCurrentAnimatorStateInfo(0).IsName("doorClose"))
                        {
                            //open
                            backDoor.isTrigger = true;

                            doorAnim.ResetTrigger("doorClose");
                            doorAnim.SetTrigger("doorOpen");
                        }
                    }
                    else if (Input.GetKeyDown(KeyCode.E) && keyObtained != true)
                    {
                        Debug.Log("key missing");
                    }
                }

                else pickUpText.SetActive(false);

                if (rune1 == false && runeCounter == 4)
                {
                    rune1Animation.SetTrigger("rune1Up");
                    rune2Animation.SetTrigger("rune1Up");
                    rune3Animation.SetTrigger("rune1Up");
                    rune4Animation.SetTrigger("rune1Up");
                    runeCounter = 0;
                }
                if (rune1 == true)
                {
                    descendingDoor.SetTrigger("puzzle1wallDescend");
                }

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
