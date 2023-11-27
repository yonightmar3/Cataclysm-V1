using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class mazeDweller : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent monster;
    // Start is called before the first frame update
    void Start()
    {
        monster = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        monster.destination = player.position;
    }
}
