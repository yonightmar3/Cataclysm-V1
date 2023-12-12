using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class footstepController : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource footstepsDungeon;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)){
            //if (SceneManager.GetActiveScene().name == "Dungeon"){
            //Debug.Log("moving");
                footstepsDungeon.enabled = true;
            }
            else
            {
                footstepsDungeon.enabled = false;
            }
        }
        
    }


