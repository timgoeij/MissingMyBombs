using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {

	void Update()
	{
		Destroy (this.transform.parent.gameObject, 11f);
	}

	void OnParticleCollision(GameObject other)
	{
		if (other.CompareTag ("crate") || other.CompareTag ("enemy"))
			Destroy (other.gameObject);

		if (other.CompareTag ("Player"))
			other.transform.position = other.transform.GetComponent<Player>().startPos;
	}
}