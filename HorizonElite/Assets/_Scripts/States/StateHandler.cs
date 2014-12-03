using UnityEngine;
using System.Collections;

public class StateHandler : MonoBehaviour {
	private State _currentState;
	public bool playerControlled;
	public Transform goal;

	// Use this for initialization
	void Start () {
		_currentState = transform.Find("States").GetComponent<IdleState>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void FixedUpdate () {
		if (networkView.isMine) {
			_currentState.execute();
			// Check for transition to next state, continue transitioning until stablizes (beware of infinite loops)
			State previous;
			do{
				previous = _currentState;
				_currentState = previous.transitionNext();
			}while(previous != _currentState);
			
		}
	}
}
