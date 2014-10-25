using UnityEngine;
using System.Collections;

public class move_ss : MonoBehaviour {
	
	private AStar_ss selfplan;
//	private obstacleAvoidance personalbubble;
	public GameObject player;
	public float speed;
	public float turnspeed;
	private Quaternion rot;
	
	void Start (){
		
		selfplan = gameObject.GetComponent<AStar_ss>();
//		personalbubble = gameObject.GetComponent<obstacleAvoidance> ();
	}
	
	void Update (){
		//	Debug.Log (selfplan.plan.Count);
		if (selfplan.plan.Count > 1) {
			Vector2 temp = selfplan.plan [selfplan.plan.Count - 2];
			Vector3 goal3 = new Vector3 (temp.x, 0.0f, temp.y);
			
			Vector3 diff = goal3 - transform.position;
			
			//if (personalbubble.avoid_behavior == Vector3.zero) {
			rot = Quaternion.LookRotation (diff);
			//} else {
			//rot = Quaternion.LookRotation (personalbubble.avoid_behavior - transform.position);
			//}
			
			float goal_dis = Vector3.Distance (transform.position, player.transform.position);
			
			if (goal_dis > 1) {
				transform.rotation = Quaternion.Slerp(transform.rotation,rot,Time.deltaTime*turnspeed);
				transform.position = transform.position + transform.forward * speed * Time.deltaTime;
			}
		}
	}
}
//
//	public float speed;
//	private float startTime;
//	private AStar selfPlan;
//	public int ID;
//	private int frameNum;
//	private Vector3 goal3;
//	private Vector2 intermGoal;
//	// Use this for initialization
//	void Start () {
//		selfPlan = GetComponent<AStar> ();
//		frameNum = 0;
//		intermGoal = new Vector2 (transform.position.x, transform.position.z);
//	}
//	
//	// Update is called once per frame
//	void Update () {
//			
//				goal3 = 
//				frameNum++;
//				if (frameNum % 10 == ID) {
//						///	Vector2 coords = new Vector2 (transform.position.x, transform.position.z);
//						//	while (Mathf.Abs(Vector2.Distance(coords-selfPlan.plan))>.1){
//		Debug.Log (selfPlan.plan.Count);
//						if (selfPlan.plan.Count > 2 && selfPlan.plan.Count < 15) {
//			if(intermGoal!=selfPlan.plan[selfPlan.plan.Count-3]){
//
//						//Debug.Log (selfPlan.hMap[(int)selfPlan.plan[selfPlan.plan.Count - 2].x,(int)selfPlan.plan[selfPlan.plan.Count - 2].y]);
//						intermGoal = selfPlan.plan [selfPlan.plan.Count - 3];
//			}
//						//Vector3 goal3 = new Vector3 (intermGoal.x, 1.0f, intermGoal.y);
//						//Debug.Log ("goal3: " + goal3);
//						Vector3 diff = goal3 - transform.position;
//
//						Quaternion rot = Quaternion.LookRotation (diff);
//						float journeyLength = Vector3.Distance (transform.position, goal3);
//						float distCovered = (Time.time - startTime) * speed;
//						float fracJourney = distCovered / journeyLength;
//						//if(fracJourney<.9){
//						transform.position = transform.position + transform.forward * speed * Time.deltaTime;//Vector3.Lerp (transform.position, goal3, speed*Time.deltaTime);
//						transform.rotation = Quaternion.Slerp (transform.rotation, rot, speed * speed * Time.deltaTime);
//						selfPlan.plan.Clear ();
//						}
//				}
//				
////				Vector3 diff = goal3 - transform.position;
////				
////				//Did we find an obstacle?  If so, we must override our original plan
////				if(script.avoid_behavior==Vector3.zero){
////					rot = Quaternion.LookRotation (diff);
////				}else{
////					rot = Quaternion.LookRotation (script.avoid_behavior-self.position);
////				}
////				
////				//Sssslllerrrrrrppp
////				self.rotation = Quaternion.Slerp (self.rotation, rot, Time.deltaTime);
////				
////				//Let's find which animation is appropriate for how far we are from the goal
////				//Additionally, we will increase or decrease the velocity of the agent here
////				goal_dist = Vector3.Distance (self.position, target_position);
////				if (goal_dist < agility.stop_walk) {
////					if (lin_vel > 0.0f) {
////						lin_vel = lin_vel - agility.lin_acc * Time.deltaTime;
////					} else {
////						lin_vel = 0.0f;
////					}
////					anim.SetFloat ("Speed", 0.0f);
////				} else if (goal_dist >= agility.stop_walk && goal_dist < agility.walk_run) {
////					if (lin_vel < agility.mxw_vel) {
////						lin_vel = lin_vel + agility.lin_acc * Time.deltaTime;
////					} else {
////						lin_vel = lin_vel - agility.lin_acc * Time.deltaTime;
////					}
////					anim.SetFloat ("Speed", 0.2f);
////				} else if (goal_dist >= agility.walk_run) {
////					if (lin_vel < agility.mxr_vel) {
////						lin_vel = lin_vel + agility.lin_acc * Time.deltaTime;
////					}
////					anim.SetFloat ("Speed", 1.2f);
////				}
////				
////				
////				//Update by moving forward since we are facing in direction we want
////				Vector3 interm_goal_pos = self.forward * lin_vel * Time.deltaTime;
////				self.position = self.position + interm_goal_pos;
////						}
//				//}
//	}
//}
