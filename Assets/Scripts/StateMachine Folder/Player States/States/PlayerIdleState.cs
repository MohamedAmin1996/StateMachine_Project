using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerStateBase
{
    public const string ID = "IDLE";

    public override string Initialize(StateMachine stateMachine)
    {
        base.Initialize(stateMachine);
        return ID;
    }

    public override void Start()
    {
        //Debug.Log("Entering IDLE state");
        player.rb.velocity = new Vector2(0f, player.rb.velocity.y);
    }

    public override void Update()
    {
        //Debug.Log("Updating IDLE state");

        if (player.input.x != 0 )
        {
            SwitchState(PlayerWalkState.ID);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) && player.isOnGround)
        {

            SwitchState(PlayerJumpState.ID);
            return;
        }

        if (player.input.y < -0f && player.isOnGround)
        {
            SwitchState(PlayerDuckState.ID);
            return;
        }
    }

    public override void Exit()
    {
        //Debug.Log("Exiting IDLE state");
    }

    public override void FixedUpdate()
    {
        if (!player.isOnGround)
        {
            SwitchState(PlayerFallState.ID);
            return;
        }
    }
}
