using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : StateMachine
{
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    public Animator animator;

    public int health = 2;
    public float movementSpeed = 5;
    public float jumpMaxHeight = 5;
    public Transform feetPos;
    public bool isOnGround = true;

    public AudioSource audioSource;
    public AudioClip jumpSound;
    public AudioClip bumpSound;
    
    [HideInInspector] public Vector2 input;
    [HideInInspector] public ActivateAI bossIsHere;
    [HideInInspector] public SceneBehaviour sceneBehaviour;

    protected override void Awake()
    {
        base.Awake();

        AddState(new PlayerIdleState());
        AddState(new PlayerWalkState());
        AddState(new PlayerJumpState());
        AddState(new PlayerFallState());
        AddState(new PlayerDuckState());
    }

    protected override void Start()
    {
        bossIsHere = GameObject.FindObjectOfType<ActivateAI>();
        sceneBehaviour = GameObject.FindObjectOfType<SceneBehaviour>();
        base.Start();
    }

    protected override void Update()
    {
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        CheckGround();
        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    void CheckGround()
    {
        Vector2 offset = new Vector2(0.4f, 0f);
        float dist = 0.5f;

        RaycastHit2D isGrounded = Physics2D.Raycast(feetPos.position, Vector2.down, dist);
        Debug.DrawRay(feetPos.position, Vector2.down * dist, Color.red);

        RaycastHit2D isGroundedLeft = Physics2D.Raycast((Vector2)feetPos.position + (offset * -1), Vector2.down, dist);
        Debug.DrawRay((Vector2)feetPos.position + (offset * -1), Vector2.down * dist, Color.green);

        RaycastHit2D isGroundedRight = Physics2D.Raycast((Vector2)feetPos.position + (offset), Vector2.down, dist);
        Debug.DrawRay((Vector2)feetPos.position + (offset), Vector2.down * dist, Color.blue);

        if (isGrounded.collider != null || isGroundedLeft.collider != null || isGroundedRight.collider != null)
        {
            isOnGround = true;
        }
        else
        {
            isOnGround = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            bossIsHere.activateAI = true;
        }

        if (collision.gameObject.tag == "Boss")
        {
            if (transform.position.y >= collision.gameObject.transform.position.y) // If player hit boss in the head
            {
                rb.AddForce(Vector2.up * jumpMaxHeight, ForceMode2D.Impulse);
                audioSource.PlayOneShot(bumpSound);
            }
            else // Player takes damage
            {
                if (health > 0)
                {
                    health -= 1;
                }
                else
                {
                    sceneBehaviour.playerLost = true;
                } 
            }
        }

        if (collision.gameObject.tag == "Wand")
        {
            sceneBehaviour.playerWon = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

    }
}
