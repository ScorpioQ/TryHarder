using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CombatUnit, IDamageable
{
    private Rigidbody2D rb;
    private FixedJoystick joystick;

    public float jumpForce;

    public float health;

    public Transform groundCheck;
    public float checkRadius;
    public LayerMask groundLayer;

    public bool isGround;
    public bool isJump;
    public bool canJump;

    public GameObject jumpFX;
    public GameObject landFX;

    public GameObject bombPrefab;
    public float nextAttack = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        joystick = FindObjectOfType<FixedJoystick>();

        GameManager.instance.IsPlayer(this);

        health = GameManager.instance.LoadHeadth();
        UIManager.instance.UpdateHealth(health);
        isPlayer = true;
    }

    // Update is called once per frame
    public override void Update()
    {
        CheckInput();
        anim.SetBool("dead", isDead);

        if (isDead)
        {
            return;
        }
    }

    public void FixedUpdate()
    {
        if (isDead)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        PhysicsCheck();
        Movement();
        Jump();
    }

    void CheckInput()
    {
        if (Input.GetButtonDown("Jump") && isGround)
        {
            canJump = true;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            Attack();
        }
    }

    void Movement()
    {
        //float horizontalInput = Input.GetAxis("Horizontal");  // -1 ~ 1 ´øÐ¡Êý
        float horizontalInput = Input.GetAxisRaw("Horizontal"); // -1 ~ 1

        if (joystick) {
            if (joystick.Horizontal > 0)
                horizontalInput = 1;
            if (joystick.Horizontal < 0)
                horizontalInput = -1;
        }

        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);

        if (horizontalInput != 0)
        {
            transform.localScale = new Vector3(horizontalInput, 1, 1);
        }
    }

    void Jump()
    {
        if (canJump)
        {
            isJump = true;
            jumpFX.SetActive(true);
            jumpFX.transform.position = transform.position + new Vector3(0, -0.45f, 0);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            rb.gravityScale = 5;
            canJump = false;
        }
    }

    public void ButtonJump()
    {
        canJump = true;
    }

    public void Attack()
    {
        if (Time.time > nextAttack)
        {
            Instantiate(bombPrefab, transform.position, bombPrefab.transform.rotation);
            nextAttack = Time.time + attackRate;
        }
    }

    void PhysicsCheck()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
        if (isGround)
        {
            rb.gravityScale = 1;
            isJump = false;
        }
    }

    public override void AnimationEventLandStart()
    {
        base.AnimationEventLandStart();

        landFX.SetActive(true);
        landFX.transform.position = transform.position + new Vector3(0, -0.75f, 0);
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
    }

    public void GetHit(float damage)
    {
        if (!anim.GetCurrentAnimatorStateInfo(1).IsName("player_hit"))
        {
            health -= damage;
            if (health < 1)
            {
                health = 0;
                isDead = true;
            }
            anim.SetTrigger("hit");

            UIManager.instance.UpdateHealth(health);
        }
    }
}
