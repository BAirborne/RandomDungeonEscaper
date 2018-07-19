using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject Player;

	// Use this for initialization
	void Start () {
        transform.position = Player.transform.position + new Vector3(-1.5f, 4, -1.5f);
        //Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Player == null)
        {
            return;
        }
        transform.position = Player.transform.position + new Vector3(-3f, 8, -3f);
        //transform.position = Player.transform.forward *-2f+ Player.transform.right * 0.5f + Player.transform.up * 0.5f + Player.transform.position;
        //transform.eulerAngles = Player.transform.eulerAngles;
    }
}
