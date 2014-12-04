using UnityEngine;
using System.Collections;

public class IdleState : State {

	public override State transitionNext()
	{
		if(_handler.playerControlled)
			return player_controlled_state;
		else if(true)
			return wander_state;
		else
			return base.transitionNext();
	}
}
