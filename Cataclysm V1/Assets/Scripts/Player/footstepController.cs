using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class footstepController : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource footstepsDungeon;
    public AudioSource footstepsDungeon2;
    public AudioSource footstepsDungeon3;
    public AudioSource footstepsDungeon4;

    public AudioClip[] sounds; // Array to hold your AudioClips
    private AudioSource audioSource;
    private WaitForSeconds waitTime = new WaitForSeconds(1f);

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Call the coroutine to play random sounds
        
    }
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
                //StartCoroutine("footstepsAudio");
                StartCoroutine(PlayRandomSounds());
            }
            else
            {
                StopCoroutine("footstepsAudio");
                footstepsDungeon.enabled = false;
                footstepsDungeon2.enabled = false;
                footstepsDungeon3.enabled = false;
                footstepsDungeon4.enabled = false;




            }
        }
        else footstepsDungeon.enabled = false;
    }

    IEnumerator footstepsAudio()
    {
            footstepsDungeon.enabled = true;
            footstepsDungeon3.enabled = false;
            yield return new WaitForSeconds(0.75f);
            footstepsDungeon2.enabled = true;
            footstepsDungeon4.enabled = false;
            yield return new WaitForSeconds(0.75f);
            footstepsDungeon3.enabled = true;
            footstepsDungeon.enabled = false;
            yield return new WaitForSeconds(.75f);
            footstepsDungeon4.enabled = true;
            footstepsDungeon2.enabled = true;
            yield return new WaitForSeconds(0.75f);




    }
    IEnumerator PlayRandomSounds()
    {
        while (true)
        {
            // Pick a random AudioClip from the array
            int randomIndex = Random.Range(0, sounds.Length);
            AudioClip selectedSound = sounds[randomIndex];

            // Play the selected sound using the AudioSource
            audioSource.PlayOneShot(selectedSound);

            // Wait for a second
            yield return waitTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other!=null)
        {
            StopCoroutine("footstepsAudio");


        }
    }


}







