/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bookInteraction : MonoBehaviour
{
    public PlayerLook playerLookScript;
    private GameObject lookingAtClose;
    public GameObject pickUpText;
    private bool isBookOpen = false;
    private bool bookSeen = false;


    public GameObject entropyBook;


    // Update is called once per frame
    void Update()
    {

        if (playerLookScript != null)
        {
            // Access the HitInfo property from the PlayerLook script
            RaycastHit hitInfoClose = playerLookScript.HitInfoClose;

            if (hitInfoClose.collider != null)
            {
                lookingAtClose = hitInfoClose.transform.gameObject;

                if (lookingAtClose.name == "Entropy Book")
                {
                    pickUpText.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        Debug.Log("book");

                        entropyBook.SetActive(true);
                        pickUpText.SetActive(false);
                        isBookOpen = true;
                    }
                }
                else pickUpText.SetActive(false);
            }

            

        }

        //bookSeen = false;

        if (isBookOpen == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {

                // Close the book
                entropyBook.SetActive(false);
                pickUpText.SetActive(true);
                isBookOpen = false;
                //bookSeen = false;
            }
        }


    }
}
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookInteraction : MonoBehaviour
{
    public PlayerLook playerLookScript;
    private GameObject lookingAtClose;
    public GameObject pickUpText;
    private bool isBookOpen = false;

    public GameObject entropyBook;

    void Update()
    {
        if (playerLookScript != null)
        {
            RaycastHit hitInfoClose = playerLookScript.HitInfoClose;

            if (hitInfoClose.collider != null)
            {
                lookingAtClose = hitInfoClose.transform.gameObject;

                if (lookingAtClose.name == "Entropy Book")
                {
                    pickUpText.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        isBookOpen = !isBookOpen;

                        if (isBookOpen)
                        {
                            InputManager.disabled = true;
                            entropyBook.SetActive(true);
                            pickUpText.SetActive(false);
                        }
                        else
                        {
                            InputManager.disabled = false;
                            entropyBook.SetActive(false);
                            pickUpText.SetActive(true);
                        }
                    }
                }
                else
                {
                    pickUpText.SetActive(false);
                }
            }
        }
    }
}
