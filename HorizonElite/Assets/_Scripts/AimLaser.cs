using UnityEngine;
using System.Collections;

public class AimLaser : MonoBehaviour {
	private Transform _target;
	public float staticLead = 500f;
	public float stupidification=0f;
	private laserFire _gun;
	public float targetAngle = 15f;

	// Use this for initialization
	void Start () {
		_gun = transform.Find ("laserSpawner").gameObject.GetComponent<laserFire>();
	}
	
	// Update is called once per frame
	void Update () {
		if(_target){
			if(!GetComponent<Sensor>().enemies.Contains(_target) || Vector3.Angle(transform.forward,_target.position-transform.position) > targetAngle){
				_target = null;
			}
		}
		if(!_target){
			lockonEnemy();
		}
	}

	public bool inSight
	{
		get{
			return _target != null;
		}
	}

	public Transform enemy
	{
		get{
			return _target;
		}
	}

	public Vector3 target
	{
		get
		{
			if(_target){
				// doesn't consider motion of ship while laser travels (can be updated for accuracy)
				if(_target.rigidbody){
					if(gameObject.GetComponent<StateHandler>().playerControlled)
						return _target.position + _target.rigidbody.velocity*(_target.position-transform.position).magnitude/_gun.shotspeed;
					else
						return _target.position + new Vector3(Random.Range (-stupidification,stupidification),Random.Range(-stupidification,stupidification),Random.Range(-stupidification,stupidification)) + _target.rigidbody.velocity*(_target.position-transform.position).magnitude/_gun.shotspeed;
				}else{
					return _target.position;
				}

			}else{
				return transform.position + transform.forward*staticLead;
				//return transform.position + transform.rigidbody.velocity.normalized*staticLead;
				
			}
		}
	}

	public void lockonEnemy()
	{
		_target = null;
		foreach(Transform e in GetComponent<Sensor>().enemies){
			if(Vector3.Angle(transform.forward,e.position-transform.position) < targetAngle){
				_target = e;
				break;
			}
		}
	}
}
