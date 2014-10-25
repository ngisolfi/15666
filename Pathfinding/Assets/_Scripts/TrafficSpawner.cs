using UnityEngine;
using System.Collections;

public class TrafficSpawner : MonoBehaviour {
	
	public GameObject hazard;
	//public Vector3 spawnValues;
	
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	private Vector3[] spawnLocations;
	private Quaternion[] spawnRotations;

	void Start () {

		spawnLocations = new Vector3[] {new Vector3(-60.0f,1.0f,-5.0f),
										new Vector3(-60.0f,1.0f,5.0f),
										//new Vector3(60.0f,1.0f,-5.0f),
										//new Vector3(60.0f,1.0f,5.0f)
		};

		spawnRotations = new Quaternion[] {Quaternion.LookRotation(new Vector3(120.0f,0.0f,0.0f)),
											Quaternion.LookRotation(new Vector3(120.0f,0.0f,0.0f)),
											//Quaternion.LookRotation (new Vector3(-120.0f,0.0f,0.0f)),
											//Quaternion.LookRotation(new Vector3(-120.0f,0.0f,0.0f))
		};

		StartCoroutine(SpawnWaves ());
		
	}
	
	IEnumerator SpawnWaves() {
		
		yield return new WaitForSeconds (startWait);
		while (true) {
			
			for(int i=0;i<hazardCount; i++){
				int choice = Random.Range(0,2);
				Vector3 spawnPosition = spawnLocations[choice];
				//new Vector3(
				//	Random.Range(-spawnValues.x, spawnValues.x),
				//	spawnValues.y,
				//	spawnValues.z);
				
				Quaternion spawnRotation = spawnRotations[choice];
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds(Random.Range(1,3));
			}
			yield return new WaitForSeconds(2);
			//if(spawnWait != 0.0f) { spawnWait = spawnWait-.05f; }
		}
		
	}
}