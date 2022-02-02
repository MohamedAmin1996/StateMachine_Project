using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectMovement : MonoBehaviour
{
    GameObject playerPos;
    Vector2 newPos;
    float speed = 5.0f;

    [HideInInspector] public SceneBehaviour sceneBehaviour;

    private void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player");
        sceneBehaviour = GameObject.FindObjectOfType<SceneBehaviour>();
        newPos = playerPos.transform.position;
    }

    private void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, newPos, step);

        if ((Vector2)transform.position == newPos)
        {
            Destroy(gameObject);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (playerPos.GetComponent<PlayerController>().health == 0 || playerPos.GetComponent<PlayerController>().health < 0)
            {
                //Debug.Log("GO TO LOSE SCREEN");
                sceneBehaviour.playerLost = true;
            }
            else
            {
                playerPos.GetComponent<PlayerController>().health -= 1;
            }
        }
        Destroy(gameObject);
    }
}
