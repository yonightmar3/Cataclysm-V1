using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portalTeleporterToPagania : portalTeleporter
{
    public static bool paganiaVisited = false;
    [SerializeField] private Material blueSky;
    [SerializeField] private GameObject paganiaSetup;
    [SerializeField] private GameObject mainCam;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && paganiaVisited == false)
        {
            mainCam.SetActive(false);
            paganiaSetup.SetActive(true);
            PlayerLook.pagania = true;
            RenderSettings.skybox = blueSky;
            paganiaVisited = true;
        }
    }
}
