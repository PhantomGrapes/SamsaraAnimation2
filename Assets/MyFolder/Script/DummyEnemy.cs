using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyEnemy : MonoBehaviour {

    [Header("Blood Effect")]
    public List<GameObject> bloodParticlePrefabs = new List<GameObject>();

    public void PlayBloodParticle(bool facingRight)
    {
        if (facingRight)
        {
            Debug.Log("Player a random blood particle to right!");
            GameObject bloodParticle = (GameObject)Instantiate(bloodParticlePrefabs[Random.Range(0, bloodParticlePrefabs.Count)], transform);
            bloodParticle.transform.localPosition = new Vector3(0, 0, -1);
            bloodParticle.transform.localScale = new Vector3(2.5f, 2.5f, 1);
            bloodParticle.transform.localRotation = Quaternion.Euler(0, 0, Random.Range(-10.0f, 45.0f));
            Destroy(bloodParticle, 3f);
        }
        else
        {
            Debug.Log("Player a random blood particle to left!");
            GameObject bloodParticle = (GameObject)Instantiate(bloodParticlePrefabs[Random.Range(0, bloodParticlePrefabs.Count)], transform);
            bloodParticle.transform.localPosition = new Vector3(0, 0, -1);
            bloodParticle.transform.localScale = new Vector3(-2.5f, 2.5f, 1);
            bloodParticle.transform.localRotation = Quaternion.Euler(0, 0, Random.Range(-45.0f, 10.0f));
            Destroy(bloodParticle, 3f);
        }
    }
}
