using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System.Collections;

public class closestEnemy : MonoBehaviour
{
    private string enemyTag = "StarvedAgent"; // Tag of your enemy GameObjects
    private string playerTag = "Player"; // Tag of your player GameObject
    private Animator closestEnemyAnimator;

    public static bool dying;

    private GameObject[] enemies;
    private Transform playerTransform;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag(playerTag).transform;
        enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        
    }

    void Update()
    {
        if (dying == false)
        {


            GameObject closestEnemy = FindClosestEnemy();
            float distanceToPlayer = Vector3.Distance(closestEnemy.transform.position, playerTransform.position);

            NavMeshAgent closestEnemyAgent = closestEnemy.GetComponent<NavMeshAgent>();
            // Do something with closestEnemy
            closestEnemyAnimator = closestEnemy.GetComponent<Animator>();
            if (distanceToPlayer <= 2.5f && eventManager.keyObtained == true)
            {
                for (int i = 0; i < enemies.Length; i++)
                {
                    if (enemies[i] != closestEnemy)
                    {
                        enemies[i].SetActive(false);
                    }
                    StartCoroutine(starvedJumpscare());
                }
                closestEnemyAnimator.SetTrigger("attackRange");
                closestEnemyAgent.isStopped = true;
            }
            if (transform != null && starvedAgent.jumpscared == true)
            {
                InputManager.disabled = true;
                // Get the direction to the enemy
                Vector3 directionToEnemy = playerTransform.position - closestEnemy.transform.position;
                directionToEnemy.y = 0f; // Ignore height difference

                if (directionToEnemy != Vector3.zero)
                {
                    // Calculate the rotation needed to look at the enemy
                    Quaternion targetRotation = Quaternion.LookRotation(-directionToEnemy);

                    // Slerp to smoothly rotate towards the enemy on the y-axis
                    playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, targetRotation, 10f * Time.deltaTime);

                    //footsteps.SetActive(false);

                    //StartCoroutine(jumpscare());
                }
            }
        }


    }

    GameObject FindClosestEnemy()
    {
        GameObject closest = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(playerTransform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closest = enemy;
                closestDistance = distance;
            }
        }

        //closest.SetActive(false);

        return closest;
    }

    IEnumerator starvedJumpscare()
    {
        yield return new WaitForSeconds(1.4f);
        //deathScreen.SetActive(true);
        playerActions.dead = true;

    }
}
