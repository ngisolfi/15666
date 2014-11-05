using UnityEngine;
using System.Collections;

public class Attack : Action {

	protected ShipController _controller;
//	private Transform _target;
	private static float _leadFraction = 0f;

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
		targetDirection += _state.target.rigidbody.velocity*distance*_leadFraction;
//		GameObject sphere = GameObject.Find("sphere");
//		if(sphere)
//			sphere.transform.position = targetDirection + _controller.transform.position;
		
		float torque = 0.1f*AngleSigned(_controller.transform.forward, targetDirection, Vector3.up) - 0.5f*_controller.rigidbody.angularVelocity.y;
		
		_controller.Torque(torque);

		GameObject bullet = _controller.Fire();
		if(bullet){
			bullet.GetComponent<shot>().setupSignal(_controller,_state.target);
		}
	}
	
	public override Action nextAction ()
	{
		if(!_state.target)
			return new Wander(_state);
		if(_state.targetHidden || _state.distanceToTarget > 20f)
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

	public static void updateLead(float closestDistance)
	{
		if(closestDistance > 0f)
			_leadFraction += 0.001f;
		else if(closestDistance < 0f)
			_leadFraction -= 0.001f;
//		Debug.Log(new Vector2(closestDistance,_leadFraction));
	}

	public static void resetLead()
	{
		_leadFraction = 0f;
	}
}
