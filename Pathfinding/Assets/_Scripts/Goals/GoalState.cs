using UnityEngine;
using System.Collections;

public abstract class GoalState {

	public abstract bool GoalAchieved(InformationState info);
}
