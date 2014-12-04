using UnityEngine;
using System.Collections;

public class AimLaser : MonoBehaviour {
	private Transform _target;
	public float staticLead = 500f;
	private laserFire _gun;
	public float targetAngle = 15f;

	// Use this for initialization
	void Start () {
		_gun = transform.Find ("laserSpawner").gameObject.GetComponent<laserFire>();
	}
	
	// Update is called once per frame
	void Update () {
		if(_target){
			if(Vector3.Angle(transform.forward,_target.position-transform.position) > targetAngle){
				_target = null;
			}
		}
	}

	public Vector3 target
	{
		get
		{
			if(_target){
				// doesn't consider motion of ship while laser travels (can be updated for accuracy)
				if(_target.rigidbody){
					return _target.position + _target.rigidbody.velocity*(_target.position-transform.position).magnitude/_gun.laserSpeed;
				}else{
					return _target.position;
				}

			}else{
				return transform.position + transform.forward*staticLead;
				//return transform.position + transform.rigidbody.velocity.normalized*staticLead;
				
			}
		}
	}

	public void senseEnemy(Transform enemy)
	{
		if(!_target && Vector3.Angle(transform.forward,enemy.position-transform.position) < targetAngle){
			_target = enemy;
		}
	}
}
