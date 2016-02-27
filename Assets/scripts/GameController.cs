using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	public Texture[] textures;

	private bool[] texturebools;

	private bool[] openTexture;

	public bool[] TextureBools
	{
		get{ return texturebools;}
		set{ texturebools = value;}
	}

	private List<int> textureNumbers = new List<int>();

	public Transform platform;
	public List<Vector3> platformPos;
	private List<Transform> platforms = new List<Transform>();

	// Use this for initialization
	void Start () {

		texturebools = new bool[textures.Length];
		openTexture = new bool[textures.Length];

		for (int i = 0; i < openTexture.Length; i++)
			openTexture [i] = false;

		for (int i = 0; i < texturebools.Length; i++)
			texturebools [i] = false;

		while (textureNumbers.Count < textures.Length) {

			int rand = Random.Range(0,textures.Length);

			if(!textureNumbers.Contains(rand))
				textureNumbers.Add(rand);
		}
	}

	// Update is called once per frame
	void Update () {

		while (platforms.Count < 3) {

			int count = platforms.Count;

			List<int> platformValues = new List<int>();

			for(int i = 0; i < 5; i++)
			{
				int value = textureNumbers[i];
				platformValues.Add(value);
			}

			Transform newPlatform = Transform.Instantiate(platform, platformPos[count],Quaternion.Euler(270, 0, 0)) as Transform;
			newPlatform.GetComponent<platformScript>().ItemValues = platformValues;
			platforms.Add(newPlatform);

			textureNumbers.RemoveRange(0, 5);
		}
	}

	void OnGUI()
	{
		for (int i = 0; i < textures.Length; i++) {

			Rect posRect;

			if( i < 8)
				posRect = new Rect((i * 50) + 10, 10, 45, 45);
			else
				posRect = new Rect(((i * 50) - (8 * 50)) + 10, 65, 45, 45);

			Rect textRect = new Rect((Screen.width / 2) - (textures[i].width / 2), (Screen.height / 2) - (textures[i].height / 2),
			                         textures[i].width, textures[i].height);
			Rect closeRect = new Rect((Screen.width / 2) - 50, (Screen.height / 2) + (textures[i].height / 2), 100, 50);

			if(TextureBools[i])
			{
				GUI.contentColor = Color.green;
				if(GUI.Button(posRect, i.ToString()))
				{
					openTexture[i] = true;
				}
			}
			else
			{
				GUI.contentColor = Color.red;
				GUI.Box(posRect, i.ToString());
			}

			if(openTexture[i])
			{
				GUI.Box(textRect, textures[i]);

				if(GUI.Button(closeRect, "close"))
				   openTexture[i] = false;
			}
		}
	}
}