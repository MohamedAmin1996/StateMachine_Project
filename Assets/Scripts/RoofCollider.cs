using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofCollider : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
        Invoke("CollsionOn", 2f);
    }

    void CollsionOn()
    {
        gameObject.SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Boss")
        {
            if (collision.gameObject.GetComponent<BossController>().isBossDead)
            {
                gameObject.SetActive(false);
            }
        }
    }


}
