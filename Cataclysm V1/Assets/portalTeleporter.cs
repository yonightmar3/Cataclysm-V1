using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portalTeleporter : MonoBehaviour
{

	public Transform player;
	public Transform reciever;

	public Transform locationA;
	public Transform locationB;

	private bool playerIsOverlapping = false;

	// Update is called once per frame
	void Update()
	{
		if (playerIsOverlapping)
		{
			Vector3 portalToPlayer = player.position - transform.position;
			float dotProduct = Vector3.Dot(transform.up, portalToPlayer);

			// If this is true: The player has moved across the portal
			if (dotProduct < 0f)
			{
				Debug.Log("please");
				// Teleport him!
				float rotationDiff = -Quaternion.Angle(transform.rotation, reciever.rotation);
				rotationDiff += 180;
				player.Rotate(Vector3.up, rotationDiff);

				Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;

				StartCoroutine("teleport");
				//teleport();
				//player.position = reciever.position + positionOffset;

				playerIsOverlapping = false;
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			Debug.Log("triggered");
			playerIsOverlapping = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			playerIsOverlapping = false;
		}
	}

	IEnumerator teleport()
	{
		InputManager.disabled = true;
		yield return new WaitForSeconds(0.01f);
		player.transform.position = new Vector3(locationB.position.x, player.position.y, locationB.position.z);
		Debug.Log("player teleported");
		yield return new WaitForSeconds(0.01f);
		InputManager.disabled = false;
	}
}