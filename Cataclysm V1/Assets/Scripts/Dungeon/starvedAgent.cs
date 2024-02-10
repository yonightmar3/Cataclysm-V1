using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class starvedAgent : MonoBehaviour
{
    private NavMeshAgent starved;
    [SerializeField] private GameObject starvedGameObject;
    public Transform player;
    private bool playerSeen;
    public Animator starvedAnimator;

    private Transform originalLocation = null;
    private Quaternion originalRotation;


    public static bool jumpscared;
    [SerializeField] private GameObject deathScreen;
    //doing this bc of weird bug where footsteps resume after jumpscare
    [SerializeField] private GameObject footsteps;


    // Start is called before the first frame update
    void Start()
    {
        starved = GetComponent<NavMeshAgent>();
        originalLocation = new GameObject().transform;
        originalLocation.position = transform.position;
        originalRotation = transform.rotation;
    }


    // Update is called once per frame
    void Update()
    {
        if (respawnDungeon.respawned)
        {
            transform.position = originalLocation.position;
            transform.rotation = originalRotation;

            //starved.SetDestination(originalLocation.position);
            //starved.isStopped = false;
            //starvedAnimator.SetTrigger("reset");
            //respawnDungeon.respawned = false;
        }
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer > 3.5f && eventManager.keyObtained == true)
        {
            starved.isStopped = false;
            starved.destination = player.position;
            starvedAnimator.SetTrigger("running");
        }
        if (distanceToPlayer <= 2.5f && eventManager.keyObtained == true)
            {
            jumpscared = true;
                //starvedAnimator.SetTrigger("attackRange");
                starved.isStopped = true;

            //player looks at starved
/*            if (transform != null)
            {
                InputManager.disabled = true;
                // Get the direction to the enemy
                Vector3 directionToEnemy = player.transform.position - transform.position;
                directionToEnemy.y = 0f; // Ignore height difference

                if (directionToEnemy != Vector3.zero)
                {
                    // Calculate the rotation needed to look at the enemy
                    Quaternion targetRotation = Quaternion.LookRotation(-directionToEnemy);

                    // Slerp to smoothly rotate towards the enemy on the y-axis
                    player.transform.rotation = Quaternion.Slerp(player.transform.rotation, targetRotation, 10f * Time.deltaTime);

                    footsteps.SetActive(false);

                    StartCoroutine(jumpscare());                }
            }*/

            //starved looks at player
            if (player != null)
            {
                // Calculate the direction vector from the enemy to the player
                Vector3 directionToPlayer = player.position - transform.position;
                directionToPlayer.y = 0f; // Ignore the y-component to only rotate on the y-axis

                if (directionToPlayer != Vector3.zero)
                {
                    // Calculate the rotation to look at the player
                    Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);

                    // Smoothly rotate towards the player
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
                }

                
            }
            //starved.destination = transform.position;
            //starvedGameObject.transform.LookAt(player);


            /*            if (distanceToPlayer <= 1.5f)
                        {
                            starved.isStopped = true;
                            starved.destination = transform.position;
                            Debug.Log("stopped");
                            //starvedAnimator.SetTrigger("attackRange");
                        }*/
        }
        }

/*    IEnumerator jumpscare()
    {
        yield return new WaitForSeconds(1.4f);
        //deathScreen.SetActive(true);
        playerActions.dead = true;
        
    }*/
/*        if (distanceToPlayer <= 15f && distanceToPlayer > 2f)
        {
            starved.isStopped = false;
            starved.destination = player.position;
            starvedAnimator.SetTrigger("running");
        }*/

        
    }

