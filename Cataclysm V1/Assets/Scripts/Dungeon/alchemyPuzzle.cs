using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alchemyPuzzle : MonoBehaviour
{
    public PlayerLook playerLookScript;
    public GameObject pickUpText;
    private GameObject lookingAtClose;

    private bool carrying;

    private bool saltCrystal;
    private bool bluejayFeather;
    private bool blackEgg;

    private bool lizardTail;


    private bool wrongIngredient;

    private int ingredientsDeposited = 0;

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

                else if (lookingAtClose.name == "Cauldron")
                {
                    pickUpText.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        if(carrying == true)
                        {
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


                else pickUpText.SetActive(false);
            }
            else pickUpText.SetActive(false);
        }
        if (ingredientsDeposited == 3)
        {
            if (!wrongIngredient)
            {
                Debug.Log("successfully made potion");
            }
            else Debug.Log("messed up");
        }
    }
}
