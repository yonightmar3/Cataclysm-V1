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
        Vector3 playerOffsetFromPortal = playerCamera.position - otherPortal.position;
        transform.position = portal.position + playerOffsetFromPortal;

        float angularDifferenceBetweenPortalRotations = Quaternion.Angle(portal.rotation, otherPortal.rotation);
        Quaternion portalRotationalDifference = Quaternion.AngleAxis(angularDifferenceBetweenPortalRotations, Vector3.up);

        // Modify the camera direction by negating the Z component
        Vector3 newCameraDirection = portalRotationalDifference * playerCamera.forward;
        newCameraDirection.z *= -1f;

        transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);
    }
}
