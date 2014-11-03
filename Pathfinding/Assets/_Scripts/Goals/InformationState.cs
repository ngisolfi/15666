using UnityEngine;
using System.Collections;

public class InformationState {
	public bool targetVisible;
	public float probabilityTargeted;
	public float distanceToTarget;
	public float targetHealth;
	public float health;

	public static InformationState Copy(InformationState inState){
		InformationState outState = new InformationState();
		outState.targetVisible = inState.targetVisible;
		outState.probabilityTargeted = inState.probabilityTargeted;
		outState.distanceToTarget = inState.distanceToTarget;
		outState.targetHealth = inState.targetHealth;
		outState.health = inState.health;
		return outState;
	}
}