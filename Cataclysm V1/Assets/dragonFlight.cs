using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragonFlight : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public Vector3 direction = Vector3.forward; // Change this vector to the desired movement direction
    public float speed = 5f; // Adjust the speed as needed

    void Update()
    {
        // Move the object in the specified direction
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
