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


    //attempt at multi controller variables
    public ControllerInputDetection input { get; private set; }
    public Player player { get; private set; }
    int playerNumber;
    public PlayerNum id;
    private string _xAxis, _yAxis, Abutton;
    void Start()
    {


        if (id == PlayerNum.p1)
        {
            _xAxis = "Horizontal";
            _yAxis = "Vertical";
            Abutton = "Submit";
        }
        if (id == PlayerNum.p2)
        {
            _xAxis = "Horizontal2";
            _yAxis = "Vertical2";
            Abutton = "Submit2";
        }
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        Move();
        Jump();
        BetterJump();
        CheckIfGrounded();
        Attack();
    }
    void Move()
    {
        float x = Input.GetAxisRaw(_xAxis);
        float moveBy = x * speed;
        rb.velocity = new Vector2(moveBy, rb.velocity.y);

        if(rb.velocity.x > 0)
        {
            Turn();
        }
        else if(rb.velocity.x < 0)
        {
            Turn();
        }
    }

    void Jump()
    {
        if (Input.GetAxisRaw(_yAxis) > 0 && (isGrounded || Time.time - lastTimeGrounded <= rememberGroundedFor))
        {
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
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    void Turn()
    {

    }

    void Attack()
    {

    }

    void Switch()
    {

    }

   
    public void SetPlayer(Player player)
    {
        this.player = player;
       playerNumber = player.playerNumber;
        input = player.GetComponent<ControllerInputDetection>();
    }

    public enum PlayerNum
    {
        p1,
        p2
    }
}
