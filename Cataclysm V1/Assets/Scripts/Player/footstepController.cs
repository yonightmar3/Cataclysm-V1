using System.Collections;
using UnityEngine;

public class footstepController : MonoBehaviour
{
    public AudioClip[] sounds; // Array to hold your AudioClips
    private AudioSource audioSource;
    private WaitForSeconds waitTime = new WaitForSeconds(.5f);
    private Coroutine footstepCoroutine; // Reference to the coroutine

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (InputManager.disabled == false)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                if (footstepCoroutine == null)
                {
                    footstepCoroutine = StartCoroutine(PlayRandomSounds());
                }
            }
            else
            {
                if (footstepCoroutine != null)
                {
                    StopCoroutine(footstepCoroutine);
                    footstepCoroutine = null;
                }
            }
        }
    }

    IEnumerator PlayRandomSounds()
    {
        while (true)
        {
            int randomIndex = Random.Range(0, sounds.Length);
            AudioClip selectedSound = sounds[randomIndex];
            audioSource.volume = Random.Range(0.02f, 0.05f);
            audioSource.pitch = Random.Range(0.8f, 1.2f);
            audioSource.PlayOneShot(selectedSound);
            yield return waitTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null && footstepCoroutine != null)
        {
            StopCoroutine(footstepCoroutine);
            footstepCoroutine = null;
        }
    }
}
