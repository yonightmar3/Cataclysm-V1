/*using System.Collections;
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
    private bool wineBottle;
    private bool anglerLight;
    private bool mushroom;
    private bool strigaFinger;
    private bool horseHoof;
    private bool whaleOil;
    private bool catTail;
    private bool roosterFoot;
    private bool lavender;


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
                            Debug.Log("black egg obtained");
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

                else if (lookingAtClose.name == "Wine Bottle")
                {
                    pickUpText.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {

                        if (carrying == false)
                        {
                            carrying = true;
                            wineBottle = true;
                            lookingAtClose.SetActive(false);
                            Debug.Log("carrying: " + carrying);
                            Debug.Log("wine bottle obtained");
                        }

                    }
                }

                else if (lookingAtClose.name == "Angler Light")
                {
                    pickUpText.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {

                        if (carrying == false)
                        {
                            carrying = true;
                            anglerLight = true;
                            lookingAtClose.SetActive(false);
                            Debug.Log("carrying: " + carrying);
                            Debug.Log("angler light obtained");
                        }

                    }
                }

                else if (lookingAtClose.name == "Mushroom")
                {
                    pickUpText.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {

                        if (carrying == false)
                        {
                            carrying = true;
                            mushroom = true;
                            lookingAtClose.SetActive(false);
                            Debug.Log("carrying: " + carrying);
                            Debug.Log("mushroom obtained");
                        }

                    }
                }

                else if (lookingAtClose.name == "Striga Finger")
                {
                    pickUpText.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {

                        if (carrying == false)
                        {
                            carrying = true;
                            strigaFinger = true;
                            lookingAtClose.SetActive(false);
                            Debug.Log("carrying: " + carrying);
                            Debug.Log("striga finger obtained");
                        }

                    }
                }

                else if (lookingAtClose.name == "Horse Hoof")
                {
                    pickUpText.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {

                        if (carrying == false)
                        {
                            carrying = true;
                            horseHoof = true;
                            lookingAtClose.SetActive(false);
                            Debug.Log("carrying: " + carrying);
                            Debug.Log("horse hoof obtained");
                        }

                    }
                }

                else if (lookingAtClose.name == "Whale Oil")
                {
                    pickUpText.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {

                        if (carrying == false)
                        {
                            carrying = true;
                            whaleOil = true;
                            lookingAtClose.SetActive(false);
                            Debug.Log("carrying: " + carrying);
                            Debug.Log("whale oil obtained");
                        }

                    }
                }

                else if (lookingAtClose.name == "Cat Tail")
                {
                    pickUpText.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {

                        if (carrying == false)
                        {
                            carrying = true;
                            catTail = true;
                            lookingAtClose.SetActive(false);
                            Debug.Log("carrying: " + carrying);
                            Debug.Log("cat tail obtained");
                        }

                    }
                }

                else if (lookingAtClose.name == "Rooster Foot")
                {
                    pickUpText.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {

                        if (carrying == false)
                        {
                            carrying = true;
                            roosterFoot = true;
                            lookingAtClose.SetActive(false);
                            Debug.Log("carrying: " + carrying);
                            Debug.Log("rooster foot obtained");
                        }

                    }
                }

                else if (lookingAtClose.name == "Lavender")
                {
                    pickUpText.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {

                        if (carrying == false)
                        {
                            carrying = true;
                            lavender = true;
                            lookingAtClose.SetActive(false);
                            Debug.Log("carrying: " + carrying);
                            Debug.Log("lavender obtained");
                        }

                    }
                }

                else if (lookingAtClose.name == "Cauldron" && canDeposit == true)
                {
                    pickUpText.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        if (carrying == true)
                        {
                            StartCoroutine(ingredientFire());
                            ingredientsDeposited++;
                            Debug.Log("Ingredients deposited: " + ingredientsDeposited);
                            if (catTail || bluejayFeather || blackEgg || horseHoof || strigaFinger || whaleOil)
                            {
                                Debug.Log("correct ingredient");
                                carrying = false;
                            }
                            if (lizardTail || wineBottle || lizardTail || roosterFoot || saltCrystal || anglerLight || lavender || mushroom){
                            //else { 
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
        if (ingredientsDeposited == 6)
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



*/

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

    private bool correctIngredient;
    private bool wrongIngredient;


    public GameObject shadowRing;

    private bool lizardTail;


    //private bool wrongIngredient;

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
                if (lookingAtClose.tag == "correctIngredient")
                {
                    pickUpText.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        if (carrying == false)
                        {
                            correctIngredient = true;
                            carrying = true;
                            lookingAtClose.SetActive(false);
                        }
                    }
                }

                else if (lookingAtClose.tag == "wrongIngredient")
                {
                    pickUpText.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        if (carrying == false)
                        {
                            wrongIngredient = true;
                            carrying = true;
                            lookingAtClose.SetActive(false);
                        }

                    }
                }
                

                else if (lookingAtClose.name == "Cauldron" && canDeposit == true)
                {
                    pickUpText.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        if (carrying == true)
                        {
                            StartCoroutine(ingredientFire());
                            ingredientsDeposited++;
                            Debug.Log("Ingredients deposited: " + ingredientsDeposited);
                            if (correctIngredient)
                            {
                                Debug.Log("correct ingredient");
                                carrying = false;
                            }
                            if (wrongIngredient)
                            {
                                //else { 
                                Debug.Log("wrong ingredient");
                                carrying = false;
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
        //increase to 6 later
        if (ingredientsDeposited == 3)
        {
            canDeposit = false;

            if (!wrongIngredient)
            {
                Debug.Log("successfully made potion");
                shadowRing.SetActive(true);
            }
            else if (wrongIngredient)
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



