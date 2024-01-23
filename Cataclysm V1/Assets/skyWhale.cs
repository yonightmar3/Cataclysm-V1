using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skyWhale : MonoBehaviour
{
    public GameObject skyWhaleObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        dragonFlight.readyForFlight = true;
        //StartCoroutine(skyWhaleFlight());
    }

/*    IEnumerator skyWhaleFlight()
    {
        skyWhaleObject.SetActive(true);
        yield return new WaitForSeconds(30);
        skyWhaleObject.SetActive(false);
    }*/
}
