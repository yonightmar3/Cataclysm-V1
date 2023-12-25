using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portalCamera180B : MonoBehaviour
{
    public Transform playerCamera;
    public Transform portal;
    public Transform otherPortal;

    void LateUpdate()
    {
        Vector3 playerOffsetFromPortal = playerCamera.position - portal.position;

        // Calculate the new position of the camera relative to the other portal
        Vector3 newCameraPosition = otherPortal.position + playerOffsetFromPortal;

        // Set the new position of the camera
        transform.position = newCameraPosition;

        // Calculate the rotation difference between the two portals
        float angularDifferenceBetweenPortalRotations = Quaternion.Angle(portal.rotation, otherPortal.rotation);
        Quaternion portalRotationalDifference = Quaternion.AngleAxis(angularDifferenceBetweenPortalRotations, Vector3.up);

        // Calculate the new camera direction based on the rotation difference
        Vector3 newCameraDirection = portalRotationalDifference * playerCamera.forward;

        // Set the new rotation of the camera
        transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);
    }
}
