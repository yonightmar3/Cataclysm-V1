using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerActions : MonoBehaviour
{
    [SerializeField] private GameObject deathScreen;
    public static bool dead;

    void Update()
    {
        if (dead)
        {
            deathScreen.SetActive(true);
        }
    }
}
