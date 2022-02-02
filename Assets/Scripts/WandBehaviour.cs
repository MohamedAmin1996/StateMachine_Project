using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandBehaviour : MonoBehaviour
{
    GameObject bossPos;
    Rigidbody2D rb;
    Animator animator;

    bool readyToFall = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        bossPos = GameObject.FindGameObjectWithTag("Boss");

        transform.position = new Vector2(0f, 10f);
        rb.simulated = false;
    }

    private void Update()
    {
        if (!readyToFall)
        {
            if (bossPos.GetComponent<BossController>().isBossDead)
            {
                transform.position = new Vector2(bossPos.transform.position.x, 10f);
            }
        }
        

        if (bossPos.activeInHierarchy == false)
        {
            readyToFall = true;
            rb.simulated = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb.simulated = false;
        animator.SetBool("hasLanded", true);

        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }



}
