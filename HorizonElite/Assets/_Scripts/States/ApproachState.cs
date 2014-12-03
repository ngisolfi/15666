using UnityEngine;
using System.Collections;

public class ApproachState : State {
	public State idle_state;
	public State player_controlled_state;

	public override void execute ()
	{
		if(_handler.goal){
			Vector3 diff = _handler.goal.position-_controller.transform.position;
			_controller.pitch(AngleSigned(_controller.transform.forward,diff,_controller.transform.right));
			_controller.roll(-AngleSigned(_controller.transform.up,diff,_controller.transform.forward));
			if(Vector3.Angle(_controller.transform.forward,diff) < 40f)
				_controller.thrust();
		}
	}
	
	public override State transitionNext ()
	{
		if(_handler.playerControlled)
			return player_controlled_state;
		else if(!_handler.goal)
			return idle_state;
		else
			return base.transitionNext ();
	}
}
