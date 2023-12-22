using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class starvedAgent : MonoBehaviour
{
    private NavMeshAgent starved;
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
        if(distanceToPlayer <= 15f/* && distanceToPlayer > 2f*/)
        {
            starved.destination = player.position;
            starvedAnimator.SetTrigger("running");
        }
        if (distanceToPlayer <= 2.5f)
        {
            starvedAnimator.SetTrigger("attackRange");
            if (distanceToPlayer <= 1f)
            {
                starved.isStopped = true;
            }
        }
        //else starvedAnimator.SetTrigger("running");
        
    }
}
