using UnityEngine;
using System.Collections;

public class WanderState : PathFollowerState {
	public float time_between_updates;
	protected float last_update = -10e-7f;
	protected float x_limit = 60000f;
	protected float y_limit = 60000f;
	protected float z_limit = 100000f;

	public override void execute ()
	{
		if(Time.time - last_update > time_between_updates || _targetList.Count < 1 || (_targetList[_targetList.Count-1] - _controller.transform.position).sqrMagnitude < distance_threshold*distance_threshold){
			updateTarget();
			last_update = Time.time;
		}
		base.execute ();
	}

	public override State transitionNext ()
	{
		if(transform.parent.GetComponent<Sensor>().inSight)
			return pursue_state;
		else
			return base.transitionNext ();
	}

	protected void updateTarget(){
		setTarget(new Vector3(Random.Range(-x_limit,x_limit),Random.Range(-y_limit,y_limit),Random.Range(-z_limit,z_limit)));
	}
}
