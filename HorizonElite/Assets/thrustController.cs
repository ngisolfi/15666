using UnityEngine;
using System.Collections;

public class thrustController : MonoBehaviour {

	public float speed;
	public float pitchSpeed;
	public float rollSpeed;

	private Vector3 thrusterLocation;

	// Use this for initialization
	void Start () {
	
		thrusterLocation = GameObject.Find("Thruster").transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButton (0)) {
		//Thrust On
			rigidbody.AddForce(speed*transform.forward);//(transform.position - thrusterLocation).normalized);
			Debug.Log ((transform.position-thrusterLocation).normalized);
		}
		if(Input.GetKey(KeyCode.W)){

			rigidbody.AddRelativeTorque(new Vector3(pitchSpeed,0,0));
			//rotate to fly down
		}
		if(Input.GetKey (KeyCode.S)){
			//rotate to fly up
			rigidbody.AddRelativeTorque(new Vector3(-pitchSpeed,0,0));

		}
		if(Input.GetKey (KeyCode.A)){
			//roll to the left
			rigidbody.AddRelativeTorque (new Vector3(0,0,rollSpeed));
		}
		if(Input.GetKey (KeyCode.D)){
			//roll to the right
			rigidbody.AddRelativeTorque(new Vector3(0,0,-rollSpeed));
		}


	}
}
