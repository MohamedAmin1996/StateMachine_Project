using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossJumpState : BossStateBase
{
    public const string ID = "JUMP";

    public override string Initialize(StateMachine stateMachine)
    {
        base.Initialize(stateMachine);
        return ID;
    }
    public override void Start()
    {
        boss.rb.AddForce(Vector2.up * boss.jumpMaxHeight, ForceMode2D.Impulse);
    }

    public override void FixedUpdate()
    {

    }

    public override void Update()
    {
        AnimationSettings();

        if (boss.isBossDead)
        {
            SwitchState(BossDeathState.ID);
            return;
        }

        if (boss.hiding)
        {
            SwitchState(BossHideIdleState.ID);
            return;
        }

        if (boss.isOnGround)
        {
            SwitchState(BossWalkState.ID);
            return;
        }
    }

    public override void Exit()
    {

    }

    void AnimationSettings()
    {
        boss.animator.SetBool("isJumping", true);
        boss.animator.SetBool("isMoving", false);
    }
}
