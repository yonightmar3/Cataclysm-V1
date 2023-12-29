using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class repeatingHallwayDoorSlam : MonoBehaviour
{
    public Animator repeatingHallwayDoor;
    private void OnTriggerEnter(Collider other)
    {
        repeatingHallwayDoor.ResetTrigger("doorOpen");
        repeatingHallwayDoor.SetTrigger("doorClose");
    }
}
