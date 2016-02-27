using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour 
{
	// Use this for initialization
	protected Transform movable;
	public float speed;

	protected bool forward = false;
	protected bool right = false;
	protected bool left = false;

	private Ray forRay;
	private Ray rightRay;
	private Ray leftRay;

	public virtual void Start () 
	{
		movable = this.transform;
	}
	
	// Update is called once per frame
	public virtual void Update () 
	{
		forRay = new Ray (movable.position, movable.TransformDirection (Vector3.forward));
		rightRay = new Ray (movable.position, movable.TransformDirection (Vector3.right));
		leftRay = new Ray (movable.position, movable.TransformDirection (Vector3.left));

		RaycastHit hit;

		if (Physics.Raycast (forRay, out hit)) {

			if(hit.distance < 8.75f && (hit.transform.tag == "crate" || (hit.transform.name == "platform" || hit.transform.tag == "platform")))
				forward = false;
			else
				forward = true;

		} else {
			forward = true;
		}

		if (Physics.Raycast (rightRay, out hit)) {

			if(hit.distance < 8.75f && (hit.transform.tag == "crate" || (hit.transform.name == "platform" || hit.transform.tag == "platform")))
				right = false;
			else
				right = true;

		} else {
			right = true;
		}

		if (Physics.Raycast (leftRay, out hit)) {

			if(hit.distance < 8.75f && (hit.transform.tag == "crate" || (hit.transform.name == "platform" || hit.transform.tag == "platform")))
				left = false;
			else
				left = true;

		} else {
			left = true;
		}
	}

	public virtual void OnDestroy()
	{

	}
}