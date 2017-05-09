using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabLedgeController : MonoBehaviour {
    private PlayerController player;
    public LayerMask whatIsLedge;
    public float YDistanceFromPlayerToLedge = 0f;
    public float XDistanceFromPlayerToLedge = 0f;
    private float rayLength;
    public bool ledgeDetected;
    public BoxCollider2D ledge; 

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<PlayerController>();

        // set the detector in the same position as player's
        transform.localPosition = Vector2.zero;
        
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        // set the distance from playr to ledge while hanging on the ledge if it is not customized
        if (YDistanceFromPlayerToLedge == 0f)
            YDistanceFromPlayerToLedge = player.height / 2;
        if (XDistanceFromPlayerToLedge == 0f)
            XDistanceFromPlayerToLedge = player.width / 2;
        rayLength = player.width * 1.5f;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right*player.transform.localScale.x, rayLength, whatIsLedge);
        if (hit.collider != null)
        {
            ledge = hit.collider.GetComponent<BoxCollider2D>();
            if (ledge.transform.position.y + ledge.offset.y + ledge.size.y*ledge.transform.localScale.y/2 - hit.point.y < YDistanceFromPlayerToLedge)
                ledgeDetected = true;
            else
                ledgeDetected = false;
        }
        else
            ledgeDetected = false;
        // set position information if ledge is detected
        if (!player.isMovingToFootHold && !player.isMovingToPlatform && ledgeDetected)
        {
            Vector2 footHold = new Vector2(ledge.transform.position.x+ledge.offset.x - (ledge.size.x*ledge.transform.localScale.x+player.width) / 2 * player.transform.localScale.x, ledge.transform.position.y+ledge.offset.y + ledge.size.y*ledge.transform.localScale.y/2 - YDistanceFromPlayerToLedge);
            Vector2 platformPos = new Vector2(ledge.transform.position.x+ledge.offset.x - (ledge.size.x*ledge.transform.localScale.x / 2 - XDistanceFromPlayerToLedge) * player.transform.localScale.x, ledge.transform.position.y+ledge.offset.y + ledge.size.y*ledge.transform.localScale.y / 2 + player.height / 2);
            player.SetGrabParameters(footHold, platformPos);
        }
    }
}
