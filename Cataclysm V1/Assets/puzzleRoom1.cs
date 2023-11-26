using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzleRoom1 : MonoBehaviour
{
    [SerializeField] private Animator descendingDoor;

    public GameObject puzzle1Door;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            descendingDoor.SetTrigger("puzzle1wallDescend");
        }
    }
}
