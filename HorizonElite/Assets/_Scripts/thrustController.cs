using UnityEngine;
using System.Collections;

public class thrustController : MonoBehaviour {

	public float speed;
	public float pitchSpeed;
	public float rollSpeed;
	public float fireRate = 0.5F;
	private float nextFire = 0.0F;
	private Vector3 gravity_force = Vector3.zero;
	public float gravity_torque;

	//private Vector3 thrusterLocation;
	private laserFire laserSpawn;
	private AimLaser laserSight;
	// Use this for initialization
	void Start () {
	
		//thrusterLocation = transform.Find("Thruster").position;
		laserSpawn = transform.Find ("laserSpawner").gameObject.GetComponent<laserFire>();
		laserSight = gameObject.GetComponent<AimLaser>();
	}
	
	public void setGravityForce(Vector3 gf)
	{
		gravity_force = gf;
	}

	public void fire()
	{
		if (Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			laserSpawn.fireLaser (laserSight.target);
		}
	}

	public void thrust()
	{
		//Thrust On
		rigidbody.AddForce (speed * transform.forward);//(transform.position - thrusterLocation).normalized);
	}

	public void pitch(float val)
	{
		if (val > 0f) {
			
			rigidbody.AddRelativeTorque (new Vector3 (pitchSpeed, 0, 0));
			//rotate to fly down
		}
		if (val < 0f) {
			//rotate to fly up
			rigidbody.AddRelativeTorque (new Vector3 (-pitchSpeed, 0, 0));
		}
	}

	public void roll(float val)
	{
		if (val < 0f) {
			//roll to the left
			rigidbody.AddRelativeTorque (new Vector3 (0, 0, rollSpeed));
		}
		if (val > 0f) {
			//roll to the right
			rigidbody.AddRelativeTorque (new Vector3 (0, 0, -rollSpeed));
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (networkView.isMine) {
		
			// Add the gravity force and rotation
			rigidbody.AddForce(gravity_force);
			
			// Add a torque if gravity is strong
			//Debug.Log ("gforce: " + gravity_force.magnitude.ToString());
			if (gravity_force.magnitude > 250.0f)
			{
				float t = gravity_torque * Mathf.Sqrt(Mathf.Sqrt(gravity_force.magnitude));
				Vector3 rotate_axis = Vector3.Cross (transform.forward, gravity_force.normalized);
				rigidbody.AddTorque(rotate_axis * t);
			}
		}
	}
}
