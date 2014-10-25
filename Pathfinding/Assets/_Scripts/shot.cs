using UnityEngine;
using System.Collections;

public class shot : MonoBehaviour {
	
	public float speed;
	// Use this for initialization
	void Start () {
		rigidbody.velocity = transform.up.normalized * speed;
	}
	
}
