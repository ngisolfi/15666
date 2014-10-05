using UnityEngine;
using System.Collections;

public class wave_spawner : MonoBehaviour {
	
	public GameObject hazard;
	//public Vector3 spawnValues;
	
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	
	void Start () {
		
		StartCoroutine(SpawnWaves ());
		
	}
	
	IEnumerator SpawnWaves() {
		
		yield return new WaitForSeconds (startWait);
		while (true) {
			
			for(int i=0;i<hazardCount; i++){
				
				Vector2 spawnCircle = Random.insideUnitCircle * 50;
				Vector3 spawnPosition = new Vector3(spawnCircle.x, 0, spawnCircle.y);
				//new Vector3(
				//	Random.Range(-spawnValues.x, spawnValues.x),
				//	spawnValues.y,
				//	spawnValues.z);

				Quaternion spawnRotation = Quaternion.LookRotation(Vector3.zero);
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds(spawnWait);
			}
			yield return new WaitForSeconds(waveWait);
			hazardCount=hazardCount+5;
			//if(spawnWait != 0.0f) { spawnWait = spawnWait-.05f; }
		}
		
	}
}