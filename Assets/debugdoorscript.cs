﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debugdoorscript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(transform.eulerAngles.y);
	}
}
