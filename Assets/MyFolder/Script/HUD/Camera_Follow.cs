using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour {

	public Transform target;
	Camera myCam;

	public bool zoom;

	public float zoomRatio;

	public float yAlter;
	public float defaultRatio;

	// Use this for initialization
	void Start ()
	{
		target = FindObjectOfType<Character> ().transform;
		myCam = GetComponent<Camera> ();
		defaultRatio = myCam.orthographicSize;
	}

	// Update is called once per frame
	void Update ()
	{

		DecideZoom ();




	}


	void DecideZoom ()
	{
		Enemy[] enemies = FindObjectsOfType<Enemy> ();
		bool pickedEnemy = false;
		Enemy targetEnemy = null;
		if (enemies.Length > 0) {
			foreach (Enemy e in enemies) {
				if (e.inBattle) {
					targetEnemy = e;
					break;
				}
			}
		}

		if (target) {
			if (targetEnemy) {

				zoom = true;
				zoomRatio = Mathf.Max (targetEnemy.targetDistance / 2f, defaultRatio);

				Vector3 enemyPosition = targetEnemy.transform.position;
				Vector3 camPosition = new Vector3 ((target.position.x + enemyPosition.x) / 2f, (target.position.y + enemyPosition.y) / 2f + yAlter
					, target.position.z + 20f);

				transform.position = new Vector3 (Mathf.Lerp (transform.position.x, camPosition.x, 0.05f), Mathf.Lerp (transform.position.y, camPosition.y, 0.05f), -10f);
			} else {
				zoomRatio = 1f;
				zoom = false;
				transform.position = new Vector3 (Mathf.Lerp (transform.position.x, target.position.x, 0.05f), Mathf.Lerp (transform.position.y, target.position.y + yAlter, 0.05f), -10f);
			}


			if (zoom) {
				Zoom ();
			} else {
				Unzoom ();
			}

		}
	}

	void Zoom ()
	{
		myCam.orthographicSize = Mathf.Lerp (myCam.orthographicSize, zoomRatio, 0.05f);
	}


	void Unzoom ()
	{
		myCam.orthographicSize = Mathf.Lerp (myCam.orthographicSize, defaultRatio, 0.05f);
	}
}
