using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeathState : BossStateBase
{
    public const string ID = "DEATH";
    float currentTime;
    float maxTime;

    bool hasPlayedEscape;

    public override string Initialize(StateMachine stateMachine)
    {
        base.Initialize(stateMachine);
        return ID;
    }
    public override void Start()
    {
        currentTime = 0f;
        maxTime = 2f;
        boss.hiding = true;
    }

    public override void FixedUpdate()
    {

    }

    public override void Update()
    {
        AnimationSettings();

        currentTime += Time.deltaTime;
        if (currentTime >= maxTime)
        {
            boss.rb.velocity = new Vector2(0f, boss.jumpMaxHeight * 1.5f);
            if (!hasPlayedEscape)
            {
                if (!boss.audioSource.isPlaying)
                {
                    boss.audioSource.PlayOneShot(boss.escapeSound);
                    hasPlayedEscape = true;
                }
            }
        }

        if (boss.transform.position.y > 10f)
        {
            boss.gameObject.SetActive(false);
        }

        if (!boss.audioSource.isPlaying && currentTime < maxTime - 1)
        {
            boss.audioSource.PlayOneShot(boss.shellSound);
        }

    }

    public override void Exit()
    {

    }

    void AnimationSettings()
    {
        boss.animator.SetBool("isDead", true);
    }
}
