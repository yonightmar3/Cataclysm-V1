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


    // Start is called before the first frame update
    void Start()
    {
        starved = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer > 3.5f && eventManager.keyObtained == true)
        {
            starved.isStopped = false;
            starved.destination = player.position;
            starvedAnimator.SetTrigger("running");
        }
        if (distanceToPlayer <= 2.5f && eventManager.keyObtained == true)
            {
                starvedAnimator.SetTrigger("attackRange");
                starved.isStopped = true;
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
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5f * Time.deltaTime);
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
/*        if (distanceToPlayer <= 15f && distanceToPlayer > 2f)
        {
            starved.isStopped = false;
            starved.destination = player.position;
            starvedAnimator.SetTrigger("running");
        }*/

        
    }

