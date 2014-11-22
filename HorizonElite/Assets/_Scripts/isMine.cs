using UnityEngine;
using System.Collections;

public class isMine : MonoBehaviour {
	//This script makes sure one client get one unique camera
	void Update () {
		if(networkView.isMine){
			camera.enabled = true;
		} else {
			camera.enabled = false;
		}
	}
}
