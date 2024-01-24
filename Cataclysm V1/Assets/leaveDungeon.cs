using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class leaveDungeon : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        eventManagerMainMap.returnedFromDungeon = true;
        SceneManager.LoadScene("MainMap");
    }
}
