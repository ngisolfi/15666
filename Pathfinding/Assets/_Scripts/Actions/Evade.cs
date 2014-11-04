using UnityEngine;
using System.Collections;

public class Evade : Action {
	protected ShipController _controller;
//	protected Transform _target;


	public Evade (WorldState state):base(state)
	{
		_controller = state.controller;
//		_target = target;
	}

	public override void Execute ()
	{
		Vector3 targetDirection = Vector3.Cross(_state.target.rigidbody.velocity,Vector3.up);
		targetDirection.y = 0f;
		if(Vector3.Dot(_controller.transform.position-_state.target.position,targetDirection) < 0f)
			targetDirection = Vector3.Cross(_state.target.rigidbody.velocity,Vector3.down);
		
		
		float torque = 0.1f*AngleSigned(_controller.transform.forward, targetDirection, Vector3.up) - 0.5f*_controller.rigidbody.angularVelocity.y;
		
		_controller.Torque(torque);
//		if(Mathf.Abs(torque) < 30f)
		_controller.Thrust();
	}
	
	public override Action nextAction ()
	{
		if(!_state.target)
			return new Wander(_state);
		if(_state.timeSinceLastHit > 1f)
			return new Pursue(_state);
		return this;
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
