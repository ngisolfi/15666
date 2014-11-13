using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Scheduler : MonoBehaviour {
	private List<ScheduledBehavior> _activeBehaviors;
	private int _maxPeriod = 0;

	// Use this for initialization
	void Awake () {
		_activeBehaviors = new List<ScheduledBehavior>();
	}

	void Start() {

	}
	
	// Update is called once per frame
	void Update () {
		foreach(ScheduledBehavior behavior in _activeBehaviors){
			if((Time.frameCount - behavior.phase) % behavior.framesBetweenUpdates == 0){
				float startTime = Time.time;
				behavior.ScheduledUpdate();
				behavior.computationTime = Time.time-startTime;
			}
		}
	}

	public int addBehavior(ScheduledBehavior newBehavior){
		if(newBehavior.framesBetweenUpdates > _maxPeriod)
			_maxPeriod = newBehavior.framesBetweenUpdates;
		float[] updateTime = new float[_maxPeriod];
		foreach(ScheduledBehavior behavior in _activeBehaviors){
			for(int i=behavior.phase;i<_maxPeriod;i+=behavior.framesBetweenUpdates){
				updateTime[i] += behavior.computationTime;
			}
		}
		int newPhase = _maxPeriod;
		float minTime = (_activeBehaviors.Count+1)*Time.deltaTime;
		for(int i=0;i<_maxPeriod;i++){
			if(updateTime[i] < minTime){
				minTime = updateTime[i];
				newPhase = i;
			}
		}
		_activeBehaviors.Add(newBehavior);
		return newPhase;
	}

	public void removeBehavior(ScheduledBehavior oldBehavior){
		_activeBehaviors.Remove(oldBehavior);
	}
}
