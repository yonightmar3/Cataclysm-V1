using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mazeTrigger3 : MonoBehaviour
{
    public GameObject collapse1;
    public GameObject collapse2;
    public GameObject collapse3;

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.tag=="Player")
        {
            collapse1.SetActive(false);
            collapse2.SetActive(false);
            collapse3.SetActive(false);
        }
    }
}
