using UnityEngine;
using System.Collections;

public class WorldState : MonoBehaviour {
	private Transform target;
	private float _targetDistance;
	private bool _distanceUpdated = false;
	private bool _targetHidden;
	private bool _hiddenUpdated = false;

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
		this.resetUpdateFlags();
		if(!targetAvailable){
			GameObject player = GameObject.FindGameObjectWithTag("Player");
			if(player)
				target = player.transform;
		}

//		Debug.Log(this.targetHidden);
	}

	void resetUpdateFlags()
	{
		this._distanceUpdated = false;
		this._hiddenUpdated = false;
	}

	public bool targetAvailable
	{
		get
		{
			return (target != null);
		}
	}

	public float distanceToTarget
	{
		get
		{
			if(targetAvailable){
				if(!this._distanceUpdated){
					this._targetDistance = (transform.position-this.target.position).magnitude;
				}
				return this._targetDistance;
			}else
				return 10e7f;
		}
	}

	public bool targetHidden
	{
		get
		{
			if(targetAvailable){
				if(!_hiddenUpdated)
					this._targetHidden = Physics.Raycast(this.transform.position, this.target.position, this.distanceToTarget, LayerMask.GetMask("Vehicles","Obstacles"));
				return this._targetHidden;
			}else{
				return true;
			}
		}
	}
}
