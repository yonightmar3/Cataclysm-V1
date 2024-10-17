/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forestMonsterA : monsterAgent
{
    [SerializeField] private Animator monster;

    private void Update()
    {
        base.Update();

        if (moving)
        {
            monster.SetTrigger("moving");
        }
        else monster.SetTrigger("stopped");
        
    }
}
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forestMonsterA : monsterAgent
{
    [SerializeField] private Animator monster;

    private void Update()
    {
        // Call the base update to handle movement logic
        base.Update();

        // Update animation based on movement and idle state
        if (moving)
        {
            // Play the moving animation when the monster is moving
            monster.SetBool("isMoving", true);
            monster.SetBool("isIdle", false);
        }
        else
        {
            // Play the idle animation when the monster is not moving
            monster.SetBool("isMoving", false);
            monster.SetBool("isIdle", true);
        }
    }
}



