using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawnMainMap : MonoBehaviour
{
    private GameObject player;

    [SerializeField] private Transform[] spawnPoints;
    // Start is called before the first frame update


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
}
