using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gabriel : MonoBehaviour
{
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            // Calculate the direction from the object to the player
            Vector3 directionToPlayer = player.position - transform.position;
            directionToPlayer.y = 0f; // Ignore the Y component

            // Rotate the object to look at the player only on the Y-axis
            transform.rotation = Quaternion.LookRotation(directionToPlayer.normalized, Vector3.up);
        }
    }
}
