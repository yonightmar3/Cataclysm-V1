using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerActions : MonoBehaviour
{
    [SerializeField] private GameObject deathScreen;
    public static bool dead;

    void Update()
    {
        if (Input.GetKey(KeyCode.K))
        {
            dead = true;
        }
        if (dead)
        {
            //deathScreen.SetActive(true);
        }
    }

    public void respawn()
    {
        //deathScreen.SetActive(false);
        Debug.Log("respawned");
    }
}
