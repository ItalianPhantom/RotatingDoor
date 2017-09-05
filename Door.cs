using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HeldObject))]
[RequireComponent(typeof(HingeJoint))]

public class Door : MonoBehaviour {

    public Transform Parent;

    public float MinRot;
    public float MaxRot;

	// Use this for initialization
	void Start () {
        JointLimits limits = new JointLimits();
        limits.min = MinRot;
        limits.max = MaxRot;
        GetComponent<HingeJoint>().limits = limits;
        GetComponent<HingeJoint>().useLimits = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(Parent != null)
        {
            Vector3 targetDelta = Parent.position - transform.position; // Direction of the controller from this object
            targetDelta.y = 0;

            float AngleDiff = Vector3.Angle(transform.forward, targetDelta);

            Vector3 cross = Vector3.Cross(transform.forward, targetDelta);

            GetComponent<Rigidbody>().angularVelocity = cross * AngleDiff * 50f;
        }
	}

    public void PickUp ()
    {
        Parent = GetComponent<HeldObject>().parent.transform;
    }

    public void Drop ()
    {
        Parent = null;
    }
}
