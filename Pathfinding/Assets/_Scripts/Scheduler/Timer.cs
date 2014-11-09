using UnityEngine;
using System.Collections;

public class Timer : ScheduledBehavior {
	public int deathTime;

	public override void ScheduledUpdate ()
	{
		Debug.Log(Time.frameCount);
		if(Time.frameCount > deathTime)
			Destroy(this.gameObject);
	}
}
