using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class FollowPath : Action {
	protected List<Vector3> _path;
	protected ShipController _controller;
	protected int _index;
	protected bool _finished;
	protected float closenessThreshold = 5;

	public FollowPath(WorldState state):base(state)
	{
		_path = new List<Vector3>();
		_controller = state.controller;
	}

	public List<Vector3> path
	{
		set
		{
			_path = value;
			_index = 0;
			_finished = false;
		}
	}

//	public EnemyController controller
//	{
//		set
//		{
//			_controller = value;
//		}
//	}

	public override void Execute ()
	{
		if(_path.Count > 0){
//			_controller.setPointAttractor();
			Vector3 targetDirection = _path[_index]-_controller.transform.position;
			targetDirection.y = 0f;
			
			float torque = 0.1f*AngleSigned(_controller.transform.forward, targetDirection, Vector3.up) - 0.5f*_controller.rigidbody.angularVelocity.y;
			
			_controller.Torque(torque);
			if(Mathf.Abs(torque) < 30f)
				_controller.Thrust();

			if((_controller.transform.position-_path[_index]).sqrMagnitude < closenessThreshold*closenessThreshold){
				if(++_index >= _path.Count){
					_index = _path.Count-1;
					_finished = true;
				}
			}
			//			anim.SetFloat("Speed",controller.velocity.magnitude);
		}
	}
}
