using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playAudio : MonoBehaviour
{
    public AudioSource doorSlam;
    public AudioSource audioSource;

    void playDoorSlam()
    {
        doorSlam.Play();
    }

    void playSound()
    {
        audioSource.Play();
    }
}
