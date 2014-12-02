using UnityEngine;
using System.Collections;

public class thrustController : MonoBehaviour {

	public float speed;
	public float pitchSpeed;
	public float rollSpeed;
	public float fireRate = 0.5F;
	private float nextFire = 0.0F;

	private Vector3 thrusterLocation;
	private laserFire laserSpawn;
	// Use this for initialization
	void Start () {
	
		thrusterLocation = GameObject.Find("Thruster").transform.position;
		laserSpawn = GameObject.Find ("laserSpawner").GetComponent<laserFire>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (networkView.isMine) {
			if (Input.GetButton("Fire1")) {
				//Thrust On
				rigidbody.AddForce (speed * transform.forward);//(transform.position - thrusterLocation).normalized);
			}
			if (Input.GetButtonDown("Fire2") && Time.time > nextFire) {
				nextFire = Time.time + fireRate;
				laserSpawn.fireLaser ();
			}
			if (Input.GetAxis("Vertical") > 0f) {

				rigidbody.AddRelativeTorque (new Vector3 (pitchSpeed, 0, 0));
				//rotate to fly down
			}
			if (Input.GetAxis("Vertical") < 0f) {
				//rotate to fly up
				rigidbody.AddRelativeTorque (new Vector3 (-pitchSpeed, 0, 0));
			}
			if (Input.GetAxis("Horizontal") < 0f) {
				//roll to the left
				rigidbody.AddRelativeTorque (new Vector3 (0, 0, rollSpeed));
			}
			if (Input.GetAxis("Horizontal") > 0f) {
				//roll to the right
				rigidbody.AddRelativeTorque (new Vector3 (0, 0, -rollSpeed));
			}
		}
	}
}
