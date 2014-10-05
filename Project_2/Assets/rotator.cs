using UnityEngine;
using System.Collections;

public class rotator : MonoBehaviour {
	
	public float tumble;
	
	void Start () {
		
		rigidbody.angularVelocity = Random.insideUnitSphere * tumble;
		
	}
}
