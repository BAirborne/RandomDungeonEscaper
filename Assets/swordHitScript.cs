using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class swordHitScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("it got hit");
        if (other.GetComponent<BasicEnemyController>())
        {
            other.GetComponent<BasicEnemyController>().Health -= 5;
            if (other.GetComponent<BasicEnemyController>().Health <= 0)
            {
                Destroy(other.GetComponent<BasicEnemyController>());
                Destroy(other.GetComponent<NavMeshAgent>());
                other.gameObject.AddComponent<DeathScript>();
                Destroy(other.gameObject, 5);
            }
        }
        else
        {
        }
    }
}
