using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragonFlight : MonoBehaviour
{
    public static bool readyForFlight;
    public Animator skyWhaleAnimator;
    public AudioSource whaleSounds;
    // Start is called before the first frame update
    void Start()
    {

    }

    public Vector3 direction = Vector3.forward; // Change this vector to the desired movement direction
    public float speed = 5f; // Adjust the speed as needed

    void Update()
    {
        if (readyForFlight)
        {
            transform.Translate(direction * speed * Time.deltaTime);
            skyWhaleAnimator.SetTrigger("beginFlight");
            whaleSounds.enabled = true;
            StartCoroutine(wait());
        }
        // Move the object in the specified direction
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(30);
        Destroy(this.gameObject);
    }
}
