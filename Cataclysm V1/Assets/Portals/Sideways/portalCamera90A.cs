using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portalCamera90A : MonoBehaviour
{
    public Transform playerCamera;
    public Transform portal;
    public Transform otherPortal;

    // Update is called once per frame

    void Update()
    {
        Vector3 playerOffsetFromPortal = playerCamera.position - otherPortal.position;

        // Rotate player offset based on the difference in portal rotations
        playerOffsetFromPortal = Quaternion.Euler(0, angularDifferenceBetweenPortalRotations, 0) * playerOffsetFromPortal;

        // Apply rotation and position
        transform.position = portal.position + playerOffsetFromPortal;
        transform.rotation = portal.rotation * Quaternion.Inverse(otherPortal.rotation) * playerCamera.rotation;
    }

    float angularDifferenceBetweenPortalRotations
    {
        get
        {
            float angularDifference = Quaternion.Angle(portal.rotation, otherPortal.rotation);
            float sign = Mathf.Sign(Vector3.Dot(portal.up, otherPortal.position - portal.position));
            return angularDifference * sign;
        }
    }
}