using UnityEngine;
using System.Collections;

public class WorldState : MonoBehaviour {
	private ShipController _controller;
	private Transform _target;
	private float _targetDistance;
	private bool _distanceUpdated = false;
	private bool _targetHidden;
	private bool _hiddenUpdated = false;
	private Health health;

	// Use this for initialization
	void Start () {
		health = gameObject.GetComponent<Health>();
//		_controller = gameObject.GetComponent<ShipController>();
	}

	// Update is called once per frame
	void Update () {
		this.resetUpdateFlags();

//		Debug.Log(this.targetHidden);
	}

	void resetUpdateFlags()
	{
		this._distanceUpdated = false;
		this._hiddenUpdated = false;
	}
	
	public ShipController controller
	{
		get
		{
			if(_controller == null)
				_controller = gameObject.GetComponent<ShipController>();
			return _controller;
		}
	}
	
	public Transform target
	{
		get
		{
			if(_target == null){
				GameObject player = GameObject.FindGameObjectWithTag("Player");
				if(player)
					_target = player.transform;
			}
			return _target;
		}
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
					this._targetDistance = (transform.position-this._target.position).magnitude;
				}
				return this._targetDistance;
			}else
				return 10e7f;
		}
	}
	
	public float timeSinceLastHit
	{
		get
		{
			return health.timeSinceLastHit;
		}
	}

	public bool targetHidden
	{
		get
		{
			if(targetAvailable){
				if(!_hiddenUpdated)
					this._targetHidden = Physics.Raycast(this.transform.position, this._target.position, this.distanceToTarget, LayerMask.GetMask("Vehicles","Obstacles"));
				return this._targetHidden;
			}else{
				return true;
			}
		}
	}
}
