using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class obstacle_avoidance : MonoBehaviour {
	
	public GameObject[] obstacles;
	public float radius;
	public float influence;
	public float magnitude;
	
	public Vector3 avoid_behavior;
	
	private Transform self;
	private float dx, dy, dz;
	private float theta;
	
	void Start () {
		int i = 0;
		
		obstacles = GameObject.FindGameObjectsWithTag ("Obstacle");
		
		self = transform;
	}
	
	
	void Update () {
		
		//Innocent until proven guilty
		avoid_behavior.Set (0.0f, 0.0f, 0.0f);
		float closest = 100000.0f; //Mathf.Infinity caused weird problems for me
		
		foreach(GameObject obstacle in obstacles) {
			//We will compare every obstacle with this gameObject
			GameObject curr_obst = obstacle;
			float dist = Vector3.Distance(self.position, curr_obst.transform.position);
			
			//Except for itself, so let's filter...
			if(dist < closest && dist !=0.0f) {				
				closest = dist;
				
				//Compute angle between transforms of self and closest obstacle
				theta = Mathf.Atan2 ((curr_obst.transform.position.z-self.position.z),(curr_obst.transform.position.x-self.position.x));// * Mathf.Rad2Deg;
				
				if( dist < radius ){
					
					//This should be infinite repulsion here, but high repulsion suffices
					dx = -Mathf.Cos (theta)*10000 * Mathf.Rad2Deg;
					dy = 0.0f;
					dz = -Mathf.Sin (theta)*10000 * Mathf.Rad2Deg;
					
				}else if((radius <= dist) && (dist <= (radius + influence))) {
					
					//We should push the agent to a variable extent away from the obstacle
					//Since we are getting closer
					dx = -magnitude *(influence+radius-dist)*Mathf.Cos(theta)*Mathf.Rad2Deg;
					dz = -magnitude * (influence+radius-dist)*Mathf.Sin(theta)*Mathf.Rad2Deg;
					dy = 0.0f;
				} else if (dist > radius + influence) {
					// Nowhere near this obstacle, we don't need to avoid it.
					dx = 0.0f;
					dy = 0.0f;
					dz = 0.0f;
				}
				
				Vector3 one_obst = new Vector3(dx,dy,dz);
				avoid_behavior = one_obst;
				
			}
		}
	}
}