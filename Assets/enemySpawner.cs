using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour {

    public static int numberOfEnemies = 0;
    public GameObject enemy;
    public GameObject Player;
    int frameCount = 0;

	// Use this for initialization
	void Start () {
        frameCount = 200;
        Player = GameObject.FindWithTag("player");
    }
	
	// Update is called once per frame
	void Update () {
        if (Player == null)
        {
            return;
        }
        var distanceToPlayer = Vector3.Distance(transform.position, Player.transform.position);
        if (distanceToPlayer < 20 && distanceToPlayer>10)
        if(frameCount == 0 && numberOfEnemies < 50) {
            frameCount = 200;
            Instantiate(enemy,transform.position,new Quaternion(0,0,0,0));
            numberOfEnemies += 1;
            enemy.GetComponent<BasicEnemyController>().Player = Player;
        }
        if (frameCount > 0)
        {
            frameCount -= 1;
        }
    }
}
