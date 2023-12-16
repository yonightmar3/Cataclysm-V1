using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawn : MonoBehaviour
{
    public GameObject deathScreen;   
    public static bool puzzle1Complete = false;
    GameObject player;
    private Vector3 mazeRespawn = new Vector3(7f, 1f, 71f);

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void respawnMaze()
    {
        StartCoroutine("respawnMazeCoroutine");
    }

    IEnumerator respawnMazeCoroutine()
    {
        //InputManager.disabled = true;
        yield return new WaitForSeconds(0.1f);
        player.transform.position = mazeRespawn;
        Debug.Log("player teleported");
        yield return new WaitForSeconds(0.1f);
        deathScreen.SetActive(false);
        Cursor.visible = false;
        InputManager.disabled = false;
    }
}
