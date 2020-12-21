using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
   // public Characters characterNum;
    public ChararacterControllerScript enemy;
    public GameObject rangeAttack;

    bool dashOverride = false;
    bool blocking = false;
    string projTag;

    int meleeStore;
    int meleeMax;
    //bool meleeCooling = false;
    //bool switchCooling = false;
    //bool blockCooling = false;
    bool lastFrameBlock = false;


    float meleeTimer;
    float blockTimer;
    float switchTimer;
    float ultTimer;
    float meleeCool;
    float blockCool;
    float switchCool = 8;
    float ultCool;
    bool meleeWait;
    bool blockWait;
    bool switchWait;
    bool ultWait;

    public Image[] cooldowns;
    public Text attackCount;

    //animation
    public Animator m_Animator;

    public ParticleSystem particleWalk, particleJump, particleSwitch;


    // sounds
    public AudioSource JumpSound;
    public AudioSource PunchSound;
    public AudioSource LightningSound;
    public AudioSource BlockSound;
    public AudioSource SwapSound;
    public AudioSource DashSound;

    private void OnEnable()
    {

      
        //  moves = new Animation();
        //clips = character.animations;
    }
    void Start()
    {
        
        
        rb = GetComponent<Rigidbody2D>();
      
        character = teamController.chars[teamController.selected];
        speed = character.speed;
        jumpForce = character.jumpForce;
        spriteRend.sprite = character.characterSprite;
        meleeStore = character.meleeStore;
        meleeMax = character.meleeStore;
        CoolReset(true);

        // might break code above some how
       // characterNum = GetComponent<Characters>();
    }

    public void Changed(Characters newChar, bool death)
    {
        character = newChar;
        speed = character.speed;
        jumpForce = character.jumpForce;
        spriteRend.sprite = character.characterSprite;
        meleeStore = character.meleeStore;
        meleeMax = character.meleeStore;
        CoolReset(death);
    }

    public void CoolReset(bool death)
    {
         meleeTimer = 0;
         blockTimer = 0;
         //ultTimer;
         meleeCool = 1/ character.attackRate;
         blockCool = character.blockWait;
         //ultCool;
         meleeWait = false ;
         blockWait=false;

         //ultWait;

         switchWait = !death;

        if (death)
        {
            switchTimer = 0;
        }
        else
        {
            switchTimer = switchCool;
            switchWait = true;
        }

        //if (!switchWait)
        //{
        //    switchWait = true;
        //    switchTimer = switchCool;
        //}
    }


    void Update()
    {
        if (rb.velocity.x == 0 || !isGrounded) particleWalk.gameObject.SetActive(false);
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


        //animation
        
        //if (m_Animator.GetBool("isRun") == true)
        //{
        //    m_Animator.SetBool("isIdle", false);
        //    m_Animator.SetBool("isAttack", false);
        //    m_Animator.SetBool("isBlock", false);
        //}
        //if (m_Animator.GetBool("isIdle") == true)
        //{

        //    m_Animator.SetBool("isRun", false);
        //    m_Animator.SetBool("isAttack", false);
        //    m_Animator.SetBool("isBlock", false);
        //}
        //if (m_Animator.GetBool("isAttack") == true)
        //{
        //    m_Animator.SetBool("isIdle", false);
        //    m_Animator.SetBool("isRun", false);
        //    m_Animator.SetBool("isBlock", false);
        //}
        //if (m_Animator.GetBool("isBlock") == true)
        //{
        //    m_Animator.SetBool("isIdle", false);
        //    m_Animator.SetBool("isAttack", false);
        //    m_Animator.SetBool("isRun", false);
        //}


       /// if (m_Animator.GetBool("isRun") == true && m_Animator.GetBool("isRun") == true && m_Animator.GetBool("isRun") == true)
       // {
      //      m_Animator.SetBool("isIdle", true);
     //       m_Animator.SetInteger("idle", 1);
      //  }


            //

            //if(!meleeCooling&& meleeStore < meleeMax)
            //{
            //    meleeCooling = true;
            //    //StartCoroutine(MeleeCool(1/character.attackRate));
            //}
            if (Input.GetButtonUp("Block" + player)&& !blockWait)
        {
            blockWait = true;
            blockTimer = blockCool;
        }

        if (meleeStore < meleeMax)
        {
            if (!meleeWait)
            {
                meleeWait = true;
                meleeTimer = meleeCool;
            }
            meleeTimer -= Time.deltaTime;
        }
        if (meleeTimer < 0)
        {
            meleeTimer = 0;
            meleeStore++;
            meleeWait = false;
        }

        if (blockWait)
        {
            blockTimer -= Time.deltaTime;
        }
        if (blockTimer < 0)
        {
            blockTimer = 0;
            blockWait = false;
        }

        if (switchWait)
        {
            switchTimer -= Time.deltaTime;
        }
        if (switchTimer < 0)
        {
            switchTimer = 0;
            switchWait = false;
        }
        UIUpdate();
        Switch();
        Block();
        Move();
        Jump();
        BetterJump();
        CheckIfGrounded();
        Attack();

        Ult();
        if ((rb.velocity.x < 0f || rb.velocity.x > 0f) && isGrounded)
        {
            particleWalk.gameObject.SetActive(true);
            particleWalk.transform.localScale = this.transform.localScale;
        }
    }

    //IEnumerator MeleeCool(float wait)
    //{

    //    yield return new WaitForSeconds(wait);
    //    meleeStore++;
    //    meleeCooling = false;

    //}
    //IEnumerator SwitchCool(float wait)
    //{

    //    yield return new WaitForSeconds(wait);
    //    switchCooling = false;

    //}
    //IEnumerator BlockCool(float wait)
    //{
    //    yield return new WaitForSeconds(wait);
    //    blockCooling = false;
    //}
    void UIUpdate()
    {
        cooldowns[0].fillAmount = (meleeCool - meleeTimer) / meleeCool;
        cooldowns[1].fillAmount = (blockCool-blockTimer)/blockCool;
        cooldowns[2].fillAmount = (switchCool - switchTimer) / switchCool;
        //cooldowns[1].fillAmount = (blockCool - blockTimer) / blockCool;
        attackCount.text = meleeStore.ToString();
    }

    void Move()
    {
        if (dashOverride) return;






        
        float moveBy = HorizontalInput * speed;
        if (blocking) moveBy *= (1-character.block) ;
        rb.velocity = new Vector2(moveBy, rb.velocity.y);

        if(rb.velocity.x > 0)
        {
          
            if (m_Animator.GetBool("isJump") == false)
            {
                m_Animator.SetInteger("run", character.characterNumber);
                m_Animator.SetBool("isRun", true);
            }
            Turn(1);
        }

        else if(rb.velocity.x < 0)
        {
            
            
            Turn(-1);

            if (m_Animator.GetBool("isJump") == false)
            {
                m_Animator.SetInteger("run", character.characterNumber);
                m_Animator.SetBool("isRun", true);
            }
        }else m_Animator.SetBool("isRun",false);

        //animation  




        if (m_Animator.GetBool("isRun") == true)
        {
            m_Animator.SetBool("isIdle", false);
         
           // m_Animator.SetBool("isAttack", false);
           // m_Animator.SetBool("isBlock", false);
        }
        else if(m_Animator.GetBool("isBlock") == false && m_Animator.GetBool("isAttack") == false && m_Animator.GetBool("isJump") == false)
        {
            m_Animator.SetInteger("idle", character.characterNumber);
            m_Animator.SetBool("isIdle", true);
        }
    }

    void Jump()
    {
        
        if (A && (isGrounded || Time.time - lastTimeGrounded <= rememberGroundedFor) && rb.velocity.y==0 && !blocking)
        {
            //spriteRend.color = Color.red;
         
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            particleJump.Play();
            JumpSound.Play();
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
            m_Animator.SetBool("isJump", true);
            m_Animator.SetInteger("jump", character.characterNumber);
            rb.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !A)
        {
            
            rb.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0)
        {
            m_Animator.SetBool("isJump", true);
            m_Animator.SetInteger("jump", character.characterNumber);
        }else m_Animator.SetBool("isJump", false);

        if (m_Animator.GetBool("isJump") == true)
        {
            m_Animator.SetBool("isIdle", false);
            m_Animator.SetBool("isRun", false);
        }
         
    }

    void Turn(int dir)
    {
        gameObject.transform.localScale = new Vector3(dir, 1, 1);
    }

    void Attack()
    {


        if (X && meleeStore > 0 && !blocking)
        {

            m_Animator.SetBool("isAttack", true);

            m_Animator.SetInteger("attack", character.characterNumber);
            Invoke("StopAttack", character.attacktime);
            // m_Animator.SetBool("isIdle", false);
            // m_Animator.SetBool("isRun", false);
            // m_Animator.SetBool("isBlock", false);
            // StartCoroutine(AttackEnd());

            if (character.type == 0)
            {
                DashSound.Play();
                if (HorizontalInput == 0 && VerticalInput == 0) HorizontalInput = transform.localScale.x;
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + new Vector3(transform.localScale.x * 0.55f, 0), 1.1f, LayerMask.GetMask("Player"));
                //RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, new Vector2(HorizontalInput, VerticalInput), 1.5f, LayerMask.GetMask("Player"));
                if (colliders.Length > 1)
                {
                    PunchSound.Play();
                    enemy.Hurt(character.attack);
                }
            }
            else if (character.type == 1)
            {
                if (HorizontalInput == 0 && VerticalInput == 0) HorizontalInput = transform.localScale.x;
             // gameObject A=   rangeAttack.GetComponent<SpriteRenderer>;
                GameObject proj = Instantiate(rangeAttack, transform.position, Quaternion.Euler(0, 0, -90 + Mathf.Rad2Deg * Mathf.Atan2(VerticalInput, HorizontalInput)));
                proj.GetComponent<SpriteRenderer>().sprite = character.attackSprite;
                proj.name = character.attack.ToString();
                proj.tag = projTag;
                Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
                rb.AddForce(proj.transform.up * 9, ForceMode2D.Impulse);
                Destroy(proj, 5f);
                LightningSound.Play();

            }
            else if (character.type == 2)
            {
                dashOverride = true;
                Vector2 start = transform.position;
                StartCoroutine(DashWait(start));

                if (HorizontalInput == 0 && VerticalInput == 0) HorizontalInput = transform.localScale.x;
                rb.velocity = new Vector2(HorizontalInput, VerticalInput * 0.7f) * 30;

                DashSound.Play();
            }

            meleeStore--;
        }
       // m_Animator.SetBool("isAttack", false);
        

    }

    IEnumerator DashWait(Vector2 start)
    {
        yield return new WaitForSeconds(0.15f);
        //RaycastHit2D[] colliders = Physics2D.LinecastAll(start, transform.position, LayerMask.GetMask("Player"));


        float xChange = (transform.position.x - start.x);
        float yChange = (transform.position.y - start.y);
        float angle = Mathf.Rad2Deg*Mathf.Atan2(yChange,xChange);
        float dist = Vector2.Distance(transform.position, start);
        Vector2 dir = new Vector2(0, 0);

        RaycastHit2D[] colliders = Physics2D.BoxCastAll(start, new Vector2(1,1), angle, new Vector2(xChange,yChange),dist, LayerMask.GetMask("Player"));

        if (colliders.Length > 1)
        {
            PunchSound.Play();
            enemy.Hurt(character.attack);
        }
        dashOverride = false;

    }

    void Switch()
    {
        if ((P || N ) && teamController.deadCount < 2 && !switchWait)//&& !switchCooling)
        {
            int dir = 0;
            if (P) dir = -1;
            else if (N) dir = 1;
          //  character = GetComponent<Characters>();
            teamController.NewChar(dir, false);
            particleSwitch.Play();
            SwapSound.Play();
        }

    }

    void Block()
    {
        //blockWait = true;
        //if (!B) blockWait = true;
        if (B && !blockWait)//&& !blockCooling)
        {
            //spriteRend.color = Color.yellow;
            blocking = true;
            m_Animator.SetBool("isBlock", true);
            m_Animator.SetInteger("block", character.characterNumber);
            m_Animator.SetBool("isIdle", false);
            m_Animator.SetBool("isAttack", false);
            m_Animator.SetBool("isRun", false);
            
            //blockWait = false;
            //blockWait = false;
            //lastFrameBlock = blocking;
        }
        else m_Animator.SetBool("isBlock", false);
        //if (lastFrameBlock && !B)
        //{
        //    //blockCooling = true;
        //    //StartCoroutine(BlockCool(character.blockWait));
        //    blockWait = true;

        //}
        //lastFrameBlock = blocking;
    }


    void Ult()
    {
        //if(Y && !blocking) spriteRend.color = Color.white;
    }

    public void Hurt(int dmg)
    {
        
        if (blocking)
        {
            float newDmg = dmg * (1 - character.block);
            dmg = Mathf.RoundToInt(newDmg);
        }
        teamController.Hurt(dmg);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Proj1" || collision.tag == "Proj2")
        {
            if(collision.tag != projTag)
            {
                if (dashOverride) return;
                Hurt(int.Parse(collision.name));
            }
        }
    }

    private void StopAttack()
    {
        m_Animator.SetBool("isAttack", false);
    }

    IEnumerator AttackEnd()
    {

        yield return new WaitForSeconds(0.3f);
        m_Animator.SetBool("isAttack", false);
       // m_Animator.SetBool("isIdle", true);
    }
    }