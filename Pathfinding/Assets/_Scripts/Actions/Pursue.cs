using UnityEngine;
using System.Collections;

public class Pursue : FollowPath {
	
	private GridHandler grid;
	private int updateCount = 10;
//	private Transform _target;
	
	public Pursue(WorldState state): base(state)
	{
		grid = GameObject.Find("Grid").GetComponent<GridHandler>();
//		_target = target;
	}
	
	// Use this for initialization
	public override void Execute(){
		if(_state.target && Time.frameCount % updateCount == 0){
			path = grid.computePath(_controller.transform.position,_state.target.position,300);
		}
		
		base.Execute();
	}
	
	public override Action nextAction ()
	{
		if(!_state.target){
//			Debug.Log("Changed to Wander");
			return new Wander(_state);
		}
		if(_state.distanceToTarget < 10f)
			return new Attack(_state);
		if(_state.timeSinceLastHit < 0.8f)
			return new Evade(_state);
		return this;
	}

	public override bool CheckPrecondition (InformationState state)
	{
		return state.targetVisible;
	}

	public override InformationState PredictedOutcome (InformationState state)
	{
		InformationState outState = InformationState.Copy(state);
		outState.distanceToTarget = 0f;
		return outState;
	}
}
