using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour {
	
	public GameObject shot;
	public Transform shotSpawn;
	
	public float acceleration;
	public float max_vel;
	public float ang_vel;
	public float tilt;
	public float fireRate;
	private float nextFire;
	//	public Boundary boundary;
	private Vector3 headingTangent;
	
	public GameObject Fire() {
		if (Time.time > nextFire) {
			
			nextFire = Time.time + fireRate;
			GameObject bullet = (GameObject) Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
			Physics.IgnoreCollision(bullet.collider,this.collider);
			return bullet;
		}
		return null;
	}
	
	public void Thrust() {
//		if(networkView.isMine){
		rigidbody.AddForce(transform.forward * acceleration);
		Vector3 velocity = rigidbody.velocity;
		float velocityMagnitude = velocity.magnitude;
		if(velocityMagnitude > max_vel){
			velocity *= (max_vel/velocityMagnitude);
			rigidbody.velocity = velocity;
		}
		headingTangent = transform.right;
//		}
	}
	
	public void Torque(float turn) {
//		if(networkView.isMine)
		rigidbody.AddTorque(0f,turn * ang_vel,0f);
	}

	void FixedUpdate () {
		
		//		rigidbody.AddForce(-Vector3.up*(5f*transform.position.y + 5f*rigidbody.velocity.y));
//		if(networkView.isMine){
		Vector3 slideDrag = -Vector3.Dot(headingTangent,rigidbody.velocity)*headingTangent;
		slideDrag.y = 0f;
		rigidbody.AddForce(slideDrag);
//		}
		
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
