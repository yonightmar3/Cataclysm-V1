using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portalCameraRenderer : MonoBehaviour
{
    public PlayerLook playerLookScript;
    private GameObject lookingAtPortal;

    public GameObject cameraToEuclidean;


    private void Update()
    {
        if (playerLookScript != null)
        {
            // Access the HitInfo property from the PlayerLook script
            RaycastHit hitInfoFar = playerLookScript.HitInfoFar;
            if (hitInfoFar.collider != null)
            {
                lookingAtPortal = hitInfoFar.transform.gameObject;
                if (lookingAtPortal.transform.gameObject.name == "Render Plane to Euclid")
                {
                    Debug.Log("working cam");
                    cameraToEuclidean.SetActive(true);
                }
                else cameraToEuclidean.SetActive(false);
            }
        }
    }
}
