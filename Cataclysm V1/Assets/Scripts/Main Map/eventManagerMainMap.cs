using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class eventManagerMainMap : MonoBehaviour
{
    public static bool returnedFromDungeon;
    public GameObject bridge;
    public GameObject seaMonster;

    private GameObject player;
    //SCRIPT REFERENCES
    public dialogue dialogueScript;


    public PlayerLook playerLookScript;

    public GameObject pickUpText;

    private GameObject lookingAtFar;
    private GameObject lookingAtClose;

    public GameObject wizardBook;
    public GameObject readingBackground;
    private bool isBookOpen;

    public GameObject Gabriel;
    public GameObject GabrielHead;


    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueText;  // Reference to the TextMeshProUGUI component for displaying text
    private KeyCode nextDialogueKey = KeyCode.Space;  // Key to trigger the next dialogue
    private List<string> dialogues = new List<string>();  // List to store dialogues
    private int currentDialogueIndex = 0;  // Index of the current dialogue
    private bool dialogueInitiated;

    public Transform playerTransform;
    public float rotationSpeed = 5f;

    private bool gabrielSequenceReady;

    public GameObject exitDoor;


    private void Start()
    {
        player = GameObject.Find("Player");
        dialogues.Add("Tomorrow's tale lies fallow, and in the dance of frailty shadows find willing partners");
        dialogues.Add("Chasing shadows, a dance with echoes, where purpose wanes like fading stars");
        //dialogues.Add("Impossible, yet it has manifested. A fractured fate's fatal figure.");
        //dialogues.Add("In the dance of frailty, shadows find willing partners, and virtue's fabric frays.");
        dialogues.Add("Beyond reason, yet feasible. Inconceivable, yet tangible... It is essential");
    }

    private void Update()
    {
        if (returnedFromDungeon)
        {
            StartCoroutine(teleportFromDungeon());
            returnedFromDungeon = false; // Reset the flag after teleporting

        } 

        /*if (Gabriel != null && playerTransform != null)
        {
            // Create a target rotation
            Vector3 directionToTarget = GabrielHead.gameObject.transform.position - playerTransform.position;
            directionToTarget.y = 0f; // Keep only X and Z components
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

            // Smoothly rotate the player towards the target on the X and Z axes
            playerTransform.rotation = Quaternion.Lerp(playerTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }*/

        if (dialogueInitiated)
        {
            InputManager.disabled = true;
            if (dialogueInitiated && !dialogueScript.IsTyping() && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0)))
            {
                Debug.Log("next dialogue????");
                // Display the next dialogue
                ShowNextDialogue();
            }

        }
        if (gabrielSequenceReady)
        {
            StartCoroutine(GabrielSequence());
        }
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
                        player.transform.position = new Vector3(0, 0, 0);
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
                            gabrielSequenceReady = true;
                            //close book
                            InputManager.disabled = false;
                            readingBackground.SetActive(false);
                            wizardBook.SetActive(false);
                            //pickUpText.SetActive(true);
                            
                        }
                    }
                }
                else pickUpText.SetActive(false);
            }
            else pickUpText.SetActive(false);
        }
    }
    /*    void ShowNextDialogue()
        {
            Debug.Log("current dialogue index: " + currentDialogueIndex);
            // Check if there are more dialogues to display
            if (currentDialogueIndex < dialogues.Count)
            {

                // Display the current dialogue text
                dialogueText.text = dialogues[currentDialogueIndex];

                // Increment the dialogue index for the next time
                currentDialogueIndex++;
            }
            else
            {
    *//*            // No more dialogues, hide the dialogue panel
                dialogue.SetActive(false);*//*
            }
        }*/
    void ShowNextDialogue()
    {
        Debug.Log("current dialogue index: " + currentDialogueIndex);

        // Check if there are more dialogues to display
        if (currentDialogueIndex < dialogues.Count)
        {
            // Display the current dialogue text with typewriter effect
            dialogueScript.SetNewText(dialogues[currentDialogueIndex]);

            // Increment the dialogue index for the next time
            currentDialogueIndex++;
        }
        else
        {
            // No more dialogues, hide the dialogue panel or perform other actions
            // For now, let's just log a message
            Debug.Log("No more dialogues");
            gabrielSequenceReady = false;
            StopAllCoroutines();
            dialogueInitiated = false;
            InputManager.disabled = false;
            dialogueBox.SetActive(false);
            exitDoor.SetActive(true);

            Vector3 currentEulerAngles = playerTransform.rotation.eulerAngles;
            currentEulerAngles.x = 0f;
            playerTransform.rotation = Quaternion.Euler(currentEulerAngles);
        }
    }

    void turnToGabe()
    {
        if (Gabriel != null && playerTransform != null)
        {
            //FOR TRAILER<TRIPPY MOVEMENT>
            Vector3 directionToTarget = GabrielHead.gameObject.transform.position - playerTransform.position;

            // Use Quaternion.LookRotation without modifying the Y component
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

            // Smoothly rotate the player towards the target on all axes
            playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }


    IEnumerator GabrielSequence()
    {
        InputManager.disabled = true;
        turnToGabe();
        yield return new WaitForSeconds(1);
        dialogueInitiated = true;
        //Gabriel.gameObject.GetComponent<BoxCollider>().enabled = false;
        dialogueBox.SetActive(true);
    }

    IEnumerator teleportFromDungeon()
    {
        yield return new WaitForSeconds(0.1f);
        player.transform.position = new Vector3(871, 33, 903);
        bridge.SetActive(true);
        seaMonster.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        InputManager.disabled = false; // Ensure that player input is enabled after teleporting
    }


}
