using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portalTeleporterFromPagania : portalTeleporter
{
    [SerializeField] private Material darkSky;
    [SerializeField] private GameObject paganiaSetup;
    [SerializeField] private GameObject mainCam;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            mainCam.SetActive(true);
            paganiaSetup.SetActive(false);
            PlayerLook.pagania = false;
            RenderSettings.skybox = darkSky;
        }
    }
}
