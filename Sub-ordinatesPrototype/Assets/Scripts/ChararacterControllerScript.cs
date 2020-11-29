using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChararacterControllerScript : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    public float jumpForce;
    bool isGrounded = false;
    public Transform isGroundedChecker;
    public float checkGroundRadius;
    public LayerMask groundLayer;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    public float rememberGroundedFor;
    float lastTimeGrounded;

    public int player;
    float VerticalInput;
    float HorizontalInput;
    bool A;
    bool B;
    bool X;
    bool Y;
    bool P;
    bool N;

    public SpriteRenderer spriteRend;
    public TeamController teamController;

    public Characters character;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        character = teamController.chars[teamController.selected];
    }


    void Update()
    {
        HorizontalInput = Input.GetAxis("Horizontal" + player);
        VerticalInput = Input.GetAxis("Vertical" + player);
        A = Input.GetButton("Jump" + player);
        B = Input.GetButton("Block" + player);
        X = Input.GetButton("Attack" + player);
        Y = Input.GetButton("Ult" + player);
        P = Input.GetButton("SwitchP" + player);
        N = Input.GetButton("SwitchN" + player);

        Move();
        Jump();
        BetterJump();
        CheckIfGrounded();
        Attack();
        Block();
        Switch();
        Ult();
    }
    void Move()
    {
        float moveBy = HorizontalInput * speed;
        rb.velocity = new Vector2(moveBy, rb.velocity.y);

        if(rb.velocity.x > 0)
        {
            Turn(1);
        }
        else if(rb.velocity.x < 0)
        {
            Turn(-1);
        }
    }

    void Jump()
    {
        
        if (A && (isGrounded || Time.time - lastTimeGrounded <= rememberGroundedFor) && rb.velocity.y==0)
        {
            spriteRend.color = Color.red;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    void CheckIfGrounded()
    {
        Collider2D colliders = Physics2D.OverlapCircle(isGroundedChecker.position, checkGroundRadius, groundLayer);
        if (colliders != null)
        {
            isGrounded = true;
        }
        else
        {
            if (isGrounded)
            {
                lastTimeGrounded = Time.time;
            }
            isGrounded = false;
        }
    }

    void BetterJump()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !A)
        {
            rb.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    void Turn(int dir)
    {
        gameObject.transform.localScale = new Vector3(dir, 1, 1);
    }

    void Attack()
    {
        if (X) spriteRend.color = Color.green;





    }

    void Switch()
    {
        if(P || N) spriteRend.color = Color.blue;
    }

    void Block()
    {
        if(B) spriteRend.color = Color.yellow;
    }

    void Ult()
    {
        if(Y) spriteRend.color = Color.white;
    }

    public void Hurt(int dmg)
    {
        teamController.Hurt(dmg);
    }
}
