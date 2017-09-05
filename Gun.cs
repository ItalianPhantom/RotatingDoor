using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HeldObject))]
public class Gun : MonoBehaviour {

    public GameObject projectile;
    public float power;
    public Transform firepoint;

    public Valve.VR.EVRButtonId shootButton;

    public bool automatic;
    public float coolDownTime;
    float time;

    HeldObject heldObj; // Purely for optimization

	// Use this for initialization
	void Start () {
        heldObj = GetComponent<HeldObject>();
	}
	
	// Update is called once per frame
	void Update () {
        if (time > 0f)
        {
            time -= Time.deltaTime;
        }
        else
        {
            if (heldObj.parent != null && ((heldObj.parent.controller.GetPressDown(shootButton) && !automatic) || (heldObj.parent.controller.GetPress(shootButton) && automatic))) // Check if the controller is receiving input from the button designated to shoot
            {
                time = coolDownTime;
                GameObject proj = (GameObject)Instantiate(projectile, firepoint.position, firepoint.rotation); // Spawns a projectile at the point and rotation you choose
                proj.GetComponent<Rigidbody>().velocity = firepoint.forward * power; // Fires projectile with specified velocity
            }
        }
	}
}
