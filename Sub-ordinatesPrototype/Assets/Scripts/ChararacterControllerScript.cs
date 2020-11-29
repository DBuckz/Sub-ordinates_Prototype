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
    public ChararacterControllerScript enemy;
    public GameObject rangeAttack;

    bool dashOverride = false;
    bool blocking = false;
    string projTag;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        character = teamController.chars[teamController.selected];
        speed = character.speed;
        jumpForce = character.jumpForce;
        spriteRend.sprite = character.characterSprite;
    }

    public void Changed(Characters newChar)
    {
        character = newChar;
        speed = character.speed;
        jumpForce = character.jumpForce;
        spriteRend.sprite = character.characterSprite;
    }


    void Update()
    {
        blocking = false;
        HorizontalInput = Input.GetAxis("Horizontal" + player);
        VerticalInput = Input.GetAxis("Vertical" + player);
        A = Input.GetButton("Jump" + player);
        B = Input.GetButton("Block" + player);
        X = Input.GetButtonDown("Attack" + player);
        Y = Input.GetButtonDown("Ult" + player);
        P = Input.GetButtonDown("SwitchP" + player);
        N = Input.GetButtonDown("SwitchN" + player);
        projTag = "Proj" + player;

        Block();
        Move();
        Jump();
        BetterJump();
        CheckIfGrounded();
        Attack();
        Switch();
        Ult();
    }
    void Move()
    {
        if (dashOverride) return;


        float moveBy = HorizontalInput * speed;
        if (blocking) moveBy = 0 ;
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
            //spriteRend.color = Color.red;
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
        if (X)
        {
            if (character.type == 0)
            {
                if (HorizontalInput == 0 && VerticalInput == 0) HorizontalInput = transform.localScale.x;
                RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, new Vector2(HorizontalInput, VerticalInput), 1, LayerMask.GetMask("Player"));
                if (hit.Length > 1)
                {
                    enemy.Hurt(character.attack);
                }
            }
            else if(character.type == 1)
            {
                if (HorizontalInput == 0 && VerticalInput == 0) HorizontalInput = transform.localScale.x;
                GameObject proj = Instantiate(rangeAttack, transform.position, Quaternion.Euler(0,0,-90+Mathf.Rad2Deg*Mathf.Atan2(VerticalInput, HorizontalInput)));
                proj.name = character.attack.ToString();
                proj.tag = projTag;
                Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
                rb.AddForce(proj.transform.up * 10, ForceMode2D.Impulse);
                Destroy(proj, 5f);

            }
            else if(character.type == 2)
            {
                dashOverride = true;
                Vector2 start = transform.position;
                StartCoroutine(DashWait(start));

                if (HorizontalInput == 0 && VerticalInput == 0) HorizontalInput = transform.localScale.x;
                rb.velocity = new Vector2(HorizontalInput, VerticalInput*0.7f) * 35;
                

            }

        }

    }

    IEnumerator DashWait(Vector2 start)
    {
        yield return new WaitForSeconds(0.15f);
        RaycastHit2D[] colliders = Physics2D.LinecastAll(start, transform.position, LayerMask.GetMask("Player"));
        if (colliders.Length > 1)
        {
            enemy.Hurt(character.attack);
        }
        dashOverride = false;

    }

    void Switch()
    {
        if ((P || N ) && teamController.deadCount < 2)
        {
            int dir = 0;
            if (P) dir = -1;
            else if (N) dir = 1;

            teamController.NewChar(dir);
        }

    }

    void Block()
    {
        if (B)
        {
            //spriteRend.color = Color.yellow;
            blocking = true;
        }
    }

    void Ult()
    {
        //if(Y) spriteRend.color = Color.white;
    }

    public void Hurt(int dmg)
    {
        if (dashOverride) return;
        if (blocking)
        {
            float newDmg = dmg * (1 - character.block);
            dmg = Mathf.RoundToInt(newDmg);
            Debug.Log(dmg);
        }
        teamController.Hurt(dmg);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Proj1" || collision.tag == "Proj2")
        {
            if(collision.tag != projTag)
            {
                Hurt(int.Parse(collision.name));
            }
        }
    }
}
