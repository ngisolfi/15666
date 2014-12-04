using UnityEngine;
using System.Collections;

public class ApproachState : State {
	public float roll_threshold_angle = 10f;
	public float pitch_threshold_angle = 50f;
	public float thrust_threshold_angle = 40f;
//	public float avoidance_distance = 10000f;
//	public float avoidance_vertical_check_angle = 30f;
	protected Vector3 _target;
	public float distance_threshold = 1000f;

	public override void execute ()
	{

		Vector3 diff = _target-_controller.transform.position;
		float angleDiff = Vector3.Angle(_controller.transform.forward,diff);
		if(angleDiff > roll_threshold_angle){
			float rollMag = -AngleSigned(_controller.transform.up,diff,_controller.transform.forward);
			if (rollMag > 90f)
				rollMag = 180f-rollMag;
			else if (rollMag < -90f)
				rollMag = -rollMag-180f;
			_controller.roll(rollMag);
			if(Mathf.Abs(rollMag) < pitch_threshold_angle)
				_controller.pitch(AngleSigned(_controller.transform.forward,diff,_controller.transform.right));
		}
		if(Vector3.Angle(_controller.transform.forward,diff) < thrust_threshold_angle && diff.sqrMagnitude > distance_threshold*distance_threshold){
			_controller.thrust();
		}
	}
	
	public override State transitionNext ()
	{
		if(_handler.playerControlled)
			return player_controlled_state;
		else
			return base.transitionNext ();
	}

//	protected void avoidanceThrust()
//	{
//		RaycastHit hit;
////		bool trajectory_clear = true;
//		if(Physics.Raycast(_controller.transform.position,_controller.transform.forward, out hit, avoidance_distance,~LayerMask.GetMask("Player"))){
//			if(Physics.Raycast(_controller.transform.position,Vector3.Slerp(_controller.transform.forward, _controller.transform.up,avoidance_vertical_check_angle/90f),out hit, avoidance_distance,~LayerMask.GetMask("Player"))){
//				if(Physics.Raycast(_controller.transform.position,Vector3.Slerp(_controller.transform.forward, -_controller.transform.up,avoidance_vertical_check_angle/90f),out hit, avoidance_distance,~LayerMask.GetMask("Player"))){
//					_controller.pitch(1f);
//					return;
//				}
//				_controller.pitch(-1f);
//			}else{
//				_controller.pitch(1f);
//			}
//		}
//		_controller.thrust();
//	}
}
