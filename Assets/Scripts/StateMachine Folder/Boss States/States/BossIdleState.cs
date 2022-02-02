using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleState : BossStateBase
{
    public const string ID = "IDLE";

    public override string Initialize(StateMachine stateMachine)
    {
        base.Initialize(stateMachine);
        return ID;
    }
    public override void Start()
    {
       
    }

    public override void FixedUpdate()
    {
        
    }

    public override void Update()
    {
        if (boss.marioIsHere.activateAI)
        {
            SwitchState(BossWalkState.ID);
            return;
        }
    }

    public override void Exit()
    {
        
    }
}
