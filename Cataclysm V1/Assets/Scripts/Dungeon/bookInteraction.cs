using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookInteraction : MonoBehaviour
{
    public PlayerLook playerLookScript;
    private GameObject lookingAtClose;
    public GameObject pickUpText;
    private bool isBookOpen = false;

    public GameObject readingBackground;
    public GameObject entropyBook;
    public GameObject wisdomBook;
    public GameObject soulBook;
    public GameObject fateBook;


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
                            //close book
                            InputManager.disabled = true;
                            readingBackground.SetActive(true);
                            entropyBook.SetActive(true);
                            pickUpText.SetActive(false);
                        }
                        else
                        {
                            //open book
                            InputManager.disabled = false;
                            readingBackground.SetActive(false);
                            entropyBook.SetActive(false);
                            pickUpText.SetActive(true);
                        }
                    }
                }
                else if (lookingAtClose.name == "Wisdom Book")
                {
                    pickUpText.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        isBookOpen = !isBookOpen;

                        if (isBookOpen)
                        {
                            InputManager.disabled = true;
                            readingBackground.SetActive(true);
                            wisdomBook.SetActive(true);
                            pickUpText.SetActive(false);
                        }
                        else
                        {
                            InputManager.disabled = false;
                            readingBackground.SetActive(false);
                            wisdomBook.SetActive(false);
                            pickUpText.SetActive(true);
                        }
                    }
                }
                else if (lookingAtClose.name == "Soul Book")
                {
                    pickUpText.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        isBookOpen = !isBookOpen;

                        if (isBookOpen)
                        {
                            InputManager.disabled = true;
                            readingBackground.SetActive(true);
                            soulBook.SetActive(true);
                            pickUpText.SetActive(false);
                        }
                        else
                        {
                            InputManager.disabled = false;
                            readingBackground.SetActive(false);
                            soulBook.SetActive(false);
                            pickUpText.SetActive(true);
                        }
                    }
                }
                else if (lookingAtClose.name == "Fate Book")
                {
                    pickUpText.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        isBookOpen = !isBookOpen;

                        if (isBookOpen)
                        {
                            InputManager.disabled = true;
                            readingBackground.SetActive(true);
                            fateBook.SetActive(true);
                            pickUpText.SetActive(false);
                        }
                        else
                        {
                            InputManager.disabled = false;
                            readingBackground.SetActive(false);
                            fateBook.SetActive(false);
                            pickUpText.SetActive(true);
                        }
                    }
                }

                else
                {
                    pickUpText.SetActive(false);
                }
            }
            else pickUpText.SetActive(false);

        }
    }
}
