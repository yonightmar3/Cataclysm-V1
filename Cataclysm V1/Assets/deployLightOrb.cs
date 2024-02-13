using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deployLightOrb : MonoBehaviour
{
    [SerializeField] private GameObject lightOrb;
    public void deployOrb()
    {
        lightOrb.SetActive(true);
        PlayerLook.orbDeployed = true;
    }
}
