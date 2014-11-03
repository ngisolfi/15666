using UnityEngine;
using System.Collections;

public class Evade : Action {
	protected ShipController _controller;
	protected Transform _target;


	public Evade (ShipController controller, Transform target)
	{
		_controller = controller;
		_target = target;
	}

	public override void Execute ()
	{
		Vector3 orientation;
		if(Vector3.Dot(_controller.transform.position-_target.position,_target.right) > 0f)
			orientation = Vector3.down;
		else
			orientation = Vector3.up;
		Vector3 targetDirection = Vector3.Cross(_target.rigidbody.velocity,orientation);
		targetDirection.y = 0f;
		
		float torque = 0.1f*AngleSigned(_controller.transform.forward, targetDirection, Vector3.up) - 0.5f*_controller.rigidbody.angularVelocity.y;
		
		_controller.Torque(torque);
//		if(Mathf.Abs(torque) < 30f)
		_controller.Thrust();
	}

	public override bool CheckPrecondition (InformationState state)
	{
		return state.probabilityTargeted > 0.7f && state.health < 0.7f;
	}

	public override InformationState PredictedOutcome (InformationState state)
	{
		InformationState outState = InformationState.Copy(state);
		outState.probabilityTargeted = 0.1f;
		return outState;
	}
}
