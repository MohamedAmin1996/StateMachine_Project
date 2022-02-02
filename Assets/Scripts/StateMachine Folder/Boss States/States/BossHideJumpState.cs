using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHideJumpState : BossStateBase
{
    public const string ID = "HIDE_JUMP";
    bool hasJumped = false;

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

        if (boss.moveLeft)
        {
            boss.rb.AddForce(Vector2.up * boss.shellJumpMaxHeight + Vector2.right * -boss.movementSpeed, ForceMode2D.Impulse);
            hasJumped = true;
        }

        if (boss.moveRight)
        {
            boss.rb.AddForce(Vector2.up * boss.shellJumpMaxHeight + Vector2.right * boss.movementSpeed, ForceMode2D.Impulse);
            hasJumped = true;
        }
    }

    public override void FixedUpdate()
    {

    }

    public override void Update()
    {
        if (hasJumped && boss.isOnGround)
        {
            SwitchState(BossJumpState.ID);
            return;
        }

        if (!boss.audioSource.isPlaying)
        {
            boss.audioSource.PlayOneShot(boss.shellSound);
        }
    }

    public override void Exit()
    {
        boss.hiding = false;
        boss.animator.SetBool("isHiding", false);
    }
}
