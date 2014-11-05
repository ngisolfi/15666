using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnShips : MonoBehaviour {

	public GameObject ship;
	public int maxActive;
	public int maxGenerated;
	public string endMessage;
	protected int numGenerated = 0;
	protected bool finished = false;
	protected bool running = false;
	protected int numActive = 0;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		GameObject[] activeShips = GameObject.FindGameObjectsWithTag(ship.tag);
		numActive = activeShips.Length;
		if(numGenerated >= maxGenerated)
			finished = true;
		if(running && !finished && Time.frameCount % 10 == 0){
			Transform location = findSpawnLocation();
			if(location){
				spawn(location);
//				if(Network.isServer){
//					spawn(location);
//					networkView.RPC("spawn",RPCMode.Others,location);
//				}else{
//					networkView.RPC("spawn",RPCMode.All,location);
//				}
			}
		}
	}

	void OnGUI () {
//		if(finished){
//			if(numActive == 0){
//				GUI.Box(new Rect(Screen.width*0.25f,Screen.height*0.25f,Screen.width*0.5f,Screen.height*0.5f),endMessage);
//				if(GUI.Button(new Rect(Screen.width*0.4f,Screen.height*0.4f,Screen.width*0.2f,Screen.height*0.2f),"Play Again?"))
//					GameObject.Find("StartGUI").GetComponent<startGame>().StartGame();
////					Application.LoadLevel(Application.loadedLevel);
//			}
//		}
	}

	public bool isDepleted()
	{
		return finished && numActive == 0;
	}

	public void BeginSpawning()
	{
		foreach(GameObject s in GameObject.FindGameObjectsWithTag(ship.tag)){
			Network.Destroy(s);
		}
		numGenerated = 0;
		finished = false;
		running = true;
	}

	protected Transform findSpawnLocation(){
		if(numGenerated <= maxGenerated){
			GameObject[] activeShips = GameObject.FindGameObjectsWithTag(ship.tag);
			if(activeShips.Length < maxActive){
				List<Transform> spawnPoints = new List<Transform>();
				foreach(Transform spawnPoint in this.transform){
					if(spawnPoint.name == "SpawnPoint")
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
						numGenerated++;
						if(numGenerated >= maxGenerated)
							finished = true;
						return spawnPoint;
					}
					spawnPoints.RemoveAt(index);
				}
			}
		}
		return null;
	}

	[RPC]
	protected virtual GameObject spawn(Transform location){
		return (GameObject) Network.Instantiate(ship,location.position,location.rotation,networkView.GetInstanceID());
	}
}
