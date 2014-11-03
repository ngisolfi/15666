using UnityEngine;
using System.Collections;

public abstract class Action {

	public abstract void Execute();

	public abstract InformationState PredictedOutcome(InformationState state);

	public abstract bool CheckPrecondition(InformationState state);

	public static float AngleSigned(Vector3 v1, Vector3 v2, Vector3 n)
	{
		return Mathf.Atan2(
			Vector3.Dot(n, Vector3.Cross(v1, v2)),
			Vector3.Dot(v1, v2)) * Mathf.Rad2Deg;
	}
}
