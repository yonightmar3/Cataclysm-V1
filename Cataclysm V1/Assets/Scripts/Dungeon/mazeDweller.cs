using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class mazeDweller : MonoBehaviour
{
    public mazeActions mazeActionsScript;
    public static bool playerSeen = false;
    public Transform player;
    private NavMeshAgent monster;
    public GameObject deathScreen;

    public Transform spawnPoint;

    public Transform bridge;
    /*public Transform spawn;
    private Transform spawnTest;*/
    //FOOTSTEPS
    private Vector3 previousPosition;
    private bool isMoving;// = false;

    public AudioSource footsteps;
    void Start()
    {
        monster = GetComponent<NavMeshAgent>();
        previousPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if(mazeActionsScript != null)
        {
            Vector3 playerPosition = player.position;

            if(mazeActionsScript.hidden == false)
            {
                if (IsOnNavMesh(playerPosition))
                {
                    if (playerSeen)
                    {
                        monster.destination = player.position;
                        isMoving = true;
                    }
                }
                else
                {
                    monster.destination = previousPosition;
                    isMoving = false;
                }
            } 
            else if (mazeActionsScript.hidden == true)
            {
                if (GetDistanceToPlayer() > 10f)
                {
                    //player hid well
                    //Debug.Log("hid well");
                    //monster goes to bridge
                    monster.destination = bridge.position;
                    playerSeen = false;
                }
                else
                {
                    //player didn't hide quikly enuff and monster jumpscares
                    //Debug.Log("hid poorly");
                    monster.destination = player.position;
                }
            } 
            



            //FOOTSTEPS
            if (isMoving)
            {
                footsteps.enabled = true;
            }
            else footsteps.enabled = false;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        deathScreen.SetActive(true);
        InputManager.disabled = true;
        //playerSeen = false;
        Cursor.visible = true;
        isMoving = false;
        monster.transform.position = spawnPoint.position;
    }

    bool IsOnNavMesh(Vector3 position)
    {
        NavMeshHit hit;
        return NavMesh.SamplePosition(position, out hit, 2f, NavMesh.AllAreas);
    }

    float GetDistanceToPlayer()
    {
        // Check if both the enemyAgent and player are valid
        if (monster != null && player != null)
        {
            // Calculate the distance between the enemy and player
            float distance = Vector3.Distance(transform.position, player.position);
            return distance;
        }

        // Return a large value if either the enemyAgent or player is not valid
        return float.MaxValue;
    }

    //if player is hidden and range between dweller and player is greater than xyz, player is hidden and dweller's path is set to the bridge
    //else if the player is hidden but range is too small, dweller opens the doors and jumpscares you
    //else if player is not hidden he jumpscares you as if in the maze



}
