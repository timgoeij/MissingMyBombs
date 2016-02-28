using UnityEngine;
using System.Collections;

public class Elevator : MonoBehaviour {

    //variables
    public Transform player;
	public Transform trigger;

	private Transform elevator;

	private bool Tosafe = false;
	private bool ToStage1 = false;
	private bool ToStage2 = false;
	private bool ToStage3 = false;

	// Use this for initialization
	void Start () {

        //the transform of the elevator
        elevator = this.transform;
	}
	
	// Update is called once per frame
	void Update () {

        /*if a bool for each of the stages or the safezone is true the elevator goes to that stage. if the elevator is at the position of a stage
        or the safezone the will stop move.*/

        if (Tosafe) {
			elevator.position = Vector3.MoveTowards(elevator.position, new Vector3(0, 50, 0), 0.1f);

			if(Mathf.Round(elevator.position.y) == 50)
				Tosafe = false;

		} else if (ToStage1) {
			elevator.position = Vector3.MoveTowards(elevator.position, new Vector3(0, 0, 0), 0.1f);
			if(Mathf.Round(elevator.position.y) == 0)
				ToStage1 = false;

		} else if (ToStage2) {
			elevator.position = Vector3.MoveTowards(elevator.position, new Vector3(0, -50, 0), 0.1f);

			if(Mathf.Round(elevator.position.y) == -50)
				ToStage2 = false;
		} else if (ToStage3) {
			elevator.position = Vector3.MoveTowards(elevator.position, new Vector3(0, -100, 0), 0.1f);

			if(Mathf.Round(elevator.position.y) == -100)
				ToStage3 = false;

		}
        //if the player is at the safezone and the elevator is at one of the stages the elevator goes automatically to the safzone
        else if (player.position.y > 50) {
			Tosafe = true;
		}
	}

	void OnGUI()
	{
        /*if the player hits the trigger of the elevator creata an UI with the destinations of the elevator*/
        if (trigger.GetComponent<Trigger> ().trigger) {

			Rect safeRect = new Rect((Screen.width / 2) - 50, (Screen.height / 2) - 25, 100, 50);
			Rect stage1Rect = new Rect((Screen.width / 2) - 50, (Screen.height / 2) + 50, 100, 50);
			Rect stage2Rect = new Rect((Screen.width / 2) - 50, (Screen.height / 2) + 100, 100, 50);
			Rect stage3Rect = new Rect((Screen.width / 2) - 50, (Screen.height / 2) + 150, 100, 50);

            //if the player click on the button of the destination the boolean of the destination will be true
            if (GUI.Button(safeRect, "safeZone"))
			{
				Tosafe = true;
			}

			if(GUI.Button(stage1Rect, "stage 1"))
			{
				ToStage1 = true;
			}

			if(GUI.Button(stage2Rect, "stage 2"))
			{
				ToStage2 = true;
			}

			if(GUI.Button(stage3Rect, "stage 3"))
			{
				ToStage3 = true;
			}
		}
	}
}
