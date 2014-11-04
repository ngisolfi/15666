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
		if(networkView.isMine){
			float turn = Input.GetAxis ("Horizontal");
			float moveForward = Input.GetAxis ("Vertical");

			if(moveForward > 0f){
				controller.Thrust();
			}

			controller.Torque(turn);
		}
	}

//	float rectifyAngle(float ang){
//		while(ang >= 180)
//			ang -= 360;
//		while(ang < -180)
//			ang += 360;
//		return ang;
//	}
}
