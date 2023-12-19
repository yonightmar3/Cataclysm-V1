/*using System.Collections;
using UnityEngine;

public class RepeatingHallway : MonoBehaviour
{
    private GameObject player;
    public Transform leftWingTeleportLocation;
    public GameObject appearableWall;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(LeftWingTeleport());
        }
    }

    IEnumerator LeftWingTeleport()
    {
        // Disable player input during teleportation
        InputManager.disabled = true;

        // Wait for a short duration before teleporting
        yield return new WaitForSeconds(0.01f);

        // Teleport the player to the left wing location while maintaining X and Y coordinates
        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, leftWingTeleportLocation.position.z);

        appearableWall.SetActive(true);

        // Log teleportation message
        Debug.Log("Player teleported");

        // Wait for a short duration before enabling player input again
        yield return new WaitForSeconds(0.01f);

        // Enable player input
        InputManager.disabled = false;
    }

}
*/

using System.Collections;
using UnityEngine;

public class RepeatingHallway : portalTeleporter
{
    public GameObject appearableWall;

    private void Update()
    {
        if (teleported == true){
            appearableWall.SetActive(true);
        }

        base.Update();
    }
}