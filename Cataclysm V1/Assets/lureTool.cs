using System.Collections.Generic;
using UnityEngine;

public class lureTool : MonoBehaviour
{
    public Camera playerCamera;
    public LayerMask targetLayer;
    public float detectionRadius = 10f;
    public KeyCode commandKey = KeyCode.E;

    // List to track all monsters moved by the tool
    private List<monsterAgent> movedMonsters = new List<monsterAgent>();

    void Update()
    {
        if (Input.GetKeyDown(commandKey))
        {
            RaycastHit hit;
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, targetLayer))
            {
                Vector3 targetPoint = hit.point;
                CommandMonsters(targetPoint);
            }
        }
    }

    void CommandMonsters(Vector3 targetPoint)
    {
        Collider[] monstersInRange = Physics.OverlapSphere(targetPoint, detectionRadius, LayerMask.GetMask("Monster"));

        if (monstersInRange.Length > 0)
        {
            foreach (Collider monsterCollider in monstersInRange)
            {
                monsterAgent agentScript = monsterCollider.GetComponent<monsterAgent>();
                if (agentScript != null)
                {
                    agentScript.SetPatrolPoint(targetPoint);
                    agentScript.isMovedByTool = true;

                    if (!movedMonsters.Contains(agentScript))
                    {
                        movedMonsters.Add(agentScript);
                    }
                }
            }
        }
    }

    // Call this to reset all monsters after the player dies
    public void ResetAllMonsters()
    {
        foreach (monsterAgent agent in movedMonsters)
        {
            agent.TeleportToInitialPosition();
            agent.isMovedByTool = false;
        }

        movedMonsters.Clear(); // Clear the list after resetting
    }
}
