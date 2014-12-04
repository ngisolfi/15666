using UnityEngine;
using System.Collections;

public class State : MonoBehaviour {
	protected StateHandler _handler;
	protected thrustController _controller;

	protected virtual void Awake () {
		_handler = transform.parent.GetComponent<StateHandler>();
		_controller = transform.parent.GetComponent<thrustController>();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public virtual void execute(){}
	public virtual State transitionNext(){
		return this;
	}
	
	public static float AngleSigned(Vector3 v1, Vector3 v2, Vector3 n)
	{
		return Mathf.Atan2(
			Vector3.Dot(n, Vector3.Cross(v1, v2)),
			Vector3.Dot(v1, v2)) * Mathf.Rad2Deg;
	}
}
