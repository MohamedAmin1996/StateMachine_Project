using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerStateBase
{
    public const string ID = "FALL";

    public override string Initialize(StateMachine stateMachine)
    {
        base.Initialize(stateMachine);
        return ID;
    }

    public override void Start()
    {
        //Debug.Log("Entering FALL state");
        AnimationSettings();
    }

    public override void Update()
    {
        //Debug.Log("Updating FALL state");

        if (player.isOnGround && (player.input.x > 0 || player.input.y > 0 || player.input.x < 0 || player.input.y < 0))
        {
            SwitchState(PlayerWalkState.ID);
            return;
        }
        if (player.isOnGround && (player.input.x == 0f || player.input.y == 0f))
        {
            SwitchState(PlayerIdleState.ID);
            return;
        }

        

        FlipSprite();
    }

    public override void Exit()
    {
        ////Debug.Log("Exiting FALL state");
        player.isOnGround = true;

        if (player.isOnGround && (player.input.x > 0 || player.input.y > 0 || player.input.x < 0 || player.input.y < 0))
        {
            player.animator.SetBool("isMoving", true);
            player.animator.SetBool("isJumping", false);
            player.animator.SetBool("isFalling", false);
            player.animator.SetBool("OnGround", true);
        }
        if (player.isOnGround && (player.input.x == 0f || player.input.y == 0f))
        {
            player.animator.SetBool("isMoving", false);
            player.animator.SetBool("isJumping", false);
            player.animator.SetBool("isFalling", false);
            player.animator.SetBool("OnGround", true);
        }
    }

    public override void FixedUpdate()
    {
        player.rb.velocity = new Vector2(player.input.x * player.movementSpeed, player.rb.velocity.y);
    }

    void FlipSprite()
    {
        if (player.input.x > 0 && !player.spriteRenderer.flipX)
        {
            player.spriteRenderer.flipX = true;
        }
        else if (player.input.x < 0 && player.spriteRenderer.flipX)
        {
            player.spriteRenderer.flipX = false;
        }
    }

    void AnimationSettings()
    {
        player.animator.SetBool("isJumping", false);
        player.animator.SetBool("isFalling", true);
        player.animator.SetBool("OnGround", false);
    }

    
}
