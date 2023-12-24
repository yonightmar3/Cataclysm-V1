using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class footstepController : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource footstepsDungeon;
    public AudioSource footstepsDungeon2;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (InputManager.disabled == false)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                //if (SceneManager.GetActiveScene().name == "Dungeon"){
                //Debug.Log("moving");
                footstepsDungeon.enabled = true;
                StartCoroutine("footstepsAudio");
            }
            else
            {
                footstepsDungeon.enabled = false;
                
            }
        }
        else footstepsDungeon.enabled = false;
    }

    IEnumerator footstepsAudio()
    {
        yield return new WaitForSeconds(0.75f);
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            //if (SceneManager.GetActiveScene().name == "Dungeon"){
            //Debug.Log("moving");
            footstepsDungeon2.enabled = true;
        }
        else
        {
            footstepsDungeon2.enabled = false;

        }
    }


}


