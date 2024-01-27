using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portalTeleporterToPagania : portalTeleporter
{
    [SerializeField] private Material blueSky;
    [SerializeField] private GameObject paganiaSetup;
    [SerializeField] private GameObject mainCam;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            mainCam.SetActive(false);
            paganiaSetup.SetActive(true);
            PlayerLook.pagania = true;
            RenderSettings.skybox = blueSky;
        }
    }
}
