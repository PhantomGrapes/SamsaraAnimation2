using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : MonoBehaviour {

    Rigidbody2D frog;
    Animator animator;

    [Header("Movement")]
    public bool canMove;
    public float movementSpeed;
    float refSpeedX;
    public bool facingRight;
    public bool running;

    [Header("Attack")]
    public float attDamage;

	void Start () {
        frog = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        canMove = true;
        facingRight = false;
	}
	
	void Update () {
        Move();
        Attack();

        StateMachine();
	}

    void Move()
    {
        if (canMove)
        {
            float targetVelocityX = Input.GetAxisRaw("Horizontal") * movementSpeed;
            frog.velocity = new Vector2(Mathf.SmoothDamp(frog.velocity.x, targetVelocityX, ref refSpeedX, 0.01f), frog.velocity.y);

            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                facingRight = true;
                running = true;
            }
            else if (Input.GetAxisRaw("Horizontal") < 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
                facingRight = false;
                running = true;
            }
            else
            {
                running = false;
            }
        }
        else
        {
            frog.velocity = new Vector2(0, frog.velocity.y);
        }
    }

    void Attack()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("attack"))
        {
            canMove = false;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                animator.SetTrigger("attack");
            }
            canMove = true;
        }
    }

    void StateMachine()
    {
        animator.SetBool("running", running);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + new Vector3(0, 0.2f, 0), new Vector3(3.5f, 5f, 0.1f));
    }
}
