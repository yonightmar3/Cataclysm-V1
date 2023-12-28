using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portalTeleporter : MonoBehaviour
{

	public Transform player;
	public Transform reciever;
	public bool teleported;

	public GameObject otherPortal;

	private Vector3 positionOffset;

	private bool playerIsOverlapping = false;
	public void Update()
	{
		if (playerIsOverlapping)
		{
			Vector3 portalToPlayer = player.position - transform.position;
			float dotProduct = Vector3.Dot(transform.up, portalToPlayer);

			if (dotProduct < 0f)
			{
				float rotationDiff = -Quaternion.Angle(transform.rotation, reciever.rotation);
				rotationDiff += 180;
				player.Rotate(Vector3.up, rotationDiff);

				positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;

				StartCoroutine("teleport");



				teleported = true;

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
				//otherPortal.SetActive(false);
			}
		}

		void OnTriggerExit(Collider other)
		{
			if (other.tag == "Player")
			{
				//otherPortal.SetActive(true);
				playerIsOverlapping = false;
			}
		}

		IEnumerator teleport()
		{
			InputManager.disabled = true;
			yield return new WaitForSeconds(0.01f);
			player.transform.position = new Vector3(reciever.position.x, reciever.position.y, reciever.position.z) + positionOffset;
			Debug.Log("player teleported");
			yield return new WaitForSeconds(0.01f);
			InputManager.disabled = false;
		}
	}
