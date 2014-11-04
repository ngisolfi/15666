using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StateHandler : MonoBehaviour {

//	private List<Action> actionList;
	private Action currentAction;
//	private ShipController controller;

	// Use this for initialization
	void Start () {
//		controller = gameObject.GetComponent<ShipController>();
//		actionList = new List<Action>();
//		actionList.Add(new Idle());
//		actionList.Add(new Wander(gameObject.GetComponent<ShipController>()));
		WorldState state = gameObject.GetComponent<WorldState>();
//		actionList.Add(new Wander(state));
		currentAction = new Wander(state);
	}
	
	// Update is called once per frame
	void Update () {
		currentAction = currentAction.nextAction();
		currentAction.Execute();
	}
}
