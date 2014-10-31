using UnityEngine;
using System.Collections;

[System.Serializable]

public class Boundary {
	
	public float xMin, xMax, zMin, zMax;
	
}

public class playerController : MonoBehaviour {
	
	public GameObject shot;
	public Transform shotSpawn;
	
	public float acceleration;
	public float max_vel;
	public float ang_vel;
	public float tilt;
	public float fireRate;
	private float nextFire;
	public Boundary boundary;
	private Vector3 headingTangent;
	
	void Update () {
		
		if (Input.GetButton ("Fire1") && Time.time > nextFire) {
			
			nextFire = Time.time + fireRate;
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
			
		}
		
	}
	
	void FixedUpdate () {
		
		float turn = Input.GetAxis ("Horizontal");
		float moveForward = Input.GetAxis ("Vertical");
		
//		rigidbody.velocity = transform.forward * lin_vel * moveForward;
		if(moveForward > 0f){
			rigidbody.AddForce(transform.forward * acceleration);
			Vector3 velocity = rigidbody.velocity;
			float velocityMagnitude = velocity.magnitude;
			if(velocityMagnitude > max_vel){
				velocity *= (max_vel/velocityMagnitude);
				rigidbody.velocity = velocity;
			}
			headingTangent = transform.right;
		}
		
		rigidbody.AddForce(-transform.up * (50f*transform.position.y + rigidbody.velocity.y));
		
		Vector3 slideDrag = -Vector3.Dot(headingTangent,rigidbody.velocity)*headingTangent;
		slideDrag.y = 0f;
		rigidbody.AddForce(slideDrag);
		
		//Debug.Log (rigidbody.velocity);
//		rigidbody.position = new Vector3 (
//			
//			Mathf.Clamp (rigidbody.position.x, boundary.xMin, boundary.xMax),
//			0.0f,
//			Mathf.Clamp (rigidbody.position.z, boundary.zMin, boundary.zMax)
//			);
		
		rigidbody.rotation = Quaternion.Euler (0.0f, rigidbody.rotation.eulerAngles.y + turn * ang_vel, turn * -tilt);
		
//		GameObject.Find("Exhaust/OuterCore").particleEmitter.worldVelocity = rigidbody.velocity;
//		GameObject.Find("Exhaust/InnerCore").particleEmitter.worldVelocity = rigidbody.velocity;
	}
	
	
	
}