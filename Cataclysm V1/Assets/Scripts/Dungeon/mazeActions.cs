using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mazeActions : MonoBehaviour
{
    //contains maze and hiding room

    public Animator mazeDwellerAnimator;
    private GameObject player;
    public PlayerLook playerLookScript;
    private GameObject lookingAtFar;
    private GameObject lookingAtClose;


    public GameObject collapse1;
    public GameObject collapse1Sound;

    public GameObject collapse2;
    public GameObject collapse2Sound;

    public GameObject collapse3;

    public GameObject pickUpText;

    public bool hidden = false;


    public GameObject closet1Cam;
    public GameObject closet2Cam;


    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            mazeDweller.playerSeen = true;
            collapse1.SetActive(false);
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
                if (lookingAtFar.name == "mazeTrigger1")
                {
                    mazeDweller.playerSeen = true;
                    mazeDwellerAnimator.SetTrigger("playerSeen");
                    collapse1.SetActive(false);
                    collapse1Sound.SetActive(true);
                }
                if (lookingAtFar.name == "mazeTrigger2")
                {
                    Debug.Log("transfer works");
                    collapse2.SetActive(false);
                    collapse2Sound.SetActive(true);
                    collapse3.SetActive(false);
                    /*pillar1.SetActive(true);
                    pillar2.SetActive(true);*/
                }
            }
            if (hitInfoClose.collider != null)
            {
                lookingAtClose = hitInfoClose.transform.gameObject;
/*                if (lookingAtClose.name == "mazeTrigger1")
                {
                    mazeDweller.playerSeen = true;
                    mazeDwellerAnimator.SetTrigger("playerSeen");
                    collapse1.SetActive(false);
                    collapse1Sound.SetActive(true);
                }*/
                if (lookingAtClose.name == "Large Hiding Cabinet")
                {
                        pickUpText.SetActive(true);
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            hidden = true;
                            StartCoroutine("hideCloset");
                            Debug.Log("entering closet");
                            pickUpText.SetActive(true);

                        }
                        if (Input.GetKeyDown(KeyCode.F))
                        {
                            hidden = false;
                            closet1Cam.SetActive(false);
                            player.SetActive(true);
                        }
                }
                    if (lookingAtClose.name == "Small Hiding Cabinet")
                    {
                        pickUpText.SetActive(true);
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            hidden = true;
                            StartCoroutine("hideSmallCloset");
                            Debug.Log("entering closet");
                            pickUpText.SetActive(true);

                        }
                        if (Input.GetKeyDown(KeyCode.F))
                        {
                            hidden = false;
                            closet2Cam.SetActive(false);
                            player.SetActive(true);
                        }
                    }
                }
            }
        
    }

    IEnumerator hideCloset()
    {
        player.SetActive(false);

        closet1Cam.SetActive(true);
        Debug.Log("player hidden");
        yield return new WaitForSeconds(0.6f);

    }
    IEnumerator hideSmallCloset()
    {
        player.SetActive(false);

        closet2Cam.SetActive(true);
        Debug.Log("player hidden");
        yield return new WaitForSeconds(0.6f);

    }


}
                
