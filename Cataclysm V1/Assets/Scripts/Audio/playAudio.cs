using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playAudio : MonoBehaviour
{
    public AudioSource doorSlam;

    void playDoorSlam()
    {
        doorSlam.Play();
    }
}
