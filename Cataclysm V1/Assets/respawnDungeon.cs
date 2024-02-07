using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class respawnDungeon : MonoBehaviour
{
    private GameObject player;
    public static bool respawned = false;

    public static bool starved;
    private Transform latestSpawn;

    [SerializeField] private Transform[] spawnPoints;

    [SerializeField] private GameObject lightOrb;
    [SerializeField] private GameObject spotlight;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        latestSpawn = spawnPoints[0];
    }

    private void Update()
    {
        if (playerActions.dead/* && respawned == false*/ && SceneManager.GetActiveScene().name == "Dungeon")
        {
            player.transform.position = latestSpawn.position;
            StartCoroutine(respawn());
            //closestEnemy.dying = false;
            playerActions.dead = false;
            starvedAgent.jumpscared = false;
            eventManager.keyObtained = false;
            respawned = true;
            if (latestSpawn == spawnPoints[0])
            {
            }
            
        }

        if (starved)
        {
            latestSpawn = spawnPoints[1];
        }
    }

    IEnumerator respawn()
    {
        InputManager.disabled = true;
        yield return new WaitForSeconds(0.1f);
        player.transform.position = latestSpawn.position;
        yield return new WaitForSeconds(.1f);
        InputManager.disabled = false;
    }

}
