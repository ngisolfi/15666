using UnityEngine;
using System.Collections;

public class respawnOtherIfTouching : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision){

		//See unity cods for onCollisionEnter if we want this script to instantiate explosion
		collision.gameObject.GetComponent<Health> ().healthLevel = 0;


	}
}
