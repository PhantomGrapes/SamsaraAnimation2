using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionFrog : MonoBehaviour {

    Rigidbody2D frog;

    private void Start()
    {
        frog = GetComponent<Rigidbody2D>();    
    }

    public void AddHorizontalForce(float force)
    {
        frog.AddForce(new Vector2(force, 0));
    }

    public void AddVerticalForce(float force)
    {
        frog.AddForce(new Vector2(0, force));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(1.7f, 2.7f, 0.1f));
    }
}
