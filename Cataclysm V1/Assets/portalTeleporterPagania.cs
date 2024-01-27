using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portalTeleporterPagania : portalTeleporter
{
    [SerializeField] private GameObject paganiaSetup;
    [SerializeField] private GameObject mainCam;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            mainCam.SetActive(false);
            paganiaSetup.SetActive(true);
        }
    }
}
