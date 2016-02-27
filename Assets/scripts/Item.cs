using UnityEngine;
using System.Collections;

public class Item : Mover {

    //variables
    float delay = 2f;
	float delayTime;

	private float itemValue;

	public float Value
	{
		get { return itemValue;}
		set { itemValue = value;}
	}


	// Use this for initialization
	public override void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	public override void Update () {

        //check if the time greater is than the delaytime
		if (Time.time > delayTime) {

            //look if can further forward or can left and right
            if (right && left && forward) {
				decideThreeWays ();
                //look if the can left or right
			} else if (right || left) {

				if(right)
					decideTwoWays("right");
				else
					decideTwoWays("left");
			}
			
			delayTime = Time.time + delay;
		} 

		if (forward)
			movable.position += movable.TransformDirection (Vector3.forward) * speed * Time.deltaTime;


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

	void OnParticleCollision(GameObject other)
	{
		if (other.gameObject.CompareTag ("Player"))
			Destroy (this.gameObject);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
			Destroy (this.gameObject);
	}

	public override void OnDestroy()
	{
		GameController controller = GameObject.FindWithTag ("GameController").GetComponent<GameController> ();
		controller.TextureBools [(int)itemValue] = true;
	}
}
