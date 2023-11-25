using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cultistMover : MonoBehaviour
{
    public int speed = 5;

    private void Update()
    {
        Vector3 movement = new Vector3(-1f, 0f, 0f);
        transform.Translate(movement * speed * Time.deltaTime);
    }

}
