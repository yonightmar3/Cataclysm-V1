using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alchemyPuzzle : MonoBehaviour
{
    public PlayerLook playerLookScript;
    public GameObject pickUpText;
    private GameObject lookingAtClose;
    public GameObject jumpscare;

    private bool carrying;
    private bool canDeposit = true;

    private bool saltCrystal;
    private bool bluejayFeather;
    private bool blackEgg;

    public GameObject shadowRing;

    private bool lizardTail;


    private bool wrongIngredient;

    public GameObject fireRed;

    private int ingredientsDeposited = 0;

    public GameObject leaveDungeonPortal;

    // Update is called once per frame
    void Update()
    {
        if (playerLookScript != null)
        {
            RaycastHit hitInfoClose = playerLookScript.HitInfoClose;

            if (hitInfoClose.collider != null)
            {
                lookingAtClose = hitInfoClose.transform.gameObject;

                //PUZZLE 1
                if (lookingAtClose.name == "Salt Crystal")
                {
                    pickUpText.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        if(carrying == false)
                        {
                            saltCrystal = true;
                            carrying = true;
                            lookingAtClose.SetActive(false);
                            Debug.Log("salt crystal obtained");
                        }                     
                    }
                }

                else if (lookingAtClose.name == "Bluejay Feather")
                {
                    pickUpText.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        if (carrying == false)
                        {
                            bluejayFeather = true;
                            carrying = true;
                            lookingAtClose.SetActive(false);
                            Debug.Log("bluejay feather obtained");
                        }

                    }
                }
                else if (lookingAtClose.name == "Black Egg")
                {
                    pickUpText.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {

                        if (carrying == false)
                        {
                            carrying = true;
                            blackEgg = true;
                            lookingAtClose.SetActive(false);
                            Debug.Log("carrying: " + carrying);
                            Debug.Log("black egg feather obtained");
                        }
                        
                    }
                }
                else if (lookingAtClose.name == "Lizard Tail")
                {
                    pickUpText.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {

                        if (carrying == false)
                        {
                            carrying = true;
                            lizardTail = true;
                            lookingAtClose.SetActive(false);
                            Debug.Log("carrying: " + carrying);
                            Debug.Log("lizard tail obtained");
                        }

                    }
                }

                else if (lookingAtClose.name == "Cauldron" && canDeposit == true)
                {
                    pickUpText.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        if(carrying == true)
                        {
                            StartCoroutine(ingredientFire());
                            ingredientsDeposited++;
                            Debug.Log("Ingredients deposited: " + ingredientsDeposited);
                            if (saltCrystal || bluejayFeather || blackEgg)
                            {
                                Debug.Log("correct ingredient");
                                carrying = false;
                            }
                            if(lizardTail){
                                Debug.Log("wrong ingredient");
                                carrying = false;
                                wrongIngredient = true;
                            }                           
                        }                      
                    }
                }

                else if (lookingAtClose.name == "Shadow Ring")
                {
                    pickUpText.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        leaveDungeonPortal.SetActive(true);
                        shadowRing.SetActive(false);
                    }
                }


                else pickUpText.SetActive(false);
            }
            else pickUpText.SetActive(false);
        }
        if (ingredientsDeposited == 3)
        {
            canDeposit = false;

            if (!wrongIngredient)
            {
                Debug.Log("successfully made potion");
                shadowRing.SetActive(true);
            }
            else
            {
                Debug.Log("messed up");
                StartCoroutine("jumpscareText");
            }

        }


    }
    IEnumerator jumpscareText()
    {
        InputManager.disabled = true;
        yield return new WaitForSeconds(.1f);
        jumpscare.SetActive(true);
        yield return new WaitForSeconds(1f);
        jumpscare.SetActive(false);
        InputManager.disabled = false;
    }

    IEnumerator ingredientFire()
    {
        fireRed.SetActive(true);
        yield return new WaitForSeconds(.5f);
        fireRed.SetActive(false);

    }
}



