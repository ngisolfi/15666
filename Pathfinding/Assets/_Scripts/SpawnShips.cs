﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnShips : MonoBehaviour {

	public GameObject ship;
	public int maxActive;
	public int maxGenerated;
	public string endMessage;
	private int numGenerated;
	private bool finished;

	// Use this for initialization
	void Start () {
		numGenerated = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(!finished && Time.frameCount % 10 == 0)
			spawn();
	}
	
	void OnGUI () {
		if(finished){
			GameObject[] activeShips = GameObject.FindGameObjectsWithTag(ship.tag);
			if(activeShips.Length == 0){
				GUI.Box(new Rect(Screen.width*0.25f,Screen.height*0.25f,Screen.width*0.5f,Screen.height*0.5f),endMessage);
				if(GUI.Button(new Rect(Screen.width*0.4f,Screen.height*0.4f,Screen.width*0.2f,Screen.height*0.2f),"Play Again?"))
					Application.LoadLevel(Application.loadedLevel);
			}
				
		}
	}
	
	void spawn(){
		if(numGenerated <= maxGenerated){
			GameObject[] activeShips = GameObject.FindGameObjectsWithTag(ship.tag);
			if(activeShips.Length < maxActive){
				List<Transform> spawnPoints = new List<Transform>();
				foreach(Transform spawnPoint in this.transform){
					spawnPoints.Add(spawnPoint);
				}
				while(spawnPoints.Count > 0){
					int index = (int)(Random.value * spawnPoints.Count);
					if(index == spawnPoints.Count)
						index--;
					Transform spawnPoint = spawnPoints[index];
					bool open = true;
					foreach(GameObject activeShip in activeShips){
						if((activeShip.transform.position-spawnPoint.transform.position).sqrMagnitude < 4f*this.ship.collider.bounds.extents.sqrMagnitude){
							open = false;
							break;
						}
					}
					if(open){
						Instantiate(ship,spawnPoint.position,spawnPoint.rotation);
						numGenerated++;
						if(numGenerated >= maxGenerated)
							finished = true;
						break;
					}
					spawnPoints.RemoveAt(index);
				}
			}
		}
	}
}
