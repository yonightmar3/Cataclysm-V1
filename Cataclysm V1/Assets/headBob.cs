using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headBob : MonoBehaviour
{
    [Header("Head Bob Settings")]
    public float bobFrequency = 1.5f; // How fast the bobbing happens
    public float bobAmplitude = 0.05f; // How much the head moves up and down
    public float bobSideAmplitude = 0.03f; // How much the head moves side to side
    public float bobSmoothing = 5f; // Smoothing for returning the camera to its default position

    private float bobTimer = 0f;
    private Vector3 originalCameraPosition;

    void Start()
    {
        // Store the original position of the camera
        originalCameraPosition = transform.localPosition;
    }

    void Update()
    {
        // Check if the player is moving using input from the Input Manager
        if (IsPlayerMoving())
        {
            // If moving, apply the head bob effect
            BobCamera();
        }
        else
        {
            // If not moving, smoothly return the camera to its original position
            ResetCameraPosition();
        }
    }

    bool IsPlayerMoving()
    {
        // Check if any movement input is detected
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // If there's significant movement input, return true
        return Mathf.Abs(horizontalInput) > 0.1f || Mathf.Abs(verticalInput) > 0.1f;
    }

    void BobCamera()
    {
        // Increase the bob timer over time, keeping it within a reasonable range using Mathf.Repeat
        bobTimer += Time.deltaTime * bobFrequency;
        bobTimer = Mathf.Repeat(bobTimer, Mathf.PI * 2); // Keep it between 0 and 2π (full sine wave cycle)

        // Calculate the new camera position with the bobbing effect
        float verticalBob = Mathf.Sin(bobTimer) * bobAmplitude;
        float horizontalBob = Mathf.Cos(bobTimer * 2f) * bobSideAmplitude;

        // Apply the new position
        transform.localPosition = new Vector3(originalCameraPosition.x + horizontalBob, originalCameraPosition.y + verticalBob, originalCameraPosition.z);
    }

    void ResetCameraPosition()
    {
        // Smoothly reset the camera back to the original position
        transform.localPosition = Vector3.Lerp(transform.localPosition, originalCameraPosition, Time.deltaTime * bobSmoothing);
    }
}
