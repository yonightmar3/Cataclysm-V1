using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class monsterAgent : MonoBehaviour
{
    private GameObject player;
    private NavMeshAgent agent;

    public bool moving;


    public float value;

    public float attackRange, sightRange;
    public static bool inAttackRange, inSightRange;

    public LayerMask whatIsGround, whatIsPlayer;

    private Vector3 initialPosition;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointsRange;

    public float waitTime = 2f;
    private float waitTimer = 0f;
    private bool waiting = false;

    //Attacking
    public float alreadyAttacked;

    private void Awake()
    {
        player = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();
        initialPosition = agent.transform.position;
        //value = sightRange;
    }
    public void Update()
    {
        moving = agent.velocity.magnitude > 1f;

        inSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        inAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        /*        if (shadowstep.lowerSights == true && !shadowstep.outOfPower && !inSightRange)
                {
                    sightRange = 5;

                }
                else sightRange = value;*/

        if (!inSightRange && !inAttackRange)
        {
            Patroling();
            //moving = true;
        }

        else if (inSightRange && !inAttackRange)
        {
            ChasePlayer();
            
        }

        else if (inAttackRange && inSightRange)
        {
            Attack();
        }


    }


    private void Patroling()
    {
        if (!walkPointSet)
        {
            if (!waiting)
            {
                SearchWalkPoint();
            }
        }
        else
        {
            agent.SetDestination(walkPoint);
            //moving = true;

            if (Vector3.Distance(transform.position, walkPoint) < 1f)
            {
                if (!waiting)
                {
                    waitTimer = 0f;
                    waiting = true;
                }
                else
                {
                    waitTimer += Time.deltaTime; //time counter
                    if (waitTimer >= waitTime)
                    {
                        waiting = false;
                        walkPointSet = false;
                    }
                }
            }
        }
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointsRange, walkPointsRange);
        float randomX = Random.Range(-walkPointsRange, walkPointsRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround)) walkPointSet = true;
    }

    private void Attack()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player.transform);
    }
    private void ChasePlayer()
    {
        agent.SetDestination(player.transform.position);
    }

}





