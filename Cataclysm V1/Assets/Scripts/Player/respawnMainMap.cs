using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawnMainMap : MonoBehaviour
{
    private GameObject player;

    private bool marlton;
    private Transform latestSpawn;

    [SerializeField] private Transform[] spawnPoints;
    // Start is called before the first frame update
    
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        latestSpawn = spawnPoints[0];
    }

    private void Update()
    {
        if (playerActions.dead)
        {
            player.transform.position = latestSpawn.position;
        }
    }
}
