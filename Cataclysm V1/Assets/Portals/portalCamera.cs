/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portalCamera : MonoBehaviour
{

	public Transform playerCamera;
	public Transform portal;
	public Transform otherPortal;

	// Update is called once per frame
	
	void LateUpdate()
	{
		Vector3 playerOffsetFromPortal = playerCamera.position - otherPortal.position;
		transform.position = portal.position + playerOffsetFromPortal;

		float angularDifferenceBetweenPortalRotations = Quaternion.Angle(portal.rotation, otherPortal.rotation);

		Quaternion portalRotationalDifference = Quaternion.AngleAxis(angularDifferenceBetweenPortalRotations, Vector3.up);
		Vector3 newCameraDirection = portalRotationalDifference * playerCamera.forward;
		transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);
	}
}*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portalCamera : MonoBehaviour
{
    public Transform playerCamera;
    public Transform portal;
    public Transform otherPortal;

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 playerOffsetFromPortal = playerCamera.position - otherPortal.position;

        // Swap X and Z components in the position calculation
        transform.position = new Vector3(portal.position.x + playerOffsetFromPortal.z, playerCamera.position.y, portal.position.z + playerOffsetFromPortal.x);

        float angularDifferenceBetweenPortalRotations = Quaternion.Angle(portal.rotation, otherPortal.rotation);

        Quaternion portalRotationalDifference = Quaternion.AngleAxis(angularDifferenceBetweenPortalRotations, Vector3.up);
        Vector3 newCameraDirection = portalRotationalDifference * playerCamera.forward;

        // Swap X and Z components in the rotation calculation
        //transform.rotation = Quaternion.LookRotation(new Vector3(newCameraDirection.z, newCameraDirection.y, newCameraDirection.x + 90), Vector3.up);
        transform.rotation = Quaternion.LookRotation(new Vector3(newCameraDirection.z, newCameraDirection.y, newCameraDirection.x)) * Quaternion.Euler(0, 90, 0);

    }
}






