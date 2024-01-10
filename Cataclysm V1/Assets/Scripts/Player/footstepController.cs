using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class footstepController : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource footstepsDungeon;
    public AudioSource footstepsDungeon2;
    
    // Update is called once per frame
    void Update()
    {
        if (InputManager.disabled == false)
        {

            //Debug.Log(portalTeleporterScript);

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                //if (SceneManager.GetActiveScene().name == "Dungeon"){
                //Debug.Log("moving");
                StartCoroutine("footstepsAudio");
            }
            else
            {
                StopCoroutine("footstepsAudio");
                footstepsDungeon.enabled = false;
                footstepsDungeon2.enabled = false;



            }
        }
        else footstepsDungeon.enabled = false;
    }

    IEnumerator footstepsAudio()
    {

            footstepsDungeon.enabled = true;
            yield return new WaitForSeconds(0.75f);
            footstepsDungeon2.enabled = true;
            yield return new WaitForSeconds(0.75f);


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other!=null)
        {
            StopCoroutine("footstepsAudio");
            footstepsDungeon.enabled = false;
            footstepsDungeon2.enabled = false;

        }
    }


}







