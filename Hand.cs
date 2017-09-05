using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attached to each controller

[RequireComponent(typeof(Controller))]
public class Hand : MonoBehaviour {

    GameObject heldObject;
    Controller controller;

    public Valve.VR.EVRButtonId pickUpButton;
    public Valve.VR.EVRButtonId dropButton;
    

	// Use this for initialization
	void Start () {
        controller = GetComponent<Controller>();
	}
	
	// Update is called once per frame
	void Update () {

        if (heldObject) // Check if there is a held object already in hand
        {
            switch (controller.CurrentTouchPosition()) // Move an object around while holding it using the touch pad
            {
                case Controller.TouchPosition.Up:
                    heldObject.transform.localPosition += Vector3.forward * Time.deltaTime;
                    print("up");
                    break;
                case Controller.TouchPosition.Down:
                    heldObject.transform.localPosition -= Vector3.forward * Time.deltaTime;
                    print("down");
                    break;
                case Controller.TouchPosition.Left:
                    heldObject.transform.localPosition -= Vector3.right * Time.deltaTime;
                    print("left");
                    break;
                case Controller.TouchPosition.Right:
                    heldObject.transform.localPosition += Vector3.right * Time.deltaTime;
                    print("right");
                    break;
                default:
                    print("off");
                    break;
            }

            if ((controller.controller.GetPressUp(pickUpButton) && heldObject.GetComponent<HeldObject>().dropOnRelease) || (controller.controller.GetPressDown(dropButton) && !heldObject.GetComponent<HeldObject>().dropOnRelease)) // Check if the grip is released
            {
                heldObject.GetComponent<HeldObject>().Drop();
                heldObject = null;
            }
        }
        else
        {
            if (controller.controller.GetPressDown(pickUpButton)) // Check if the grip is being pressed
            {
                Collider[] cols = Physics.OverlapSphere(transform.position, 0.1f); // Create an array of colliders of everything that is within a small radius (0.1f) of the controller

                foreach (Collider col in cols) // Check each collider to see if they're ready to be a held object
                {
                    if (heldObject == null && col.GetComponent<HeldObject>() && col.GetComponent<HeldObject>().parent == null) // Checks if hand is empty, object is branded as a holdable object, and if that object has no parent
                    {
                        heldObject = col.gameObject;
                        heldObject.GetComponent<HeldObject>().parent = controller; // Sets the object's parent to the controller
                        heldObject.GetComponent<HeldObject>().PickUp();                       
                    }
                }
            }
        }
	}
}
