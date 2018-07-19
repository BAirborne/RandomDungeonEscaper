using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class basicBulletScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Destroy(this.gameObject,2);
    }
	
	// Update is called once per frame
	void Update () {
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BasicEnemyController>())
        {
            other.GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity;
            other.GetComponent<BasicEnemyController>().Health -= 5;
            Destroy(this.gameObject);
            if (other.GetComponent<BasicEnemyController>().Health <= 0)
            {
                Destroy(other.GetComponent<BasicEnemyController>());
                Destroy(other.GetComponent<NavMeshAgent>());
                other.GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity;
                other.gameObject.AddComponent<DeathScript>();
                Destroy(other.gameObject, 5);
            }
        }
        else
        {
            if (other.GetComponent<DeathScript>() == null) {
                Destroy(this.gameObject);
            }
        }
    }
}
