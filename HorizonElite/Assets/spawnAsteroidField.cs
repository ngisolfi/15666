using UnityEngine;
using System.Collections;

public class spawnAsteroidField : MonoBehaviour {

	public GameObject[] asteroids;
	public int number;
	public int radius;

	public float rotationSpeed;
	public float driftSpeed;

	public int lowScale;
	public int highScale;


	// Spawn a full field
	void Start () {
	
		for(int i=0;i<number; i++){
			GameObject temp = Instantiate(asteroids[(int)Random.Range(0,3)],
			                   Random.insideUnitSphere*radius,
			                   Quaternion.LookRotation (Random.onUnitSphere*radius))  as GameObject;
			float scale = Random.Range (lowScale,highScale);
			temp.transform.localScale = new Vector3(scale,scale,scale);
			temp.rigidbody.mass = scale;
//			temp.transform.localScale.y = scale;
//			temp.transform.localScale.z = scale;
			temp.rigidbody.angularVelocity = Random.insideUnitCircle*rotationSpeed;
//			temp.rigidbody.velocity = Random.Range (0,driftSpeed)*Random.onUnitSphere;
		}
	}
	
	// spawn on side to fill field as it travels
	void Update () {
	
	}
}
