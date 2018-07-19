using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehavior : MonoBehaviour {

    public GameObject Projectile;
    float firerate = 6;
    float timeToFire = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (timeToFire > 0)
        {
            timeToFire -= 1;
        }
    }

    public void Attack()
    {
        if (timeToFire <= 0)
        {
            var proj = Instantiate(Projectile, transform.parent.position + transform.forward * 0.4f, new Quaternion(0, 0, 0, 0));
            proj.GetComponent<Rigidbody>().velocity = transform.forward * 20;
            timeToFire = 60 / firerate;
        }

    }
}
