using UnityEngine;
using System.Collections;

public class Attack : Action {

	protected ShipController _controller;
	private Transform _target;

	public Attack(WorldState state):base(state)
	{
		_controller = state.controller;
//		_target = state.target;
	}

	public override void Execute ()
	{
		Vector3 targetDirection = _state.target.position-_controller.transform.position;
		targetDirection.y = 0f;

		float distance = targetDirection.magnitude;
		targetDirection += _state.target.rigidbody.velocity*distance/20f;
		
		float torque = 0.1f*AngleSigned(_controller.transform.forward, targetDirection, Vector3.up) - 0.5f*_controller.rigidbody.angularVelocity.y;
		
		_controller.Torque(torque);

		_controller.Fire();
	}
	
	public override Action nextAction ()
	{
		if(!_state.target)
			return new Wander(_state);
		if(_state.distanceToTarget > 10f)
			return new Pursue(_state);
		if(_state.timeSinceLastHit < 0.8f)
			return new Evade(_state);
		return this;
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
