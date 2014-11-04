using UnityEngine;
using System.Collections;

public class Idle : Action {
	public Idle(WorldState state):base(state)
	{
	}

	public override void Execute ()
	{
	}
	
	public override Action nextAction ()
	{
		return this;
	}

	public override InformationState PredictedOutcome (InformationState state)
	{
		return InformationState.Copy(state);
	}

	public override bool CheckPrecondition (InformationState state)
	{
		return true;
	}
}
