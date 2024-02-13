using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deployLightOrb : MonoBehaviour
{
    [SerializeField] private GameObject lightOrb;
    public static bool animationStart = false;
    public void deployOrb()
    {
        lightOrb.SetActive(true);
        PlayerLook.orbDeployed = true;
    }

    public void animationStartTrigger()
    {
        animationStart = true;
    }

    public void animationEndTrigger()
    {
        animationStart = false;
    }
}
