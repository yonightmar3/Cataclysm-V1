using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cultistMover : MonoBehaviour
{
    private Vector3 initialPosition;

    public int speed = 5;

    private void Start()
    {
        initialPosition = transform.position;
    }
    private void Update()
    {
        float distanceMoved = Vector3.Distance(initialPosition, transform.position);

        Vector3 movement = new Vector3(-1f, 0f, 0f);
        transform.Translate(movement * speed * Time.deltaTime);

        if(distanceMoved > 25f)
        {
            Destroy(gameObject);
        }
    }

}
