using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cultistMover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        Vector3 movement = new Vector3(-1f, 0f, 0f);
        transform.Translate(movement * 5 * Time.deltaTime);
    }

}
