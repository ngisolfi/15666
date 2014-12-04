using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Sensor : MonoBehaviour {
	[HideInInspector]
	public List<Transform> enemies;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void clearSenses()
	{
		enemies.Clear();
	}

	public void senseEnemy(Transform enemy)
	{
		enemies.Add(enemy);
	}

	public bool inSight
	{
		get{
			return enemies.Count > 0;
		}
	}
}
