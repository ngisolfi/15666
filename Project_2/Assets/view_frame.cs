﻿using UnityEngine;
using System.Collections;

public class view_frame : MonoBehaviour {

	public GameObject ship;
	public Camera camera1;
	public Camera camera2;
	public GUIText crosshair;
	public float look_speed;
	
	private int chosen_camera;
	
	// Use this for initialization
	void Start () {
		
		camera1.enabled = true;
		camera2.enabled = false;
		crosshair.enabled = false;
		//		chosen_camera = 1;
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown ("f") && camera1.enabled) {
			//			chosen_camera = 2;
			camera2.enabled = true;
			camera1.enabled = false;
			crosshair.enabled = true;
		} else if (Input.GetKeyDown ("g") && camera2.enabled) {
			//			chosen_camera=1;
			camera1.enabled = true;
			camera2.enabled = false;
			crosshair.enabled = false;
		} else if (Input.GetKey(KeyCode.LeftControl)) {

			float hoz_rotation = Input.GetAxis ("Mouse X") * 3;
			float ver_rotation = -Input.GetAxis ("Mouse Y") * 3;
			Vector3 look_at = new Vector3(0.0f, hoz_rotation, 0.0f);
			Debug.Log ("rotation: " + hoz_rotation + "look_at: " + look_at);
			camera2.transform.Rotate(ver_rotation, hoz_rotation, 0.0f);
			Vector3 snap = Vector3.Normalize(new Vector3(camera2.transform.right.x, 0.0f, camera2.transform.right.z));
			camera2.transform.right = snap;
		}else if(Input.GetKeyUp(KeyCode.LeftControl)){
			Debug.Log ("DIDIT");
			camera2.transform.forward = ship.transform.forward;
			//camera2.transform.Rotate (
			//	-camera2.transform.rotation.eulerAngles.x,
			//	-camera2.transform.rotation.eulerAngles.y,
			//	-camera2.transform.rotation.eulerAngles.z);
		}
		
	}
}
