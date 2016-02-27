using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class platformScript : MonoBehaviour 
{
	public Transform crate;
	public Transform enemy;
	public Transform item;
	public float difficulty;

	private bool cleared = false;

	public bool Cleared
	{
		get{ return cleared;}
	}

	private float startX = -124;
	private float startZ = 124;
	
	private Transform Platform;
	
	private int[,] platform = new int[15,15]
	{
		{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
		{0,1,0,1,0,1,0,1,0,1,0,1,0,1,0},
		{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
		{0,1,0,1,0,1,0,1,0,1,0,1,0,1,0},
		{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
		{0,1,0,1,0,1,0,1,0,1,0,1,0,1,0},
		{0,0,0,0,0,0,0,1,0,0,0,0,0,0,0},
		{0,1,0,1,0,1,1,1,1,1,0,1,0,1,0},
		{0,0,0,0,0,0,0,1,0,0,0,0,0,0,0},
		{0,1,0,1,0,1,0,1,0,1,0,1,0,1,0},
		{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
		{0,1,0,1,0,1,0,1,0,1,0,1,0,1,0},
		{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
		{0,1,0,1,0,1,0,1,0,1,0,1,0,1,0},
		{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
	};
	
	private float multiply = 17.5f;

	private float x = 0;
	private float z = 0;
	
	private List<Transform> crateList = new List<Transform>();
	private List<Transform> enemyList = new List<Transform>();
	private List<Transform> itemList = new List<Transform>();
	private List<int> itemValues = new List<int> ();

	public List<int> ItemValues {
		set{ itemValues = value;}
	}
	
	// Use this for initialization
	void Start() 
	{
		Platform = transform;

		createCrates();
		createEnemies();
		createItems();
	}
	
	// Update is called once per frame
	void Update () 
	{
		for(int c = 0; c < crateList.Count; c++)
		{
			if(crateList[c] == null)
				crateList.RemoveAt(c);
		}
		
		for(int e = 0; e < enemyList.Count; e++)
		{
			if(enemyList[e] == null)
				enemyList.RemoveAt(e);
		}

		for (int i = 0; i < itemList.Count; i++) 
		{
			if(itemList[i] == null)
			   itemList.RemoveAt(i);
		}

		if (itemList.Count == 0 && enemyList.Count == 0 && crateList.Count == 0)
			cleared = true;
	}
	void createCrates()
	{
		for(int i = 0; i < platform.GetLength(0); i++)
		{
			for(int j = 0; j < platform.GetLength(1); j++)
			{
				int place = platform[i, j];

				if(place == 0)
				{	
					x = startX + (i * multiply);
					z = startZ - (j * multiply);
					float rand = Random.value;

					if(rand < difficulty)
					{
						Transform newCrate = Transform.Instantiate(crate,new Vector3(x, Platform.position.y + 9f,z),Quaternion.identity) as  Transform;
						newCrate.tag = "crate";
						newCrate.parent = Platform;
						crateList.Add(newCrate);
					}
					else
					{
						platform[i, j] = 2;
					}
				}
			}
		}
	}

	void createEnemies()
	{
		for (int i = 0; i < platform.GetLength(0); i++) 
		{
			for(int j = 0; j < platform.GetLength(1); j++)
			{
				int place = platform[i, j];

				if(place == 2)
				{	
					x = startX + (i * multiply);
					z = startZ - (j * multiply);
					float rand = Random.value;
					
					if(rand < (difficulty - 0.25f))
					{
						Transform newEnemy = Transform.Instantiate(enemy,new Vector3(x,Platform.position.y + 1.08f,z),Quaternion.identity) as  Transform;
						newEnemy.tag = "enemy";
						newEnemy.parent = Platform;
						enemyList.Add(newEnemy);
					}
					else
					{
						platform[i, j] = 3;
					}
				}
			}
		}
	}

	void createItems()
	{

		while (itemValues.Count > 0) {

			for (int i = 0; i < platform.GetLength(0); i++) 
			{
				for(int j = 0; j < platform.GetLength(1); j++)
				{
					int place = platform[i, j];
					
					if(place == 3)
					{	
						x = startX + (i * multiply);
						z = startZ - (j * multiply);
						float rand = Random.value;
						
						if(itemValues.Count == 0)
							break;

						if(rand < (difficulty))
						{
							Transform newItem = Transform.Instantiate(item,new Vector3(x,Platform.position.y + 12f,z),Quaternion.identity) as  Transform;
							newItem.GetComponent<Item>().Value = itemValues[0];
							newItem.parent = Platform;
							itemValues.RemoveAt(0);
							itemList.Add(newItem);
							platform[i, j] = 4;
						}
					}
				}
			}
		}
	}
}