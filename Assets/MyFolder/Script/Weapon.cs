using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public PlayerController player;

    [Header("Damage")]
    public float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            Debug.Log("Hit a enemy!");
            collision.GetComponent<DummyEnemy>().PlayBloodParticle(player.GetComponent<PlayerController>().facingRight);

            if (collision.GetComponent<MinionFrog>() != null)
            {
                MinionFrog enemy = collision.GetComponent<MinionFrog>();
                if(player.animator.GetCurrentAnimatorClipInfo(0)[0].clip.ToString() == "HS_Attack_Clip4 (UnityEngine.AnimationClip)")
                {
                    enemy.AddVerticalForce(player.verticalForce);
                }
                else if(player.animator.GetCurrentAnimatorClipInfo(0)[0].clip.ToString() == "HS_Attack_Clip5 (UnityEngine.AnimationClip)")
                {
                    if (player.facingRight)
                    {
                        enemy.AddHorizontalForce(player.horizontalForce);
                    }
                    else
                    {
                        enemy.AddHorizontalForce(-player.horizontalForce);
                    }
                }
            }
        }
    }
}
