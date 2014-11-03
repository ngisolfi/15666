using UnityEngine;
using System.Collections;

public class Idle : Action {
	public override void Execute ()
	{
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
