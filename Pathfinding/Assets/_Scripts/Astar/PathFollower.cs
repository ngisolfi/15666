using UnityEngine;
using System.Collections;

public class PathFollower : MonoBehaviour {
	protected Planner plan;
	protected int index;
	public float max_speed;
	public float acceleration;
	public float rotateSpeed;
	public float Kp;
	public float Kd;
	public float closenessThreshold;
	protected float speed;
//	protected Animator anim;
	protected ShipController controller;

	// Use this for initialization
	void Start () {
		index = 0;
		plan = GetComponent<Planner>();
//		anim = GetComponent<Animator>();
		controller = GetComponent<ShipController>();
	}
	
	// Update is called once per frame
	void Update () {
		checkIndex();
		Step();
	}
	
	void checkIndex(){
		if(!plan.running){
			index = 0;
			plan.running = true;
		}
		if(Time.frameCount % 1 == 0)
			controller.Fire();
	}
	
	void Step() {
		if(plan.path.Count > 0){
			speed = max_speed;
			Vector3 target = plan.path.GetPoint(index);
			Vector3 targetDirection = target-transform.position;
			targetDirection.y = 0f;
			
			float torque = 0.1f*AngleSigned(transform.forward, targetDirection, Vector3.up) - 0.5f*rigidbody.angularVelocity.y;
			
//			float max_angle = 180f;
			controller.Torque(torque);
			if(Mathf.Abs(torque) < 30f)
				controller.Thrust();
			
//			Vector3 moveDirection = Vector3.RotateTowards(transform.forward, targetDirection, rotateSpeed * Mathf.Deg2Rad * Time.deltaTime, 1000);
//			moveDirection.Normalize();
//			
//			float targetSpeed = Mathf.Min(Mathf.Max (Kp*targetDirection.magnitude/Time.deltaTime - Kd*speed,-max_speed),max_speed);
//			
//			speed = Mathf.Lerp(speed,targetSpeed,acceleration*Time.deltaTime);
//			
//			transform.rotation = Quaternion.LookRotation(moveDirection);
//			CollisionFlags collisionFlags = controller.Move(speed*moveDirection*Time.deltaTime+Vector3.down);
//			transform.position = next;
			if((transform.position-target).sqrMagnitude < closenessThreshold*closenessThreshold){
				if(++index >= this.plan.path.Count){
					index = this.plan.path.Count-1;
					speed = 0;
				}
			}
//			anim.SetFloat("Speed",controller.velocity.magnitude);
		}
	}
	
	public static float AngleSigned(Vector3 v1, Vector3 v2, Vector3 n)
	{
		return Mathf.Atan2(
			Vector3.Dot(n, Vector3.Cross(v1, v2)),
			Vector3.Dot(v1, v2)) * Mathf.Rad2Deg;
	}
	
	public Vector3 CurrentTarget(){
		if(plan.path.Count > 0)
			return plan.path.GetPoint(index);
		else
			return Vector3.zero;
	}
}
