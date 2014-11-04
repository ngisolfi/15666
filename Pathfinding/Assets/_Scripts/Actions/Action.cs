using UnityEngine;
using System.Collections;

public abstract class Action {
	protected WorldState _state;
	
	public Action(WorldState state)
	{
		_state = state;
	}
	

	public abstract void Execute();

	public abstract InformationState PredictedOutcome(InformationState state);

	public abstract bool CheckPrecondition(InformationState state);
	
	public abstract Action nextAction();

	public static float AngleSigned(Vector3 v1, Vector3 v2, Vector3 n)
	{
		return Mathf.Atan2(
			Vector3.Dot(n, Vector3.Cross(v1, v2)),
			Vector3.Dot(v1, v2)) * Mathf.Rad2Deg;
	}
}
