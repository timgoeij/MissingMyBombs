using UnityEngine;
using System.Collections;

public class flybot : Mover {

	float delay = 4f;
	float delayTime;
	CharacterController controller;
    Animation anim;

	// Use this for initialization
	public override void Start () {
		base.Start();

		controller = movable.GetComponent<CharacterController> ();
		anim = movable.GetComponent<Animation> ();
	}
	
	// Update is called once per frame
	public override void Update () {

		if (Time.time > delayTime) {
			
			if (right && left && forward) {
				decideThreeWays ();
			} else if (right || left) {
				
				if(right)
					decideTwoWays("right");
				else
					decideTwoWays("left");
			}
			
			delayTime = Time.time + delay;
		} 
		
		if (forward) {
			anim.Play();
			controller.SimpleMove (movable.TransformDirection (Vector3.forward) * speed);
		} else
			anim.Stop ();

		base.Update ();
	}

	private void decideTwoWays(string directions)
	{
		float randomValue = Random.value;
		
		if (randomValue < 0.5f) {
			switch(directions)
			{
			case "right": movable.eulerAngles = new Vector3 (movable.eulerAngles.x, movable.eulerAngles.y + 90, movable.eulerAngles.z);
				break;
			case "left": movable.eulerAngles = new Vector3 (movable.eulerAngles.x, movable.eulerAngles.y - 90, movable.eulerAngles.z);
				break;
			}
		}
	}
	
	private void decideThreeWays()
	{
		float randomValue = Random.value;
		
		if (randomValue < 0.33f)
			movable.eulerAngles = new Vector3 (movable.eulerAngles.x, movable.eulerAngles.y + 90, movable.eulerAngles.z);
		else if (randomValue > 0.66f)
			movable.eulerAngles = new Vector3 (movable.eulerAngles.x, movable.eulerAngles.y - 90, movable.eulerAngles.z);
	}

	private void onControllerColiderHit(ControllerColliderHit hit)
	{
		if(hit.transform.CompareTag("Player"))
		{
			hit.transform.position = hit.transform.GetComponent<Player>().startPos;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
			other.transform.position = other.transform.GetComponent<Player>().startPos;
	}
	
}
