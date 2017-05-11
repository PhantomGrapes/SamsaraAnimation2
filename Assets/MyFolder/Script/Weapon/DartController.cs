using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartController : MonoBehaviour {
    public LayerMask whatIsEnemy;
    public LayerMask whatIsGround;

    // 检测是否击打到了敌人, 是否打到了墙壁（打到了之后留在墙上）
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(whatIsGround == (whatIsGround | (1 << collision.gameObject.layer)))
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().gravityScale = 0f;
        }

        if (whatIsEnemy == (whatIsEnemy | (1 << collision.gameObject.layer)))
        {
            // 守卫被攻击
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().gravityScale = 0f;
            transform.parent = collision.transform;
        }
    }
}
