using UnityEngine;
using System.Collections;

public class Aim : Action {

	protected ShipController _controller;
	private Transform _target;

	public Aim(ShipController controller, Transform target):base()
	{
		_controller = controller;
		_target = target;
	}

	public override void Execute ()
	{
		Vector3 targetDirection = _target.position-_controller.transform.position;
		targetDirection.y = 0f;
		
		float torque = 0.1f*AngleSigned(_controller.transform.forward, targetDirection, Vector3.up) - 0.5f*_controller.rigidbody.angularVelocity.y;
		
		_controller.Torque(torque);
	}
}
