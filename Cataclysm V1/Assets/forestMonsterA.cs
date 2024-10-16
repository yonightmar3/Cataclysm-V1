using System.Collections;
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
