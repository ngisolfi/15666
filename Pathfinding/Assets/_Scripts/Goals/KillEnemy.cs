using UnityEngine;
using System.Collections;

public class KillEnemy : GoalState {

	public override bool GoalAchieved(InformationState info){
		return info.targetHealth == 0;
	}
}
