using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public bool inBattle = false;

	public float aggroRangeX;
	public float aggroRangeY;
	public float targetDistance;
	protected Character target;
	// Use this for initialization
	void Start () {
		target = FindObjectOfType<Character> ();
	}
	
	// Update is called once per frame
	void Update () {
		targetDistance = (target.transform.position - transform.position).magnitude;
		float targetDistanceX = Mathf.Abs( target.transform.position.x - transform.position.x);
		float targetDistanceY = Mathf.Abs(target.transform.position.y - transform.position.y);

		inBattle = (targetDistanceX < aggroRangeX && targetDistanceY < aggroRangeY);
		
	}


}
