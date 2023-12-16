using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{

    public Camera cam;
    private float xRotation = 0f;

    public float xSensitivity = 30f;
    public float ySensitivity = 30f;

    public RaycastHit HitInfoFar { get; private set; }
    public RaycastHit HitInfoClose { get; private set; }


    private void Start()
    {
        Cursor.visible = false;
    }

    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        xRotation -= (mouseY * Time.deltaTime) * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    }

    /*    private void Update()
        {
            Vector3 cameraDirection = cam.transform.forward;
            //Physics.Raycast(cam.transform.position, cameraDirection, out RaycastHit hitInfo, 20f);
        }*/

    private void Update()
    {
        Vector3 cameraDirection = cam.transform.forward;

        // Declare a local RaycastHit variable
        RaycastHit hitFar;
        RaycastHit hitClose;


        // Perform the raycast and assign the result to the local variable
        if (Physics.Raycast(cam.transform.position, cameraDirection, out hitFar, 20f))
        {
            // Assign the local variable to the class property
            HitInfoFar = hitFar;
        }
        else
        {
            // If no hit, set the HitInfo property to null or some default value
            HitInfoFar = new RaycastHit(); // You might want to set a default value based on your requirements
        }

        if (Physics.Raycast(cam.transform.position, cameraDirection, out hitClose, 3f))
        {
            // Assign the local variable to the class property
            HitInfoClose = hitFar;
        }
        else
        {
            // If no hit, set the HitInfo property to null or some default value
            HitInfoClose = new RaycastHit(); // You might want to set a default value based on your requirements
        }
    }


}
