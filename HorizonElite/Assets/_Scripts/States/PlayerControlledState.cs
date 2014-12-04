using UnityEngine;
using System.Collections;

public class PlayerControlledState : State {
	public State idle_state;

	public override void execute ()
	{
		if (Input.GetButton("Fire1")) {
			//Thrust On
			_controller.thrust();
		}
		if (Input.GetButtonDown("Fire2")) {
			_controller.fire();
		}
		_controller.pitch(Input.GetAxis("Vertical"));
		_controller.roll(Input.GetAxis("Horizontal"));
	}
	
	public override State transitionNext ()
	{
		if(_handler.playerControlled = false){
			return idle_state;
		}else{
			return base.transitionNext ();
		}
	}
}
