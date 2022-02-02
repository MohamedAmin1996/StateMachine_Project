using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWalkState : BossStateBase
{
    public const string ID = "WALK";
    float currentTime;
    int maxTime;

    public override string Initialize(StateMachine stateMachine)
    {
        base.Initialize(stateMachine);
        return ID;
    }
    public override void Start()
    {
        currentTime = 0;
        maxTime = Random.Range(1, 2);

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
    }

    public override void FixedUpdate()
    {
        if (boss.moveLeft)
        {
            //Debug.Log("Move Left");
            boss.rb.velocity = new Vector2(-boss.movementSpeed, boss.rb.velocity.y);
        }

        if (boss.moveRight)
        {
            //Debug.Log("Move Right");
            boss.rb.velocity = new Vector2(boss.movementSpeed, boss.rb.velocity.y);
        }
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

        currentTime += Time.deltaTime;
        if (currentTime >= maxTime)
        {
            SwitchState(BossAttackState.ID);
            return;
        }

        WallDetection();
        FlipSprite();
        AnimationSettings();
    }

    public override void Exit()
    {
       
    }

    void WallDetection()
    {
        if (boss.moveRight)
        {
            RaycastHit2D wallInfo = Physics2D.Raycast(boss.wallDetection.position, Vector2.right, 1.5f, boss.layerMask);
            Debug.DrawRay(boss.wallDetection.position, Vector2.right * 1.5f, Color.white);

            if (wallInfo.collider != null)
            {
                SwitchState(BossJumpState.ID);
                return;
            }
        }

        if (boss.moveLeft)
        {
            RaycastHit2D wallInfo = Physics2D.Raycast(boss.wallDetection.position, Vector2.left, 1.5f, boss.layerMask);
            Debug.DrawRay(boss.wallDetection.position, Vector2.left * 1.5f, Color.white);

            if (wallInfo.collider != null)
            {
                SwitchState(BossJumpState.ID);
                return;
            }
        }
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
        boss.animator.SetBool("isMoving", true);
        boss.animator.SetBool("isJumping", false);
    }

    
}
