using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class mazeDweller : MonoBehaviour
{
    public static bool playerSeen = false;
    public Transform player;
    private NavMeshAgent monster;
    public GameObject deathScreen;
    /*public Transform spawn;
    private Transform spawnTest;*/
    //FOOTSTEPS
    private Vector3 previousPosition;
    private bool isMoving;// = false;
    // Adjust this value based on your desired sensitivity
    //private float movementThreshold = 0.1f;
    public AudioSource footsteps;
    // Start is called before the first frame update
    void Start()
    {
        monster = GetComponent<NavMeshAgent>();
        previousPosition = transform.position;
        //spawnTest = gameObject.transform;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPosition = player.position;

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
            //monster.destination = spawn.position;
            monster.destination = previousPosition;
            isMoving = false;
            //deathScreen.SetActive(false);
        }

/*        if (playerSeen)
        {
            monster.destination = player.position;
            isMoving = true;
        }
        else
        {
            //monster.destination = spawn.position;
            monster.destination = previousPosition;
            isMoving = false;
            //deathScreen.SetActive(false);
        }*/

/*        if (Vector3.Distance(transform.position, previousPosition) >= movementThreshold)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
            Debug.Log("wtf");
        }*/

        // Update the previous position for the next frame NO!!!!!
        //previousPosition = transform.position;

        // Use the isMoving variable as needed (e.g., log, perform actions, etc.)
        if (isMoving)
        {
            //Debug.Log("Object is moving!");

            footsteps.enabled = true;
        }
        else footsteps.enabled = false;


    }

    private void OnTriggerEnter(Collider other)
    {
        deathScreen.SetActive(true);
        //playerSeen = false;
        Cursor.visible = true;
    }

    bool IsOnNavMesh(Vector3 position)
    {
        NavMeshHit hit;
        return NavMesh.SamplePosition(position, out hit, 2f, NavMesh.AllAreas);
    }



}
