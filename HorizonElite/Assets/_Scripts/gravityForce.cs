using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class gravityForce : MonoBehaviour {

	private List<GameObject> bodies;
	public Vector3 gravity_force;
	public float gravitational_constant = 1.0f;
	
	void Awake () {
		
		bodies = new List<GameObject>();
		
		// Get a list of all planets and suns in the scene for later
		GameObject[] planets = GameObject.FindGameObjectsWithTag("Planet");
		bodies.AddRange(planets);
		
		GameObject[] suns = GameObject.FindGameObjectsWithTag("Sun");
		bodies.AddRange(suns);	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		gravity_force = Vector3.zero;
		Vector3 my_pos = transform.position;
		
		// Loop over all planetary bodies, adding to the gravity force vector
		foreach (GameObject b in bodies)
		{

			if(b == null)
				continue;

			// m1 (i.e. mass of ship) is absorbed into gravitational_constant
			Vector3 direction = b.transform.position - my_pos;
			float r = direction.magnitude;
			float m2 = b.GetComponent<planetMass>().mass;
			gravity_force += gravitational_constant * m2 / (r * r) * direction.normalized;
		}
		
		// Set the gravity force in the ship's thrust controller
		gameObject.GetComponent<thrustController>().setGravityForce(gravity_force);
	}
}
