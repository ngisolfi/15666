using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StateHandler : MonoBehaviour {

	private List<Action> actionList;
	private Action currentAction;

	// Use this for initialization
	void Start () {
		actionList = new List<Action>();
		actionList.Add(new Idle());
//		actionList.Add(new Wander(gameObject.GetComponent<ShipController>()));
//		GameObject player = GameObject.Find("Player");
//		if(player){
//			actionList.Add(new Pursue(gameObject.GetComponent<ShipController>(),player.transform));
//			actionList.Add(new Aim(gameObject.GetComponent<ShipController>(),player.transform));
//		}
		currentAction = actionList[0];
	}
	
	// Update is called once per frame
	void Update () {
		currentAction.Execute();
	}
}
