using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Collision coll;
    public Rigidbody2D rb;
    private Animator anim;
    
    [Header("Stats")]
    public float speed = 10;
    public float jumpForce = 50;
    public float slideSpeed = 5;
    public float dashSpeed = 20;
    
    [Header("Booleans")]
    public bool canMove;
    public bool wallSlide;
    public bool isDashing;
    private bool isWalking;
    private bool groundTouch;
    private bool hasDashed;
    public bool isFacingright = true;
    
    [Header("Coyote Time")]
    public float coyoteTime = 0.2f;
    private float coyoteTimer;

    [Header("Jump Buffer")]
    public float jumpBufferTimer = 0.2f; 
    private float jumpBufferTime = 0f;
    
    public int side = 1;
    public KeyCode DashButton = KeyCode.LeftShift;
    
    void Start()
    {
        coll = GetComponent<Collision>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        float xRaw = Input.GetAxisRaw("Horizontal");
        float yRaw = Input.GetAxisRaw("Vertical");
        Vector2 dir = new Vector2(x, y);

        Walk(dir);

        
        
        
        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferTime = Time.time;
        }
        
        if (coll.onGround && !isDashing)
        {
            coyoteTimer = coyoteTime;
            GetComponent<BetterJump>().enabled = true;

            if (Time.time - jumpBufferTime < jumpBufferTimer)
            {
                Jump(Vector2.up);
                coyoteTimer = 0;
                jumpBufferTime = 0;
            }
        }
        
        if (coll.onGround && !isDashing)
        {
            coyoteTimer = coyoteTime;
            GetComponent<BetterJump>().enabled = true;
        }
        else
        {
            rb.gravityScale = 3;
            if (coyoteTimer > 0 && Input.GetButtonDown("Jump")) 
            {
                Jump(Vector2.up);
                coyoteTimer = 0;
            }
            else
            {
                coyoteTimer -= Time.deltaTime; 
            }
        }
        
        if (coll.onGround && !isDashing)
        {
            GetComponent<BetterJump>().enabled = true;
        }
        
        else
        {
            rb.gravityScale = 3;
        }

        if(coll.onWall && !coll.onGround)
        {
            if (x != 0 )
            {
                wallSlide = true;
                WallSlide();
            }
        }

        if (!coll.onWall || coll.onGround)
            wallSlide = false;

        if (Input.GetButtonDown("Jump"))
        {

            if (coll.onGround)
                Jump(Vector2.up);
        }

        if (Input.GetKeyDown(DashButton) && !hasDashed)
        {
            if(xRaw != 0 || yRaw != 0)
                Dash(xRaw, yRaw);
        }

        if (coll.onGround && !groundTouch)
        {
            GroundTouch();
            groundTouch = true;
        }

        if(!coll.onGround && groundTouch)
        {
            groundTouch = false;
        }

        if ( wallSlide || !canMove)
            return;

        if(x > 0 && !isFacingright)
        {
            Flip();
        }
        if (x < 0 && isFacingright)
        {
           Flip();
        }

        if (rb.velocity.x != 0)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
        
        UpdateAnimations();
        
    }

    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        isFacingright = !isFacingright;
    }

    void GroundTouch()
    {
        hasDashed = false;
        isDashing = false;
    }

    private void Dash(float x, float y)
    {

        hasDashed = true;
        
        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2(x, y);

        rb.velocity += dir.normalized * dashSpeed;
        StartCoroutine(DashWait());
    }

    IEnumerator DashWait()
    {
        StartCoroutine(GroundDash());
        
        rb.gravityScale = 0;
        GetComponent<BetterJump>().enabled = false;
        isDashing = true;

        yield return new WaitForSeconds(0.3f);
        
        rb.gravityScale = 3;
        GetComponent<BetterJump>().enabled = true;
        isDashing = false;
    }

    IEnumerator GroundDash()
    {
        yield return new WaitForSeconds(.15f);
        if (coll.onGround)
            hasDashed = false;
    }
    

    private void WallSlide()
    {
        if(coll.wallSide != side)

            if (!canMove)
            return;

        bool pushingWall = false;
        if((rb.velocity.x > 0 && coll.onRightWall) || (rb.velocity.x < 0 && coll.onLeftWall))
        {
            pushingWall = true;
        }
        float push = pushingWall ? 0 : rb.velocity.x;

        rb.velocity = new Vector2(push, -slideSpeed);
    }

    private void Walk(Vector2 dir)
    {
        if (!canMove)
            return;
        
        else
        {
            rb.velocity = Vector2.Lerp(rb.velocity, (new Vector2(dir.x * speed, rb.velocity.y)), 20*Time.deltaTime);
        }
    }

    private void Jump(Vector2 dir)
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += dir * jumpForce;
    }

    private void UpdateAnimations()
    {
        anim.SetBool("isWalking", isWalking);
        anim.SetBool("isGrounded", groundTouch);
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isDashing", isDashing);
    }
    
}