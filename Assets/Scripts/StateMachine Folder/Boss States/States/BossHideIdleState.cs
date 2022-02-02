using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHideIdleState : BossStateBase
{
    public const string ID = "HIDE_IDLE";

    float currentTime;
    int maxTime;

    public override string Initialize(StateMachine stateMachine)
    {
        base.Initialize(stateMachine);
        return ID;
    }
    public override void Start()
    {
        boss.hiding = true;
        boss.rb.velocity = new Vector2(0f, boss.rb.velocity.y);
        AnimationSettings();

        currentTime = 0;
        maxTime = 1;
    }

    public override void FixedUpdate()
    {

    }

    public override void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= maxTime)
        {
            SwitchState(BossHideJumpState.ID);
            return;
        }

        if (!boss.audioSource.isPlaying)
        {
            boss.audioSource.PlayOneShot(boss.shellSound);
        }
    }

    public override void Exit()
    {

    }

    void AnimationSettings()
    {
        boss.animator.SetBool("isHiding", true);
    }
}
