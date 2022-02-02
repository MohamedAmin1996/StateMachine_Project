using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : StateMachine
{
    public GameObject playerPos;
    public Transform wallDetection;
    public GameObject spawnObject;
    public GameObject spawnPointLeft;
    public GameObject spawnPointRight;

    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    public Animator animator;


    public int health = 3;
    public float movementSpeed = 3;
    public float jumpMaxHeight = 5;
    public float shellJumpMaxHeight = 10;
    public LayerMask layerMask;
    public bool isOnGround = true;
    public bool readyToAttack = false;
    public bool hiding = false;

    public bool moveLeft;
    public bool moveRight;
    [HideInInspector] public ActivateAI marioIsHere;
    [HideInInspector] public SceneBehaviour sceneBehaviour;

    public bool isBossDead = false;

    public AudioSource audioSource;
    public AudioClip shellSound;
    public AudioClip attackSound;
    public AudioClip escapeSound;



    protected override void Awake()
    {
        base.Awake();

        AddState(new BossIdleState());
        AddState(new BossWalkState());
        AddState(new BossJumpState());
        AddState(new BossAttackState());
        
        AddState(new BossHideIdleState());
        AddState(new BossHideJumpState());
        
        AddState(new BossDeathState());
    }
    protected override void Start()
    {
        marioIsHere = GameObject.FindObjectOfType<ActivateAI>();
        sceneBehaviour = GameObject.FindObjectOfType<SceneBehaviour>();
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        if (readyToAttack)
        {
            StartCoroutine(InstanciateAttackObject());
            readyToAttack = false;
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            isOnGround = true;
        }

        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.transform.position.y >= transform.position.y)
            {
                if (health > 0 && !hiding) // boss is not hiding
                {
                    health -= 1; // boss takes damage
                    hiding = true;
                }
                
                if(health == 0)
                {
                    isBossDead = true;
                }
            }
        }

        if (collision.gameObject.tag == "Effect")
        {
            collision.gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            isOnGround = false;
        }

        if (collision.gameObject.tag == "Effect")
        {
            collision.gameObject.GetComponent<Collider2D>().enabled = true;
        }
    }

    IEnumerator InstanciateAttackObject()
    {
        yield return new WaitForSeconds(0.66f);
        float cooldown = 0.25f;
        audioSource.PlayOneShot(attackSound);

        if (moveLeft)
        {
            Instantiate(spawnObject, spawnPointLeft.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(cooldown);
            Instantiate(spawnObject, spawnPointLeft.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(cooldown);
            Instantiate(spawnObject, spawnPointLeft.transform.position, Quaternion.identity);
        }
        if (moveRight)
        {
            Instantiate(spawnObject, spawnPointRight.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(cooldown);
            Instantiate(spawnObject, spawnPointRight.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(cooldown);
            Instantiate(spawnObject, spawnPointRight.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(cooldown);
        }
    }
}
