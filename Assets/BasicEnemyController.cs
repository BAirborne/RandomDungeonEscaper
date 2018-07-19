using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemyController : MonoBehaviour {

    public GameObject Player;
    public float Health;

	// Use this for initialization
	void Start ()
    {
        Player = GameObject.FindWithTag("player");
    }
	
	// Update is called once per frame
	void Update () {
        if (Player == null)
        {
            return;
        }
        GetComponent<NavMeshAgent>().SetDestination(Player.transform.position);
        if (Vector3.Distance(Player.transform.position, transform.position) < 0.6f)
        {
            Player.GetComponent<PlayerController>().Disable();
        }
	}

    void OnDestroy()
    {
        enemySpawner.numberOfEnemies -= 1;
        Debug.Log("Pow");
    }
}
