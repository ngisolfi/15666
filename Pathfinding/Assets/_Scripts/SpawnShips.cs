using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnShips : MonoBehaviour {

	public GameObject alien_ship;
	public GameObject human_ship;
	protected GameObject ship;
	
	public int num_ships = 0;
	private List<GameObject> active_ships;

	// Use this for initialization
	void Awake () {
		if (Network.isServer)
			ship = human_ship;
		else
			ship = alien_ship;
			
		active_ships = new List<GameObject>();
	}
	
	void Update()
	{
		while (active_ships.Count < num_ships)
			Spawn();
	}
	
	public void Spawn()
	{
		// Determine a spawn location and instantiate a new ship of the player's type
		Transform spawn_location = GetSpawnLocation();
		GameObject spawned = (GameObject)Network.Instantiate(ship, 
															 spawn_location.position, 
															 spawn_location.rotation, 
															 networkView.GetInstanceID());
		
		// If the spawn was successful, set the camera to point to the spawned object
		if (spawned)
		{
			if(Camera.main)
				Camera.main.enabled = false;
			Transform spawned_camera = transform.Find("Camera");
			if (spawned_camera)
			{
				spawned_camera.camera.GetComponent<SmoothFollowCSharp>().target = spawned.transform;
				spawned_camera.camera.enabled = true;
			}
			
			active_ships.Add(spawned);
		}
	}
	
	private Transform GetSpawnLocation(){
		GameObject[] activeShips = GameObject.FindGameObjectsWithTag(ship.tag);
		List<Transform> spawnPoints = new List<Transform>();
		foreach(Transform spawnPoint in this.transform)
		{
			if(spawnPoint.name == "SpawnPoint")
				spawnPoints.Add(spawnPoint);
		}
		while(spawnPoints.Count > 0)
		{
			int index = (int)(Random.value * spawnPoints.Count);
			if(index == spawnPoints.Count)
				index--;
			Transform spawnPoint = spawnPoints[index];
			bool open = true;
			foreach(GameObject activeShip in activeShips)
			{
				if((activeShip.transform.position-spawnPoint.transform.position).sqrMagnitude < 4f*this.ship.collider.bounds.extents.sqrMagnitude)
				{
					open = false;
					break;
				}
			}
			if(open){
				return spawnPoint;
			}
			spawnPoints.RemoveAt(index);
		}
		return null;
	}
}
