using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBehavior : MonoBehaviour {

    bool swinging = false;
    public GameObject player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        var parentY = transform.parent.eulerAngles.y;
        var relativeY = Mathf.Abs(transform.eulerAngles.y - parentY) % 360;
        if (relativeY < 315 && relativeY > 45)
        {
            swinging = false;
            transform.eulerAngles = new Vector3(0, parentY, 0) ;
        }
            if (swinging)
        {
            this.transform.eulerAngles -= new Vector3(0, 5, 0);
        }
	}

    public void Attack()
    {
        var parentY = transform.parent.eulerAngles.y;
        if (!swinging)
        {
            //player.GetComponent<Rigidbody>().AddForce(transform.parent.forward * 4000);
            swinging = true;
            this.transform.eulerAngles = new Vector3(0, parentY + 45, 0);
        }

    }
}
