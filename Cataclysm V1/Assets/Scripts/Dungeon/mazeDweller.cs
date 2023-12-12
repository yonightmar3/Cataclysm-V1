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
    public Transform spawn;
    //FOOTSTEPS
    private Vector3 previousPosition;
    private bool isMoving;
    // Adjust this value based on your desired sensitivity
    public float movementThreshold = 1f;
    public AudioSource footsteps;
    // Start is called before the first frame update
    void Start()
    {
        monster = GetComponent<NavMeshAgent>();
        previousPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        
        if (playerSeen)
        {
            monster.destination = player.position;
        }
        else
        {
            monster.destination = spawn.position;
            //deathScreen.SetActive(false);
        }
        if (Vector3.Distance(transform.position, previousPosition) > movementThreshold)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        // Update the previous position for the next frame
        previousPosition = transform.position;

        // Use the isMoving variable as needed (e.g., log, perform actions, etc.)
        if (isMoving)
        {
            Debug.Log("Object is moving!");

            footsteps.enabled = true;
        }
        else footsteps.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        deathScreen.SetActive(true);
        playerSeen = false;
        Cursor.visible = true;
    }

}
