using UnityEngine;
using System.Collections;

public class LoadMazeLevel : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter( Collider other){
		Debug.Log (other.tag);
				if (other.tag == "Player") {
						if (Application.loadedLevelName == "SolarSystem") {
								Application.LoadLevel ("IndoorMaze");
						} else {
								Application.LoadLevel ("SolarSystem");
						}

				}
		}
}
