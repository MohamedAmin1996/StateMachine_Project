using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackState : BossStateBase
{
    public const string ID = "ATTACK";

    public override string Initialize(StateMachine stateMachine)
    {
        base.Initialize(stateMachine);
        return ID;
    }
    public override void Start()
    {
        if (boss.playerPos.transform.position.x < boss.transform.position.x)
        {
            boss.moveLeft = true;
            boss.moveRight = false;
        }

        if (boss.playerPos.transform.position.x >= boss.transform.position.x)
        {
            boss.moveLeft = false;
            boss.moveRight = true;
        }

        boss.rb.velocity = new Vector2(0f, boss.rb.velocity.y);
        boss.readyToAttack = true;
        
        FlipSprite();
        AnimationSettings();
    }

    public override void FixedUpdate()
    {

    }

    public override void Update()
    {
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

        if (boss.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            SwitchState(BossWalkState.ID);
            return;
        }
    }

    public override void Exit()
    {
        boss.animator.SetBool("isAttacking", false);
    }

    void FlipSprite()
    {
        if (boss.moveRight)
        {
            boss.spriteRenderer.flipX = true;
        }
        else if (boss.moveLeft)
        {
            boss.spriteRenderer.flipX = false;
        }
    }

    void AnimationSettings()
    {
        boss.animator.SetBool("isAttacking", true);
    }
}
