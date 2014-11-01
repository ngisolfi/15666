using UnityEngine;
using System.Collections;

[System.Serializable]

//public class Boundary {
//	
//	public float xMin, xMax, zMin, zMax;
//	
//}

public class playerController : MonoBehaviour {
	
//	public GameObject shot;
//	public Transform shotSpawn;
//	
//	public float acceleration;
//	public float max_vel;
//	public float ang_vel;
//	public float tilt;
//	public float fireRate;
//	private float nextFire;
//	public Boundary boundary;
//	private Vector3 headingTangent;
	private ShipController controller;
	
	void Start () {
		controller = GetComponent<ShipController>();
	}
	
	void Update () {
		
		if (Input.GetButton ("Fire1")) {
			controller.Fire();
		}
		
	}
	
	void FixedUpdate () {
		
		float turn = Input.GetAxis ("Horizontal");
		float moveForward = Input.GetAxis ("Vertical");
		
//		rigidbody.velocity = transform.forward * lin_vel * moveForward;
		if(moveForward > 0f){
			controller.Thrust();
		}
		
//		rigidbody.AddForce(-Vector3.up*(5f*transform.position.y + 5f*rigidbody.velocity.y));
		
//		Vector3 slideDrag = -Vector3.Dot(headingTangent,rigidbody.velocity)*headingTangent;
//		slideDrag.y = 0f;
//		rigidbody.AddForce(slideDrag);

//		rigidbody.AddTorque(0f,turn * ang_vel,0f);
		controller.Torque(turn);
//		rigidbody.AddTorque(10f * transform.rotation.x * transform.forward + 10f * transform.rotation.z * transform.right);
//		Debug.Log(new Vector3(rectifyAngle(transform.eulerAngles.x),0f,rectifyAngle(transform.eulerAngles.z)));
		
		//Debug.Log (rigidbody.velocity);
//		rigidbody.position = new Vector3 (
//			
//			Mathf.Clamp (rigidbody.position.x, boundary.xMin, boundary.xMax),
//			0.0f,
//			Mathf.Clamp (rigidbody.position.z, boundary.zMin, boundary.zMax)
//			);
		
//		rigidbody.rotation = Quaternion.Euler (0.0f, rigidbody.rotation.eulerAngles.y + turn * ang_vel, turn * -tilt);
		
//		GameObject.Find("Exhaust/OuterCore").particleEmitter.worldVelocity = rigidbody.velocity;
//		GameObject.Find("Exhaust/InnerCore").particleEmitter.worldVelocity = rigidbody.velocity;
	}

//	float rectifyAngle(float ang){
//		while(ang >= 180)
//			ang -= 360;
//		while(ang < -180)
//			ang += 360;
//		return ang;
//	}
}
