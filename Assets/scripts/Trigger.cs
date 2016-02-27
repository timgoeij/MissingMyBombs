using UnityEngine;
using System.Collections;

public class Trigger : MonoBehaviour {

	public bool trigger = false;

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
			trigger = true;
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
			trigger = false;
	}
}
