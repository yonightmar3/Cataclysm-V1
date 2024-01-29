using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forestMonsterA : monsterAgent
{
    [SerializeField] private Animator monsterA;

    private void Update()
    {
        base.Update();

        if (moving)
        {
            monsterA.SetTrigger("moving");
        }
        else monsterA.SetTrigger("stopped");
        
    }
}
