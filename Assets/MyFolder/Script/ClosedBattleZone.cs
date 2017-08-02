using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosedBattleZone : MonoBehaviour {

	public bool closed = false;
	public List<GameObject> enemies;
	public float closeDelay;
	public float openDelay;
	public bool enteredBefore = false;
	public Collider2D door;

	Character player;


	void Start(){
		player = FindObjectOfType<Character> ();
		door.gameObject.SetActive (false);
	}

	void Update(){
		foreach (GameObject g in enemies) {
			if (g == null) {
				enemies.Remove (g);
			}
		}
		if (enemies.Count == 0) {
			StartCoroutine (Open ());
		}
	}


	void OnTriggerEnter2D(Collider2D other){

		if (other.gameObject.GetComponents<Enemy> ().Length != 0) {
			enemies.Add (other.gameObject);
		}

		if (other == player.GetComponent<Collider2D> () && !enteredBefore) {
			StartCoroutine (Close ());
			enteredBefore = true;
		}
	}

	// 以后改成动画
	IEnumerator Close(){
		print ("here");
		yield return new WaitForSeconds (closeDelay);

		closed = true;
		door.gameObject.SetActive (true);
	}

	// 以后改成动画
	IEnumerator Open(){
		yield return new WaitForSeconds (openDelay);
		closed = false;
		door.gameObject.SetActive (false);
	}

}
