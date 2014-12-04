using UnityEngine;
using System.Collections;

public class PursueState : PathFollowerState {
	protected Transform _enemy;

	protected Transform enemy
	{
		get
		{
			if(!_enemy)
				chooseEnemy();
			return _enemy;
		}
	}

	public override void execute ()
	{
		if(enemy){
			setTarget(enemy.position);
			if(transform.parent.GetComponent<AimLaser>().inSight)
			{
				_controller.fire();
			}
		}
		base.execute();
	}

	protected void chooseEnemy()
	{
		Transform potential_enemy = null;
		float min_dist = 10e12f, dist;
		foreach(Transform e in transform.parent.GetComponent<Sensor>().enemies){
			dist = Vector3.Distance(e.transform.position,transform.parent.position);
			if(dist < min_dist){
				min_dist = dist;
				potential_enemy = e;
			}
		}
		_enemy = potential_enemy;
	}

	public override State transitionNext ()
	{
		if(!enemy)
			return idle_state;
		else
			return base.transitionNext ();
	}
}
