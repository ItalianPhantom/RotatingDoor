using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HeldObject))]
public class SlidingDrawer : MonoBehaviour {

    Transform parent;
    public Transform pointA;
    public Transform pointB;

    Vector3 offset;

    HeldObject heldObject;

	// Use this for initialization
	void Start () {
        heldObject = GetComponent<HeldObject>();
	}
	
	// Update is called once per frame
	void Update () {
        if (parent != null)
        {
            transform.position = ClosestPointOnLine(parent.position) - offset;
        }
	}

    public void PickUp()
    {
        parent = heldObject.parent.transform;

        offset = parent.position - transform.position;
    }

    public void Drop()
    {
        heldObject.simulator.transform.position = transform.position + offset;

        parent = heldObject.simulator.transform;
    }

    // Start of Dot Product script -- closest point on the line from point A to point B
    Vector3 ClosestPointOnLine (Vector3 point)
    {
        Vector3 va = pointA.position + offset;
        Vector3 vb = pointB.position + offset;

        Vector3 vVector1 = point - va; // Offset from point A to the position of the controller
        Vector3 vVector2 = (vb - va).normalized; // Gives the direction from vb to va

        float t = Vector3.Dot(vVector2, vVector1); // Gets the float of the distance between the two points

        if (t <= 0)
        {
            return va;
        }

        if (t >= Vector3.Distance(va, vb)) // If the point is further along the line than the last point, then return the last point
        {
            return vb;
        }

        Vector3 vVector3 = vVector2 * t; // Exact point in local position

        Vector3 vClosestPoint = va + vVector3; // Exact point in world position

        return vClosestPoint;
    }
}
