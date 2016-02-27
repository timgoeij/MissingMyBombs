using UnityEngine;
using System.Collections;

public class Cam : MonoBehaviour {

	public Transform player;
	private Transform cam;

	// Use this for initialization
	void Start () {
		cam = this.transform;
	}
	
	// Update is called once per frame
	void Update () {
	
		Vector3 pos = new Vector3 (player.position.x, player.position.y + 45, player.position.z);
		cam.position = pos;
	}
}
