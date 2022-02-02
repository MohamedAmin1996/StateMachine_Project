using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerStateBase
{
    public const string ID = "WALK";
    public override string Initialize(StateMachine stateMachine)
    {
        base.Initialize(stateMachine);
        return ID;
    }

    public override void Start()
    {
        //Debug.Log("Entering WALK state");
    }

    public override void Update()
    {
        //Debug.Log("Updating WALK state");

        if (Input.GetKeyDown(KeyCode.Space) && player.isOnGround)
        {
            SwitchState(PlayerJumpState.ID);
            return;
        }

        FlipSprite();
        AnimationSettings();
    }

    public override void Exit()
    {
        //Debug.Log("Exiting WALK state");
    }

    public override void FixedUpdate()
    {
        if (player.input.x == 0f)
        {
            player.rb.velocity = Vector3.zero;
            SwitchState(PlayerIdleState.ID);
            return;
        }

        if (!player.isOnGround)
        {
            SwitchState(PlayerFallState.ID);
            return;
        }

        if (player.input.y < -0f && player.isOnGround)
        {
            SwitchState(PlayerDuckState.ID);
            return;
        }

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
        if (player.input.x > 0f || player.input.x < 0f)
        {
            player.animator.SetBool("isMoving", true);
            player.animator.SetBool("isJumping", false);
            player.animator.SetBool("isFalling", false);
            player.animator.SetBool("OnGround", true);
        }
        else
        {
            player.animator.SetBool("isMoving", false);
            player.animator.SetBool("isJumping", false);
            player.animator.SetBool("isFalling", false);
            player.animator.SetBool("OnGround", true);
        }
    }


}
