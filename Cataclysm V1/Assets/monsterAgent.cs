/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class monsterAgent : MonoBehaviour
{
    private GameObject player;
    private NavMeshAgent agent;
    private Quaternion originalPlayerRotation; // Store the original rotation of the player

    public bool moving;
    public float attackRange, sightRange;
    public static bool inAttackRange, inSightRange;
    private Animator monsterAnimator;
    public LayerMask whatIsGround, whatIsPlayer;

    [SerializeField] private AudioSource jumpscareNoise;

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
        monsterAnimator.SetBool("isMoving", true);
        monsterAnimator.SetBool("isIdle", false);
    }

    private void Attack()
    {
        TriggerJumpscare();
        jumpscareNoise.Play();

        // Attack logic (stop moving and trigger jumpscare)
        agent.SetDestination(transform.position);
        transform.LookAt(player.transform); // Ensure the monster looks at the player
        moving = false;
    }

    private void TriggerJumpscare()
    {
        if (!jumpscareTriggered)
        {
            jumpscareTriggered = true;


            // Store the original player rotation
            originalPlayerRotation = player.transform.rotation;



            // Trigger the jumpscare animation
            monsterAnimator.SetTrigger("Jumpscare");

            // Disable player's movement and camera control during jumpscare
            DisablePlayerMovementAndCamera();


            // Start coroutine to handle death and reset after jumpscare
            StartCoroutine(HandleDeathAfterJumpscare());
        }
    }

    private IEnumerator HandleDeathAfterJumpscare()
    {
        float jumpscareDuration = 2f; // Set this to match the length of your jumpscare animation
        float timer = 0f;

        // During the jumpscare, make the player's body rotate to face the monster and tilt back 30 degrees
        while (timer < jumpscareDuration)
        {
            // Rotate the player to face the monster and tilt backwards by 30 degrees
            Vector3 directionToMonster = transform.position - player.transform.position;
            directionToMonster.y = 0; // Keep player upright when rotating

            // Create rotation towards the monster
            Quaternion lookRotation = Quaternion.LookRotation(directionToMonster);
            // Tilt the player's rotation backwards by 30 degrees
            Quaternion tiltBackRotation = Quaternion.Euler(-30f, lookRotation.eulerAngles.y, lookRotation.eulerAngles.z);

            // Apply the rotation gradually
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, tiltBackRotation, Time.deltaTime * 5f);

            timer += Time.deltaTime;
            yield return null;
        }

        // Mark the player as dead
        playerActions.dead = true;

        // Teleport the monster back to its initial position after the jumpscare
        TeleportToInitialPosition();

        jumpscareNoise.Stop();


        // Reset patrol or idle state
        ResetPatrol();

        // Re-enable player movement and camera control after the player dies
        EnablePlayerMovementAndCamera();

        // Reset the player's rotation to the original
        player.transform.rotation = originalPlayerRotation;

        // Reset the jumpscare trigger in the animator
        monsterAnimator.ResetTrigger("Jumpscare");
        monsterAnimator.SetBool("isIdle", true);
        monsterAnimator.SetBool("isMoving", false);

        jumpscareTriggered = false; // Reset the jumpscare trigger flag
    }







    private void DisablePlayerMovementAndCamera()
    {
        // Disable the player movement script
        var playerMovement = player.GetComponent<InputManager>();
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        // Disable the player's camera control script (e.g., mouse or joystick input)
        var cameraController = player.GetComponent<PlayerLook>();
        if (cameraController != null)
        {
            cameraController.enabled = false;
        }
    }

    private void EnablePlayerMovementAndCamera()
    {
        // Re-enable the player movement script after jumpscare
        var playerMovement = player.GetComponent<InputManager>();
        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }

        // Re-enable the camera control after the jumpscare
        var cameraController = player.GetComponent<PlayerLook>();
        if (cameraController != null)
        {
            cameraController.enabled = true;
        }
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
*/
/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class monsterAgent : MonoBehaviour
{
    private GameObject player;
    private NavMeshAgent agent;
    private Quaternion originalCameraRotation; // Store the original rotation of the camera
    private Camera playerCamera; // Reference to the player's camera

    public bool moving;
    public float attackRange, sightRange;
    public static bool inAttackRange, inSightRange;
    private Animator monsterAnimator;
    public LayerMask whatIsGround, whatIsPlayer;

    [SerializeField] private AudioSource jumpscareNoise;

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

        // Dynamically find the player's camera (assuming the camera is a child of the player)
        playerCamera = player.GetComponentInChildren<Camera>();

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
        monsterAnimator.SetBool("isMoving", true);
        monsterAnimator.SetBool("isIdle", false);
    }

    private void Attack()
    {
        TriggerJumpscare();
        jumpscareNoise.Play();

        // Attack logic (stop moving and trigger jumpscare)
        agent.SetDestination(transform.position);
        transform.LookAt(player.transform); // Ensure the monster looks at the player
        moving = false;
    }

    private void TriggerJumpscare()
    {
        if (!jumpscareTriggered)
        {
            jumpscareTriggered = true;

            // Store the original camera rotation
            originalCameraRotation = playerCamera.transform.rotation;

            // Trigger the jumpscare animation
            monsterAnimator.SetTrigger("Jumpscare");

            // Disable player's movement and camera control during jumpscare
            DisablePlayerMovementAndCamera();

            // Start coroutine to handle death and reset after jumpscare
            StartCoroutine(HandleDeathAfterJumpscare());
        }
    }

    private IEnumerator HandleDeathAfterJumpscare()
    {
        float jumpscareDuration = 2f; // Set this to match the length of your jumpscare animation
        float timer = 0f;

        // Rotate the player to face the monster
        Vector3 directionToMonster = transform.position - player.transform.position;
        directionToMonster.y = 0; // Keep player upright when rotating
        Quaternion targetPlayerRotation = Quaternion.LookRotation(directionToMonster);

        // Tilt the camera backwards by 30 degrees after the player faces the monster
        Quaternion fixedCameraTilt = Quaternion.Euler(-30f, targetPlayerRotation.eulerAngles.y, 0f);

        // First, rotate the player to face the monster
        while (timer < jumpscareDuration) // First half of the jumpscare duration
        {
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, targetPlayerRotation, Time.deltaTime * 5f);
            playerCamera.transform.rotation = Quaternion.Slerp(playerCamera.transform.rotation, fixedCameraTilt, Time.deltaTime * 5f);
            timer += Time.deltaTime;
            yield return null;
        }


        // Mark the player as dead
        playerActions.dead = true;

        // Teleport the monster back to its initial position after the jumpscare
        TeleportToInitialPosition();

        jumpscareNoise.Stop();

        // Reset patrol or idle state
        ResetPatrol();

        // Re-enable player movement and camera control after the player dies
        EnablePlayerMovementAndCamera();

        // Reset the camera's rotation to the original
        playerCamera.transform.rotation = originalCameraRotation;

        // Reset the jumpscare trigger in the animator
        monsterAnimator.ResetTrigger("Jumpscare");
        monsterAnimator.SetBool("isIdle", true);
        monsterAnimator.SetBool("isMoving", false);

        jumpscareTriggered = false; // Reset the jumpscare trigger flag
    }

    private void DisablePlayerMovementAndCamera()
    {
        // Disable the player movement script
        var playerMovement = player.GetComponent<InputManager>();
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        // Disable the player's camera control script (e.g., mouse or joystick input)
        var cameraController = player.GetComponent<PlayerLook>();
        if (cameraController != null)
        {
            cameraController.enabled = false;
        }
    }

    private void EnablePlayerMovementAndCamera()
    {
        // Re-enable the player movement script after jumpscare
        var playerMovement = player.GetComponent<InputManager>();
        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }

        // Re-enable the camera control after the jumpscare
        var cameraController = player.GetComponent<PlayerLook>();
        if (cameraController != null)
        {
            cameraController.enabled = true;
        }
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
/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class monsterAgent : MonoBehaviour
{
    private GameObject player;
    private NavMeshAgent agent;
    private Quaternion originalCameraRotation; // Store the original rotation of the camera
    private Camera playerCamera; // Reference to the player's camera

    public bool moving;
    public float attackRange, sightRange;
    public static bool inAttackRange, inSightRange;
    private Animator monsterAnimator;
    public LayerMask whatIsGround, whatIsPlayer;

    [SerializeField] private AudioSource jumpscareNoise;

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

        // Dynamically find the player's camera (assuming the camera is a child of the player)
        playerCamera = player.GetComponentInChildren<Camera>();

        initialPosition = agent.transform.position; // Store initial position

        // Set stopping distance to help with precision at patrol points
        agent.stoppingDistance = 0.5f;
    }

    public void Update()
    {
        // Debugging to check the animator state
*//*        Debug.Log("isMoving: " + monsterAnimator.GetBool("isMoving"));
        Debug.Log("isIdle: " + monsterAnimator.GetBool("isIdle"));*//*

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
        monsterAnimator.SetBool("isMoving", true);
        monsterAnimator.SetBool("isIdle", false);
    }

    private void Attack()
    {
        TriggerJumpscare();
        jumpscareNoise.Play();

        // Attack logic (stop moving and trigger jumpscare)
        agent.SetDestination(transform.position);
        transform.LookAt(player.transform); // Ensure the monster looks at the player
        moving = false;
    }

    private void TriggerJumpscare()
    {
        if (!jumpscareTriggered)
        {
            jumpscareTriggered = true;

            // Store the original camera rotation
            originalCameraRotation = playerCamera.transform.rotation;

            // Trigger the jumpscare animation
            monsterAnimator.SetTrigger("Jumpscare");

            // Disable player's movement and camera control during jumpscare
            DisablePlayerMovementAndCamera();

            // Start coroutine to handle death and reset after jumpscare
            StartCoroutine(HandleDeathAfterJumpscare());
        }
    }

    private IEnumerator HandleDeathAfterJumpscare()
    {
        float jumpscareDuration = 2f; // Set this to match the length of your jumpscare animation
        float timer = 0f;

        // Rotate the player to face the monster
        Vector3 directionToMonster = transform.position - player.transform.position;
        directionToMonster.y = 0; // Keep player upright when rotating
        Quaternion targetPlayerRotation = Quaternion.LookRotation(directionToMonster);

        // Tilt the camera backwards by 30 degrees after the player faces the monster
        Quaternion fixedCameraTilt = Quaternion.Euler(-30f, targetPlayerRotation.eulerAngles.y, 0f);

        // First, rotate the player to face the monster
        while (timer < jumpscareDuration) // First half of the jumpscare duration
        {
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, targetPlayerRotation, Time.deltaTime * 5f);
            playerCamera.transform.rotation = Quaternion.Slerp(playerCamera.transform.rotation, fixedCameraTilt, Time.deltaTime * 5f);
            timer += Time.deltaTime;
            yield return null;
        }


        // Mark the player as dead
        playerActions.dead = true;

        // Teleport the monster back to its initial position after the jumpscare
        TeleportToInitialPosition();

        jumpscareNoise.Stop();

        // Reset patrol or idle state
        ResetPatrol();

        // Re-enable player movement and camera control after the player dies
        EnablePlayerMovementAndCamera();

        // Reset the camera's rotation to the original
        playerCamera.transform.rotation = originalCameraRotation;

        // Reset the jumpscare trigger in the animator
        monsterAnimator.ResetTrigger("Jumpscare");
        monsterAnimator.SetBool("isIdle", true);
        monsterAnimator.SetBool("isMoving", false);

        jumpscareTriggered = false; // Reset the jumpscare trigger flag
    }

    private void DisablePlayerMovementAndCamera()
    {
        // Disable the player movement script
        var playerMovement = player.GetComponent<InputManager>();
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        // Disable the player's camera control script (e.g., mouse or joystick input)
        var cameraController = player.GetComponent<PlayerLook>();
        if (cameraController != null)
        {
            cameraController.enabled = false;
        }
    }

    private void EnablePlayerMovementAndCamera()
    {
        // Re-enable the player movement script after jumpscare
        var playerMovement = player.GetComponent<InputManager>();
        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }

        // Re-enable the camera control after the jumpscare
        var cameraController = player.GetComponent<PlayerLook>();
        if (cameraController != null)
        {
            cameraController.enabled = true;
        }
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
        agent.transform.position = initialPosition;
    }

    public void SetPatrolPoint(Vector3 newPatrolPoint)
    {
        walkPoint = newPatrolPoint;
        walkPointSet = true; // Mark that a walk point is now set
        waiting = false; // Stop waiting if it was waiting
        moving = true; // Start moving toward the new patrol point
        monsterAnimator.SetBool("isMoving", true);
        monsterAnimator.SetBool("isIdle", false);

        // Set the agent's destination to the new patrol point
        agent.SetDestination(walkPoint);
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
using UnityEngine;
using UnityEngine.AI;

public class monsterAgent : MonoBehaviour
{
    private GameObject player;
    private NavMeshAgent agent;
    private Quaternion originalCameraRotation;
    private Camera playerCamera;

    public bool moving;
    public float attackRange, sightRange;
    public static bool inAttackRange, inSightRange;
    private Animator monsterAnimator;
    public LayerMask whatIsGround, whatIsPlayer;

    [SerializeField] private AudioSource jumpscareNoise;

    private Vector3 initialPosition; // The starting position of the monster
    public bool isMovedByTool = false; // Flag to track if moved by the tool

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

        playerCamera = player.GetComponentInChildren<Camera>();
        initialPosition = agent.transform.position; // Store initial position

        agent.stoppingDistance = 0.5f; // Set stopping distance for patrol points
    }

    public void Update()
    {
        if (playerActions.dead)
        {
            // Stop all behavior if the player is dead
            ResetPatrol();
            return;
        }

        moving = agent.velocity.magnitude > 0.1f;
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
            agent.SetDestination(walkPoint);

            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!waiting)
                {
                    waitTimer = 0f;
                    waiting = true;
                    moving = false;
                    monsterAnimator.SetBool("isIdle", true);
                    monsterAnimator.SetBool("isMoving", false);
                }
                else
                {
                    waitTimer += Time.deltaTime;
                    if (waitTimer >= waitTime)
                    {
                        waiting = false;
                        walkPointSet = false;
                    }
                }
            }
            else
            {
                moving = true;
                monsterAnimator.SetBool("isMoving", true);
                monsterAnimator.SetBool("isIdle", false);
            }
        }
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointsRange, walkPointsRange);
        float randomX = Random.Range(-walkPointsRange, walkPointsRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.transform.position);
        moving = true;
        monsterAnimator.SetBool("isMoving", true);
        monsterAnimator.SetBool("isIdle", false);
    }

    private void Attack()
    {
        TriggerJumpscare();
        jumpscareNoise.Play();

        agent.SetDestination(transform.position);
        transform.LookAt(player.transform);
        moving = false;
    }

    private void TriggerJumpscare()
    {
        if (!jumpscareTriggered)
        {
            jumpscareTriggered = true;
            originalCameraRotation = playerCamera.transform.rotation;
            monsterAnimator.SetTrigger("Jumpscare");
            DisablePlayerMovementAndCamera();
            StartCoroutine(HandleDeathAfterJumpscare());
        }
    }

    private IEnumerator HandleDeathAfterJumpscare()
    {
        float jumpscareDuration = 2f;
        float timer = 0f;

        Vector3 directionToMonster = transform.position - player.transform.position;
        directionToMonster.y = 0;
        Quaternion targetPlayerRotation = Quaternion.LookRotation(directionToMonster);
        Quaternion fixedCameraTilt = Quaternion.Euler(-30f, targetPlayerRotation.eulerAngles.y, 0f);

        while (timer < jumpscareDuration)
        {
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, targetPlayerRotation, Time.deltaTime * 5f);
            playerCamera.transform.rotation = Quaternion.Slerp(playerCamera.transform.rotation, fixedCameraTilt, Time.deltaTime * 5f);
            timer += Time.deltaTime;
            yield return null;
        }

        playerActions.dead = true;

        TeleportToInitialPosition();

        jumpscareNoise.Stop();

        ResetPatrol();

        lureTool tool = FindObjectOfType<lureTool>();
        tool.ResetAllMonsters(); // Reset all monsters moved by the tool

        EnablePlayerMovementAndCamera();

        playerCamera.transform.rotation = originalCameraRotation;
        monsterAnimator.ResetTrigger("Jumpscare");
        monsterAnimator.SetBool("isIdle", true);
        monsterAnimator.SetBool("isMoving", false);

        jumpscareTriggered = false;
    }

    public void SetPatrolPoint(Vector3 newPatrolPoint)
    {
        walkPoint = newPatrolPoint;
        walkPointSet = true;
        waiting = false;
        moving = true;
        monsterAnimator.SetBool("isMoving", true);
        monsterAnimator.SetBool("isIdle", false);
        agent.SetDestination(walkPoint);
    }

    public void TeleportToInitialPosition()
    {
        agent.Warp(initialPosition); // Warp back to the original position
        ResetPatrol();
    }

    private void ResetPatrol()
    {
        agent.ResetPath();
        walkPointSet = false;
        waiting = false;
        moving = false;
        monsterAnimator.SetBool("isIdle", true);
        monsterAnimator.SetBool("isMoving", false);
    }

    private void DisablePlayerMovementAndCamera()
    {
        var playerMovement = player.GetComponent<InputManager>();
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        var cameraController = player.GetComponent<PlayerLook>();
        if (cameraController != null)
        {
            cameraController.enabled = false;
        }
    }

    private void EnablePlayerMovementAndCamera()
    {
        var playerMovement = player.GetComponent<InputManager>();
        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }

        var cameraController = player.GetComponent<PlayerLook>();
        if (cameraController != null)
        {
            cameraController.enabled = true;
        }
    }
}













