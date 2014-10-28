using UnityEngine;
using System.Collections;

[System.Serializable]

public class Boundary {
	
	public float xMin, xMax, zMin, zMax;
	
}

public class playerController : MonoBehaviour {
	
	public GameObject shot;
	public Transform shotSpawn;
	
	public float lin_vel;
	public float ang_vel;
	public float tilt;
	public float fireRate;
	private float nextFire;
	public Boundary boundary;
	
	void Update () {
		
		if (Input.GetButton ("Fire1") && Time.time > nextFire) {
			
			nextFire = Time.time + fireRate;
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
			
		}
		
	}
	
	void FixedUpdate () {
		
		float turn = Input.GetAxis ("Horizontal");
		float moveForward = Input.GetAxis ("Vertical");
		
		rigidbody.velocity = transform.forward * lin_vel * moveForward;
		//Debug.Log (rigidbody.velocity);
		rigidbody.position = new Vector3 (
			
			Mathf.Clamp (rigidbody.position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp (rigidbody.position.z, boundary.zMin, boundary.zMax)
			);
		
		rigidbody.rotation = Quaternion.Euler (0.0f, rigidbody.rotation.eulerAngles.y + turn * ang_vel, turn * -tilt);
	}
	
	
	
}