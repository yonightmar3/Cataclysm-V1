using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mazeActions : MonoBehaviour
{
    public PlayerLook playerLookScript;
    private GameObject lookingAt;

    public GameObject collapse1;
    public GameObject collapse1Sound;

    public GameObject collapse2;
    public GameObject collapse2Sound;

    public GameObject collapse3;


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
            RaycastHit hitInfo = playerLookScript.HitInfo;

            // Now you can use hitInfo in this script
            if (hitInfo.collider != null)
            {
                lookingAt = hitInfo.transform.gameObject;
                // Do something with hitInfo
                //Debug.Log("Hit object: " + hitInfo.collider.gameObject.name);
                if (lookingAt.name == "mazeTrigger1")
                {
                    mazeDweller.playerSeen = true;
                    collapse1.SetActive(false);
                    collapse1Sound.SetActive(true);
                }
                if (lookingAt.name == "mazeTrigger2")
                {
                    Debug.Log("transfer works");
                    collapse2.SetActive(false);
                    collapse2Sound.SetActive(true);
                    collapse3.SetActive(false);
                    /*pillar1.SetActive(true);
                    pillar2.SetActive(true);*/
                }

            }
        }
    }

}
                
