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
            Debug.Log(teleported);
            appearableWall.SetActive(true);
            amountOfTeleports++;
            teleported = false;
            Debug.Log(amountOfTeleports);

        }
        if (amountOfTeleports >= 3)
        {
            mainDoor.SetActive(false);
            portal.SetActive(true);
        }

        base.Update();
    }
}