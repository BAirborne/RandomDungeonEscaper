using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameObject raycam;
    //public GameObject currentWeapon;
    public GameObject currentWeapon2;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Move();
        Aim();
        if (Input.GetKey("mouse 0"))
        {
            Attack();
        }
        if (Input.GetKey("mouse 1"))
        {
            Attack2();
        }


    }

    void Move()
    {
        Vector3 vertVal = new Vector3(Input.GetAxisRaw("Vertical"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 horizVal = new Vector3(Input.GetAxisRaw("Horizontal"), 0, -Input.GetAxisRaw("Horizontal"));
        Vector3 totalVal = Vector3.Normalize(vertVal + horizVal) * 400;
        //Vector3 totalVal = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")) * 400;
        //Vector3 toLocal = transform.TransformVector(totalVal);
        GetComponent<Rigidbody>().AddForce(totalVal);


    }

    void Aim()
    {
        Ray mouseRay = raycam.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(mouseRay, out hit))
        {
            transform.LookAt(hit.point);
            transform.eulerAngles -= new Vector3(transform.eulerAngles.x, 0, 0);
            currentWeapon2.transform.LookAt(hit.point + new Vector3(0,.05f,0));
        }

        //Vector3 turnAngle = new Vector3(0,Input.GetAxisRaw("Mouse X"),0)*2;
        //transform.eulerAngles += turnAngle;

    }

    void Attack()
    {
        currentWeapon2.GetComponent<SwordBehavior>().Attack();
    }

    void Attack2()
    {
        currentWeapon2.GetComponent<WeaponBehavior>().Attack();
    }

    public void Disable()
    {
        Destroy(this.gameObject);
    }
}
