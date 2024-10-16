/*using System.Collections;
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

        *//*        if (shadowstep.lowerSights == true && !shadowstep.outOfPower && !inSightRange)
                {
                    sightRange = 5;

                }
                else sightRange = value;*//*

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerActions.dead = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerActions.dead = false;
            //respawnMainMap.respawned = false;
        }
    }

}





*/
/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class monsterAgent : MonoBehaviour
{
    private GameObject player;
    private NavMeshAgent agent;

    public bool moving;
    public bool chasing;
    public float attackRange, sightRange;
    public static bool inAttackRange, inSightRange;
    private Animator monsterAnimator;

    public LayerMask whatIsGround, whatIsPlayer;

    private Vector3 initialPosition; // The starting position of the monster

    // Patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointsRange;

    public float waitTime = 2f;
    private float waitTimer = 0f;
    private bool waiting = false;

    // Jumpscare
    private bool jumpscareTriggered = false;

    private void Awake()
    {
        player = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();
        monsterAnimator = GetComponent<Animator>();

        initialPosition = agent.transform.position; // Store initial position

        // Set stopping distance to help with precision at patrol points
        agent.stoppingDistance = 0.5f;
    }

    public void Update()
    {
        // Check if the agent is moving based on velocity threshold (more precise)
        moving = agent.velocity.magnitude > 0.1f;

        // Check sight and attack range for the player
        inSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        inAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!inSightRange && !inAttackRange && !jumpscareTriggered)
        {
            Patroling();
        }
        else if (inSightRange && !inAttackRange && !jumpscareTriggered)
        {
            ChasePlayer();
        }
        else if (inAttackRange && inSightRange && !jumpscareTriggered)
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
            // Set destination for the agent
            agent.SetDestination(walkPoint);

            // Check if the monster has reached the patrol point within stopping distance
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                // Start waiting when the monster reaches the patrol point
                if (!waiting)
                {
                    waitTimer = 0f;
                    waiting = true;
                    moving = false; // Not moving
                    monsterAnimator.SetBool("isIdle", true);
                    monsterAnimator.SetBool("isMoving", false);
                }
                else
                {
                    // Continue waiting at the patrol point
                    waitTimer += Time.deltaTime;
                    if (waitTimer >= waitTime)
                    {
                        waiting = false;
                        walkPointSet = false; // Set a new patrol point
                    }
                }
            }
            else
            {
                // Monster is still moving towards the patrol point
                moving = true;
                monsterAnimator.SetBool("isMoving", true);
                monsterAnimator.SetBool("isIdle", false);
            }
        }
    }

    private void SearchWalkPoint()
    {
        // Search for a new patrol point within a random range
        float randomZ = Random.Range(-walkPointsRange, walkPointsRange);
        float randomX = Random.Range(-walkPointsRange, walkPointsRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround)) walkPointSet = true;
    }

    private void ChasePlayer()
    {
        // Move the monster towards the player
        agent.SetDestination(player.transform.position);
        moving = true; // Ensure the monster is marked as moving
        chasing = true;
    }

    private void Attack()
    {
        // Attack logic (stop moving and trigger jumpscare)
        agent.SetDestination(transform.position);
        transform.LookAt(player.transform);
        moving = false;

        // Trigger the jumpscare when reaching the player
        TriggerJumpscare();
    }

    private void TriggerJumpscare()
    {
        if (!jumpscareTriggered)
        {
            jumpscareTriggered = true;
            // Trigger the jumpscare animation
            GetComponent<Animator>().SetTrigger("Jumpscare");

            // Start coroutine to handle death and reset after jumpscare
            StartCoroutine(HandleDeathAfterJumpscare());
        }
    }

    private IEnumerator HandleDeathAfterJumpscare()
    {
        // Wait for the duration of the jumpscare animation
        yield return new WaitForSeconds(3f); // Replace 3f with the actual length of your jumpscare animation

        // Mark player as dead (optional: trigger death UI or respawn system here)
        playerActions.dead = true;

        // Teleport the monster back to its initial position
        TeleportToInitialPosition();

        // Reset patrol or idle state
        ResetPatrol();

        // Explicitly reset the animator states after the jumpscare
        monsterAnimator.ResetTrigger("Jumpscare");  // Clear the jumpscare trigger
        monsterAnimator.SetBool("isIdle", true);  // Set idle to true as default
        monsterAnimator.SetBool("isMoving", false); // Ensure it isn't moving yet

        jumpscareTriggered = false; // Reset the jumpscare trigger flag
    }

    private void ResetPatrol()
    {
        // Reset patrol logic
        agent.ResetPath(); // Clear any path the agent had
        walkPointSet = false; // Force the monster to find a new patrol point
        waiting = false; // Reset waiting state
    }

    private void TeleportToInitialPosition()
    {
        // Directly set the monster's position back to its starting point
        //agent.Warp(initialPosition); // Warp teleports the agent safely
        agent.transform.position = initialPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            // The monster caught the player
            TriggerJumpscare();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerActions.dead = false;
            monsterAnimator.SetBool("isIdle", true);
        }
    }
}*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class monsterAgent : MonoBehaviour
{
    private GameObject player;
    private NavMeshAgent agent;

    public bool moving;
    public bool chasing;
    public float attackRange, sightRange;
    public static bool inAttackRange, inSightRange;
    private Animator monsterAnimator;

    public LayerMask whatIsGround, whatIsPlayer;

    private Vector3 initialPosition; // The starting position of the monster

    // Patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointsRange;

    public float waitTime = 2f;
    private float waitTimer = 0f;
    private bool waiting = false;

    // Jumpscare
    private bool jumpscareTriggered = false;

    private void Awake()
    {
        player = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();
        monsterAnimator = GetComponent<Animator>();

        initialPosition = agent.transform.position; // Store initial position

        // Set stopping distance to help with precision at patrol points
        agent.stoppingDistance = 0.5f;
    }

    public void Update()
    {
        // Debugging to check the animator state
        Debug.Log("isMoving: " + monsterAnimator.GetBool("isMoving"));
        Debug.Log("isIdle: " + monsterAnimator.GetBool("isIdle"));

        // Check if the agent is moving based on velocity threshold
        moving = agent.velocity.magnitude > 0.1f;

        // Check sight and attack range for the player
        inSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        inAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!inSightRange && !inAttackRange && !jumpscareTriggered)
        {
            Patroling();
        }
        else if (inSightRange && !inAttackRange && !jumpscareTriggered)
        {
            ChasePlayer();
        }
        else if (inAttackRange && inSightRange && !jumpscareTriggered)
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
            // Set destination for the agent
            agent.SetDestination(walkPoint);

            // Check if the monster has reached the patrol point within stopping distance
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                // Start waiting when the monster reaches the patrol point
                if (!waiting)
                {
                    waitTimer = 0f;
                    waiting = true;
                    moving = false; // Not moving
                    monsterAnimator.SetBool("isIdle", true);
                    monsterAnimator.SetBool("isMoving", false);
                }
                else
                {
                    // Continue waiting at the patrol point
                    waitTimer += Time.deltaTime;
                    if (waitTimer >= waitTime)
                    {
                        waiting = false;
                        walkPointSet = false; // Set a new patrol point
                    }
                }
            }
            else
            {
                // Monster is still moving towards the patrol point
                moving = true;
                monsterAnimator.SetBool("isMoving", true);
                monsterAnimator.SetBool("isIdle", false);
            }
        }
    }

    private void SearchWalkPoint()
    {
        // Search for a new patrol point within a random range
        float randomZ = Random.Range(-walkPointsRange, walkPointsRange);
        float randomX = Random.Range(-walkPointsRange, walkPointsRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround)) walkPointSet = true;
    }

    private void ChasePlayer()
    {
        // Move the monster towards the player
        agent.SetDestination(player.transform.position);
        moving = true; // Ensure the monster is marked as moving
        chasing = true;
        monsterAnimator.SetBool("isMoving", true);
        monsterAnimator.SetBool("isIdle", false);
    }

    private void Attack()
    {
        TriggerJumpscare();
        // Attack logic (stop moving and trigger jumpscare)
        agent.SetDestination(transform.position);
        transform.LookAt(player.transform);
        moving = false;

        // Trigger the jumpscare when reaching the player
        
    }

    private void TriggerJumpscare()
    {
        if (!jumpscareTriggered)
        {
            jumpscareTriggered = true;
            // Trigger the jumpscare animation
            monsterAnimator.SetTrigger("Jumpscare");


            // Start coroutine to handle death and reset after jumpscare
            StartCoroutine(HandleDeathAfterJumpscare());
        }
    }

    private IEnumerator HandleDeathAfterJumpscare()
    {
        // Wait for the full duration of the jumpscare animation
        yield return new WaitForSeconds(3f); // Ensure this matches your jumpscare animation length

        // Mark player as dead (optional: trigger death UI or respawn system here)
        playerActions.dead = true;

        // Teleport the monster back to its initial position
        TeleportToInitialPosition();

        // Reset patrol or idle state
        ResetPatrol();

        // Reset the jumpscare trigger in the animator
        monsterAnimator.ResetTrigger("Jumpscare");

        // Explicitly set the monster back to idle (optional)
        monsterAnimator.SetBool("isIdle", true);
        monsterAnimator.SetBool("isMoving", false);

        jumpscareTriggered = false; // Reset the jumpscare trigger flag
    }


    private void ResetPatrol()
    {
        // Reset patrol logic
        agent.ResetPath(); // Clear any path the agent had
        walkPointSet = false; // Force the monster to find a new patrol point
        waiting = false; // Reset waiting state
        // No immediate patrol call, wait for next Update cycle to manage it
    }

    private void TeleportToInitialPosition()
    {
        // Directly set the monster's position back to its starting point
        //agent.Warp(initialPosition); // Warp teleports the agent safely
        agent.transform.position = initialPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            // The monster caught the player
            TriggerJumpscare();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerActions.dead = false;
            monsterAnimator.SetBool("isIdle", true);
        }
    }
}














