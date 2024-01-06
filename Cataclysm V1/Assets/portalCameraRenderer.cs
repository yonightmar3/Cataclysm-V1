using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portalCameraRenderer : MonoBehaviour
{
    public List<Camera> cameras = new List<Camera>();
    public GameObject AlchemyA;
    public GameObject AlchemyB;

    private void Start()
    {
        foreach (Camera camera in cameras)
        {
            camera.gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            foreach (Camera camera in cameras)
            {
                camera.gameObject.SetActive(true);
            }

        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            foreach (Camera camera in cameras)
            {
                camera.gameObject.SetActive(false);
            }
        }
    }
}
