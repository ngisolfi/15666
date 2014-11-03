using UnityEngine;
using System.Collections;

public class Wander : FollowPath {

	private GridHandler grid;
	private Vector3 nextPoint;
	private float timeOut = 10f;
	private int updateCount = 4;
	private float timerStart;

	public Wander(ShipController controller): base(controller)
	{
		timerStart = Time.time;
		grid = GameObject.Find("Grid").GetComponent<GridHandler>();
		setNewTarget();
	}

	// Use this for initialization
	public override void Execute(){
		if(Time.frameCount % updateCount == 0){
			if(_finished || Time.time-timerStart > timeOut){
				timerStart = Time.time;
				setNewTarget();
			}else{
				computePath();
			}
		}
		base.Execute();
	}

	public override bool CheckPrecondition (InformationState state)
	{
		return true;
	}

	public override InformationState PredictedOutcome (InformationState state)
	{
		return InformationState.Copy(state);
	}

	void setNewTarget()
	{
//		Vector3 nextPoint = grid.gridToWorld((int)(Random.value*grid.GetLength(0)),(int)(Random.value*grid.GetLength(1)));
		do{
			nextPoint = grid.gridToWorld((int)(Random.value*grid.GetLength(0)),(int)(Random.value*grid.GetLength(1)));
		}while(grid.occupied(nextPoint));
		computePath();
	}

	void computePath()
	{
		path = grid.computePath(_controller.transform.position,nextPoint,500);
	}
}
