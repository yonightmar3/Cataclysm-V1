using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portalTeleporterOrigin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        portalTeleporterToPagania.paganiaVisited = false;
    }
}
