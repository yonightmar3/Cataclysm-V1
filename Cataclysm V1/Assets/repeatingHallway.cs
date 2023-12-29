using System.Collections;
using UnityEngine;

public class RepeatingHallway : portalTeleporter
{
    public GameObject appearableWall;
    private int amountOfTeleports;
    public GameObject portal;
    public GameObject mainDoor;

    private void Update()
    {
        if (teleported == true){
            appearableWall.SetActive(true);
            amountOfTeleports++;
            teleported = false;
        }
        if(amountOfTeleports >= 3)
        {
            mainDoor.SetActive(false);
            portal.SetActive(true);
        }
        Debug.Log("Amount of teleports: " + amountOfTeleports);

        base.Update();
    }
}