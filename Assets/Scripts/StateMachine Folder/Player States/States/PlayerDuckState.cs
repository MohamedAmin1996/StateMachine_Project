using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDuckState : PlayerStateBase
{
    public const string ID = "DUCK";

    public override string Initialize(StateMachine stateMachine)
    {
        base.Initialize(stateMachine);
        return ID;
    }
    public override void Start()
    {
        player.rb.velocity = new Vector2(0f, player.rb.velocity.y);
    }

    public override void FixedUpdate()
    {

    }

    public override void Update()
    {
        AnimationSettings();
        if (player.input.y >= 0f)
        {
            SwitchState(PlayerIdleState.ID);
            return;
        }
    }
    public override void Exit()
    {

    }

    void AnimationSettings()
    {
        if (player.input.y < 0f)
        {
            player.animator.SetBool("IsDucking", true);
            player.animator.SetBool("OnGround", true);
        }
        else
        {
            player.animator.SetBool("IsDucking", false);
            player.animator.SetBool("OnGround", true);
        }

    }


}
