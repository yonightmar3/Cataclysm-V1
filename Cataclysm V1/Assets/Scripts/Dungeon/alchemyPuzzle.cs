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



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
                if (lookingAtClose.name == "Salt Crystal")
                {
                    pickUpText.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        saltCrystal = true;
                        lookingAtClose.SetActive(false);
                        Debug.Log("salt crystal obtained");
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
                        blackEgg = true;
                        lookingAtClose.SetActive(false);
                        Debug.Log("black egg feather obtained");
                    }
                }
                else pickUpText.SetActive(false);
            }
            else pickUpText.SetActive(false);
        }
    }
}
