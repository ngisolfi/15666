using UnityEngine;
using System.Collections;

public class IdleState : State {
	public State player_controlled_state;
	public State approach_state;
	public State wander_state;

	public override State transitionNext()
	{
		if(_handler.playerControlled)
			return player_controlled_state;
		else if(_handler.goal)
			return approach_state;
		else
			return base.transitionNext();
	}
}
