using UnityEngine;
using System.Collections;

public abstract class ScheduledBehavior : MonoBehaviour {
	private Scheduler _scheduler;
	private int _phase;
	public int framesBetweenUpdates = 1;

	public virtual void Awake() {
		GameObject scheduleObject = GameObject.Find("Scheduler");
		if(scheduleObject)
			_scheduler = scheduleObject.GetComponent<Scheduler>();
	}

	// Use this for initialization
	public virtual void Start () {
		if(_scheduler)
			_phase = _scheduler.addBehavior(this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public int phase
	{
		get
		{
			return _phase;
		}
	}

	public abstract void ScheduledUpdate();

	public virtual void OnDestroy()
	{
		if(_scheduler)
			_scheduler.removeBehavior(this);
	}
}
