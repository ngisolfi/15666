using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class spawnAsteroidField : MonoBehaviour {

	public GameObject[] asteroids;
	public int number;
	//public int radius;

	public float rotationSpeed;
	public float driftSpeed;

	public int lowScale;
	public int highScale;

	public Vector3 center;
	public Vector3 spread;

	private List<Transform> positions;

	// Spawn a full field
	void Start () {

		positions = new List<Transform>();
		int bad_counter = 0;

		for(int i=0;i<number; i++){

			// If we have unsuccessfully attempted to spawn too many asteroids,
			// just stop trying and move on with the game
			if (bad_counter > number / 2)
				break;

			GameObject temp = Network.Instantiate(asteroids[(int)Random.Range(0,3)],
			                   /*Random.insideUnitSphere*radius*/BoxMullerRand(center, spread),
			                   Quaternion.LookRotation (Random.onUnitSphere), 0)  as GameObject;

			// Check for collisions with other asteroids
			bool spawn = true;
			Vector3 extents1 = temp.renderer.bounds.extents;
			float radius1 = Mathf.Max(extents1.x, Mathf.Max (extents1.y, extents1.z));
			for (int j=0;j<positions.Count;++j)
			{
				Vector3 extents2 = positions[j].renderer.bounds.extents;
				float radius2 = Mathf.Max(extents2.x, Mathf.Max (extents2.y, extents2.z));
				Vector3 delta = temp.transform.position - positions[j].position;

				if (delta.magnitude < radius1 + radius2)
					spawn = false;
			}

			// If we collide with another asteroid, decrement and try from a new position
			if (!spawn)
			{
				bad_counter++;
				i--;
				continue;
			}

			float scale = Random.Range (lowScale,highScale);
			temp.transform.localScale = new Vector3(scale,scale,scale);
			temp.rigidbody.mass = scale;
//			temp.transform.localScale.y = scale;
//			temp.transform.localScale.z = scale;
			temp.rigidbody.angularVelocity = Random.insideUnitCircle*rotationSpeed;
//			temp.rigidbody.velocity = Random.Range (0,driftSpeed)*Random.onUnitSphere;

			positions.Add(temp.transform);
		}
	}
	
	// spawn on side to fill field as it travels
	Vector3 BoxMullerRand(Vector3 mean, Vector3 var){
		float u1 = Random.value;
		float u2 = Random.value;
		float a1 = Mathf.Sqrt(-2.0f * Mathf.Log(u1)) * Mathf.Sin(2.0f * Mathf.PI * u2);

		float u3 = Random.value;
		float u4 = Random.value;
		float a2 = Mathf.Sqrt(-2.0f * Mathf.Log(u3)) * Mathf.Sin(2.0f * Mathf.PI * u4);

		float u5 = Random.value;
		float u6 = Random.value;
		float a3 = Mathf.Sqrt(-2.0f * Mathf.Log(u5)) * Mathf.Sin(2.0f * Mathf.PI * u6);

		return new Vector3(mean.x + var.x * a1, mean.y + var.y * a2, mean.z + var.z * a3);

	}
}
