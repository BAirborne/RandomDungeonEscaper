using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBoyScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<BasicEnemyController>().Health = 100;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
