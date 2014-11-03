using UnityEngine;
using System.Collections;

public class Attack : Action {

	protected ShipController _controller;
	private Transform _target;

	public Attack(ShipController controller, Transform target):base()
	{
		_controller = controller;
		_target = target;
	}

	public override void Execute ()
	{
		Vector3 targetDirection = _target.position-_controller.transform.position;
		targetDirection.y = 0f;

		float distance = targetDirection.magnitude;
		targetDirection += _target.rigidbody.velocity*distance/20f;
		
		float torque = 0.1f*AngleSigned(_controller.transform.forward, targetDirection, Vector3.up) - 0.5f*_controller.rigidbody.angularVelocity.y;
		
		_controller.Torque(torque);

		_controller.Fire();
	}

	public override InformationState PredictedOutcome (InformationState state)
	{
		InformationState outState = InformationState.Copy(state);
		outState.targetHealth = 0;
		return outState;
	}

	public override bool CheckPrecondition (InformationState state)
	{
		return state.targetVisible && state.distanceToTarget < 15f;
	}
}
