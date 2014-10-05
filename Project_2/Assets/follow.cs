using UnityEngine;
using System.Collections;

public class follow : MonoBehaviour {
	
	public GameObject leader;
	public float lin_vel;
	public static Vector3 target_position;
	private float goal_dist;
	private Vector3 direction;
	private Transform self;
	private float target_orientation;
	private Vector3 zero;
	private obstacle_avoidance script;
	
	void Start () {
		
		self = transform;
		target_position = self.position;
		
		script = GetComponent<obstacle_avoidance> ();
		
	}
	
	void Update () {
		
		//In this script, we want to follow the leader which is a public
		//gameObject set in the inspector
		target_position = leader.transform.position;
		
		goal_dist = Vector3.Distance (self.position, target_position);
		
		//We are always walking here...having the agents run caused them
		//to knock each other all over the place...lol

		Vector3 move = new Vector3 (lin_vel * Time.deltaTime,lin_vel * Time.deltaTime,lin_vel * Time.deltaTime);
		Vector3 diff = target_position - self.position;
		Quaternion rot;
		
		if(script.avoid_behavior==Vector3.zero){
			rot = Quaternion.LookRotation (diff);
		} else{
			rot = Quaternion.LookRotation (script.avoid_behavior);
		}
		
		//Interpolate toward goal orientation
		self.rotation = Quaternion.Slerp (self.rotation, rot, Time.deltaTime);
		
		//Move in the direction the agent is facing
		Vector3 interm_goal_pos = self.forward * lin_vel*Time.deltaTime;
		self.position = self.position + interm_goal_pos;
		
	}
	
}
