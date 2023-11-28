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
    // Start is called before the first frame update
    void Start()
    {
        monster = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerSeen)
        {
            monster.destination = player.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        deathScreen.SetActive(true);
    }

}
