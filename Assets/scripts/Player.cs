using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

	private Transform player;
	private CharacterController controller;

	public Transform bomb;

	private float maxSpeed = 10;
	private float speed = 0;

	public Vector3 startPos;

	private List<Transform> bombList = new List<Transform>();

	Animation anim;

	public Transform trigger;

	void Start () {

		player = this.transform;
		controller = player.GetComponent<CharacterController> ();
		anim = player.GetComponent<Animation> ();
		startPos = player.position;
	}
	
	// Update is called once per frame
	void Update () {

		if (trigger.GetComponent<Trigger> ().trigger)
			player.position = startPos;

		if (Input.GetKey (KeyCode.UpArrow)) {

			if (player.eulerAngles.y != 0)
				player.eulerAngles = new Vector3 (0, 0, 0);

			speed = maxSpeed;

		} else if (Input.GetKey (KeyCode.LeftArrow)) {

			if (player.eulerAngles.y != 270)
				player.eulerAngles = new Vector3 (0, 270, 0);

			speed = maxSpeed;

		} else if (Input.GetKey (KeyCode.DownArrow)) {

			if (player.eulerAngles.y != 180)
				player.eulerAngles = new Vector3 (0, 180, 0);

			speed = maxSpeed;

		} else if (Input.GetKey (KeyCode.RightArrow)) {

			if (player.eulerAngles.y != 90)
				player.eulerAngles = new Vector3 (0, 90, 0);

			speed = maxSpeed;
		} else {
			speed = 0;
		}

		if (Input.GetKeyDown (KeyCode.Space)) {

			if(bombList.Count < 2)
			{
				Vector3 bombPos = new Vector3(player.position.x,player.position.y + 3,player.position.z);
				Transform newBomb = Transform.Instantiate(bomb, bombPos, Quaternion.identity) as Transform;
				bombList.Add(newBomb);
			}
		}

		for (int i = 0; i < bombList.Count; i++) {
			if(bombList[i] == null)
				bombList.RemoveAt(i);
		}

		if (speed > 0)
			anim.Play();
		else
			anim.Stop();

		controller.SimpleMove(player.TransformDirection(Vector3.forward) * speed);
	}
}
