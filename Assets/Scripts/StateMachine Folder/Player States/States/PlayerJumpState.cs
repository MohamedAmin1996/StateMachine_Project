using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerStateBase
{
    public const string ID = "JUMP";

    public override string Initialize(StateMachine stateMachine)
    {
        base.Initialize(stateMachine);
        return ID;
    }

    public override void Start()
    {
        //Debug.Log("Entering JUMP state");
        player.audioSource.PlayOneShot(player.jumpSound);
        player.rb.AddForce(Vector2.up * player.jumpMaxHeight, ForceMode2D.Impulse);
    }

    public override void Update()
    {
        //Debug.Log("Updating JUMP state");
        AnimationSettings();
        FlipSprite();

    }

    public override void Exit()
    {
        //Debug.Log("Exiting JUMP state");
    }

    public override void FixedUpdate()
    {
        player.rb.velocity = new Vector2(player.input.x * player.movementSpeed, player.rb.velocity.y);

        if (player.rb.velocity.y < 0f)
        {
            SwitchState(PlayerFallState.ID);
            return;
        }
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
        player.animator.SetBool("isJumping", true);
        player.animator.SetBool("isFalling", false);
        player.animator.SetBool("OnGround", false);
    }

    
}
