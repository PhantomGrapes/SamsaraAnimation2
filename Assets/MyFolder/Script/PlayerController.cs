using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    Rigidbody2D player;
    public Animator animator;
    [Header("ShapeInformation")]
    public float width;
    public float height;
    [Header("Movement")]
    public float movementSpeed;
    public bool facingRight;
    public bool running;
    float refVelocityX;
    [Header("Jump")]
    public LayerMask groundMask;
    public bool grounded;
    public float jumpMaxHeight;
    public float jumpMinHeight;
    public float jumpMaxTime;
    float jumpMaxVelocity;
    float jumpMinVelocity;
    [Header("DoubleJump")]
    public bool canDoubleJump;
    [Header("GrabLedge")]
    private GrabLedgeController grabLedgeController;
    public bool isMovingToFootHold;
    public bool isMovingToPlatform;
    private Vector2 grabVelRef;
    private Vector2 grabFoothold;
    private Vector2 grabPlatformPos;
    public float timeFromInitialPosToFoothold;
    public float timeFromFootholdToPlatform;
    [Header("Roll")]
    public float rollVelocityX;
    public bool isRolling;
    [Header("Attack")]
    public bool isAttacking;
    public bool isAttackingTrans;
    public bool isPressingS;
    public bool isPressingW;
    public float verticalForce;
    public float horizontalForce;
    public ParticleSystem slashClip1And3;
    public ParticleSystem slashClip2;
    public ParticleSystem slashClip4;
    public ParticleSystem slashClip5;
    private DartPivotController dart;
    [Header("Camera Shake")]
    public float shakeTimer;
    public float shakeAmount;
    private CameraController cameraController;

    void Start () {
        player = GetComponent<Rigidbody2D>();
        cameraController = FindObjectOfType<CameraController>();
        animator = GetComponent<Animator>();
        grabLedgeController = FindObjectOfType<GrabLedgeController>();
        dart = FindObjectOfType<DartPivotController>();

        width = GetComponent<BoxCollider2D>().size.x + 0.2f;
        height = GetComponent<BoxCollider2D>().size.y + 0.2f;

        CalculateGravityAndVelocity();

        facingRight = true;
        running = true;
        isRolling = false;    
	}
	
	void Update () {

        StateMachine();

        // if player is grabbing ledge, following process will not be done 
        if (GrabLedge())
        {
            //print(transform.position);
            return;
        }

        if (!isRolling && !isAttacking && !isAttackingTrans)
        {
            Move();
        }

        Attack();
        //Jump();
        DoubleJump();
        Roll(); 
	}

    void Move()
    {
        float targetVelocityX = Input.GetAxisRaw("Horizontal") * movementSpeed;
        player.velocity = new Vector2(Mathf.SmoothDamp(player.velocity.x, targetVelocityX, ref refVelocityX, 0.02f), player.velocity.y);

        if(Input.GetAxisRaw("Horizontal") > 0)
        {
            facingRight = true;
            running = true;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if(Input.GetAxisRaw("Horizontal") < 0)
        {
            facingRight = false;
            running = true;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            running = false;
        }
    }

    bool GrabLedge()
    {
        if (isMovingToFootHold)
        {
            GrabMoveToFootHold();
            return true;
        }
        else if (isMovingToPlatform)
        {
            GrabMoveToPlatform();
            return true;
        }
        else if (grabLedgeController.ledgeDetected && Input.GetAxisRaw("Horizontal") != 0)
        {
            GrabStart();
            return true;
        }
        else
            return false;
    }

    void Jump()
    {
        grounded = Physics2D.OverlapBox(transform.position + new Vector3(0, -1.6f, 0), new Vector2(1, 0.1f), 0, groundMask);
        if (grounded)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button0))
            {
                player.velocity = new Vector2(player.velocity.x, jumpMaxVelocity);
            }
        }
    }

    void DoubleJump()
    {
        grounded = Physics2D.OverlapBox(transform.position + new Vector3(0, -1.6f, 0), new Vector2(1.1f, 0.1f), 0, groundMask);
        if (grounded && !isAttacking && !isAttackingTrans)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button0))
            {
                player.velocity = new Vector2(player.velocity.x, jumpMaxVelocity);
            }
            canDoubleJump = true;
        }
        else if (!grounded && !isAttacking && !isAttackingTrans && canDoubleJump)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button0))
            {
                player.velocity = new Vector2(player.velocity.x, jumpMaxVelocity);
                if (animator.GetCurrentAnimatorStateInfo(0).IsTag("jump"))
                {
                    animator.SetTrigger("doubleJump");
                }
                canDoubleJump = false;
            }
        }
    }

    void Roll()
    {
        if (grounded)
        {
            if (facingRight)
            {
                if ((Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown(KeyCode.Joystick1Button1)) && !animator.GetCurrentAnimatorStateInfo(0).IsTag("roll") && !animator.GetCurrentAnimatorStateInfo(0).IsTag("attack") && !animator.GetCurrentAnimatorStateInfo(0).IsTag("attackTrans"))
                {
                    animator.SetTrigger("roll");
                }
            }
            else
            {
                if ((Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown(KeyCode.Joystick1Button1)) && !animator.GetCurrentAnimatorStateInfo(0).IsTag("roll") && !animator.GetCurrentAnimatorStateInfo(0).IsTag("attack") && !animator.GetCurrentAnimatorStateInfo(0).IsTag("attackTrans"))
                {
                    animator.SetTrigger("roll");
                }
            }
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("roll"))
        {
            isRolling = true;
        }
        else
        {
            isRolling = false;
        }

        if (isRolling)
        {
            if (facingRight)
            {
                player.velocity = new Vector2(rollVelocityX, player.velocity.y);
            }
            else
            {
                player.velocity = new Vector2(-rollVelocityX, player.velocity.y);
            }
            Physics2D.IgnoreLayerCollision(8, 10, true);
        }
        else
        {
            Physics2D.IgnoreLayerCollision(8, 10, false);
        }
    }

    void Attack()
    {
        CheckPressDownAndUp();

        if (Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.Joystick1Button2))
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("attack") && !isRolling)
            {
                animator.SetTrigger("attack");
            }
        }

        if(dart.finishThrow && (Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.Joystick1Button3)))
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("attack") && !isRolling)
            {
                animator.SetTrigger("throw");
                dart.setThrowParameters();
                dart.StartRotationSelection();
                
            }
        }
        if(dart.finishToThrow && (Input.GetAxisRaw("Horizontal") != 0 && (Input.GetKey(KeyCode.K) || Input.GetKey(KeyCode.Joystick1Button3))))
        {
            dart.AdjustDart(Input.GetAxisRaw("Horizontal"));
        }

        // 如果人中途取消了，动画回到正常状态，同时需要调用dart.FinishThrow()
        if((Input.GetKeyUp(KeyCode.K) || Input.GetKeyUp(KeyCode.Joystick1Button3)) && animator.GetCurrentAnimatorClipInfo(0)[0].clip.ToString() == "HS_Attack_Throw (UnityEngine.AnimationClip)")
        {
            animator.SetTrigger("doThrow");
            dart.StartDart();
        }

        if ((Input.GetKeyDown(KeyCode.O) || Input.GetKeyUp(KeyCode.Joystick1Button1)) && animator.GetCurrentAnimatorClipInfo(0)[0].clip.ToString() == "HS_Attack_Throw (UnityEngine.AnimationClip)")
        {
            animator.SetTrigger("cancelThrow");
            dart.FinishThrow();
        }
        
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("attack"))
        {
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("attackTrans"))
        {
            isAttackingTrans = true;
            player.velocity = new Vector2(0, player.velocity.y);
        }
        else
        {
            isAttackingTrans = false;
        }

        if (isAttacking)
        {
            player.velocity = new Vector2(0, 0);
        }
    }

    void CheckPressDownAndUp()
    {
        if (Input.GetAxisRaw("Vertical") > 0)
        {
            isPressingW = true;
            isPressingS = false;
        }
        else if (Input.GetAxisRaw("Vertical") < 0)
        {
            isPressingW = false;
            isPressingS = true;
        }
        else
        {
            isPressingS = false;
            isPressingW = false;
        }
    }

    void CalculateGravityAndVelocity()
    {
        player.gravityScale = (2 * jumpMaxHeight) / (10.5f * Mathf.Pow(jumpMaxTime, 2));
        jumpMaxVelocity = Mathf.Sqrt(2 * player.gravityScale * 10.5f * jumpMaxHeight);
        jumpMinVelocity = Mathf.Sqrt(2 * player.gravityScale * 10.5f * jumpMinHeight);
    }

    private void StateMachine()
    {
        animator.SetBool("running", running);
        animator.SetBool("grounded", grounded);
        animator.SetBool("isPressingS", isPressingS);
        animator.SetBool("isPressingW", isPressingW);
        animator.SetBool("isMovingToFootHold", isMovingToFootHold);
        animator.SetBool("isMovingToPlatform", isMovingToPlatform);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(1, 3.2f, 0.1f));
        Gizmos.DrawWireCube(transform.position + new Vector3(0, -1.6f, 0), new Vector3(1.1f, 0.1f, 0.1f));
    }

    // 爬墙函数们
    public void SetGrabParameters(Vector2 foothold, Vector2 platformPos)
    {
        grabFoothold = foothold;
        grabPlatformPos = platformPos;
    }
    
    public void GrabStart()
    {
        isMovingToFootHold = true;
        isMovingToPlatform = false;
        player.gravityScale = 0f;
        player.velocity = Vector2.zero;
        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), grabLedgeController.ledge, true);
    }

    public void GrabMoveToFootHold()
    {
        transform.position = Vector2.SmoothDamp(player.transform.position, grabFoothold, ref grabVelRef, timeFromInitialPosToFoothold, float.MaxValue, Time.deltaTime);
    }

    public void GrabMoveToPlatform()
    {
        transform.position = Vector2.SmoothDamp(player.transform.position, grabPlatformPos, ref grabVelRef, timeFromFootholdToPlatform, float.MaxValue, Time.deltaTime);
    }

    // 用在动画上的代码
    public void EmitOnceSlashClip1And3()
    {
        slashClip1And3.Emit(1);
    }

    public void EmitOnceSlashClip2()
    {
        slashClip2.Emit(1);
    }

    public void EmitOnceSlashClip4()
    {
        slashClip4.Emit(1);
    }

    public void EmitOnceSlashClip5()
    {
        slashClip5.Emit(1);
    }

    public void ChangeDirection()
    {
        if (Input.GetAxisRaw("Horizontal") < 0 )
        {
            facingRight = false;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if(Input.GetAxisRaw("Horizontal") > 0)
        {
            facingRight = true;
            transform.localScale = new Vector3(1, 1, 1);
        }

    }

    public void CameraShake()
    {
        cameraController.StartShake(new Vector2(shakeAmount * 20, shakeAmount * 5f), new Vector2(shakeAmount * 20 / shakeTimer, shakeAmount * 5f / shakeTimer));
    }

    public void GrabChangeFromFootholdToPlatform()
    {
        isMovingToFootHold = false;
        isMovingToPlatform = true;
    }

    public void GrabEnd()
    {
        CalculateGravityAndVelocity();
        isMovingToFootHold = false;
        isMovingToPlatform = false;
        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), grabLedgeController.ledge, false);
    }

    public void FinishToThrow()
    {
        dart.FinishToThrow();
    }

    public void FinishThrow()
    {
        dart.FinishThrow();
    }
}
