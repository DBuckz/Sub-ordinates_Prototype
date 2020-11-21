using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChararacterControllerScript : MonoBehaviour
{
    //    public Rigidbody2D rb;
    //    public GameObject hitto;
    //    [SerializeField] ContactFilter2D groundFilter;
    //    // Start is called before the first frame update
    //    void Start()
    //    {

    //    }

    //    // Update is called once per frame
    //    void Update()
    //    {
    //        if (Input.GetAxis("Vertical") > 0)
    //        {
    //            RaycastHit2D[] hits = new RaycastHit2D[2];

    //            hits[1] = Physics2D.Raycast(transform.position, Vector2.down, 1.5f, 8);
    //            //Debug.DrawRay(transform.position, new Vector2(0,-1.5f), Color.red, 8);

    //           // hitto = hit.transform.gameObject;

    //            if (Physics2D.Raycast(transform.position, Vector2.down, groundFilter, hits, 1.5f)>1)
    //            {
    //                Debug.Log("yes");
    //                rb.AddForce(new Vector2(0, Input.GetAxis("Vertical") * 45), ForceMode2D.Impulse);
    //            }

    //        }
    //        if (Input.GetAxis("Horizontal") !=0 )
    //        {
    //            rb.AddForce(new Vector2(Input.GetAxis("Horizontal")*100, 0));
    //        }
    //    }
    //}

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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        Move();
        Jump();
        BetterJump();
        CheckIfGrounded();
    }
    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float moveBy = x * speed;
        rb.velocity = new Vector2(moveBy, rb.velocity.y);
    }

    void Jump()
    {
        if (Input.GetAxisRaw("Vertical") > 0 && (isGrounded || Time.time - lastTimeGrounded <= rememberGroundedFor))
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
}
