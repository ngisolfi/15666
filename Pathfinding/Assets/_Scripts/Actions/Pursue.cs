using UnityEngine;
using System.Collections;

public class Pursue : FollowPath {
	
	private GridHandler grid;
	private int updateCount = 10;
	private Transform _target;
	
	public Pursue(ShipController controller, Transform target): base(controller)
	{
		grid = GameObject.Find("Grid").GetComponent<GridHandler>();
		_target = target;
	}
	
	// Use this for initialization
	public override void Execute(){
		if(_target && Time.frameCount % updateCount == 0){
			path = grid.computePath(_controller.transform.position,_target.position,300);
		}
		
		base.Execute();
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
