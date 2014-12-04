using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SenseManager : MonoBehaviour {
//	private List<GameObject> _humans;
//	private List<GameObject> _aliens;
	public float sightDistanceLimit;
	public float sightAngleLimit;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
//		List<GameObject> humans = new List<GameObject>();
//		List<GameObject> aliens = new List<GameObject>();
//		foreach(GameObject ship in GameObject.FindGameObjectsWithTag("Player")){
//			if(Network.isClient ^ ship.networkView.isMine){
//				humans.Add(ship);
//			}else{
//				aliens.Add(ship);
//			}
//		}
		GameObject[] humans = GameObject.FindGameObjectsWithTag("Player");
		GameObject[] aliens = GameObject.FindGameObjectsWithTag("Enemies");

		Sensor sensor;
		if(Network.isServer){
			foreach(GameObject humanShip in humans){
				sensor = humanShip.GetComponent<Sensor>();
				sensor.clearSenses();
				foreach(GameObject alienShip in aliens){
					Vector3 diff = alienShip.transform.position-humanShip.transform.position;
					if(Vector3.Angle(humanShip.transform.forward,diff) < sightAngleLimit && diff.sqrMagnitude < sightDistanceLimit*sightDistanceLimit){
						sensor.senseEnemy(alienShip.transform);
					}
				}
			}
		}else{
			foreach(GameObject alienShip in aliens){
				sensor = alienShip.GetComponent<Sensor>();
				sensor.clearSenses();
				foreach(GameObject humanShip in humans){
					Vector3 diff = humanShip.transform.position-alienShip.transform.position;
					if(Vector3.Angle(alienShip.transform.forward,diff) < sightAngleLimit && diff.sqrMagnitude < sightDistanceLimit*sightDistanceLimit){
						sensor.senseEnemy(humanShip.transform);
					}
				}
			}
		}
	}

//	public void addHuman(){
//
//	}
//
//	public void addAlien(){
//
//	}
}
