using UnityEngine;
using System.Collections;

public class shot : MonoBehaviour {
	public float speed;
	private float startTime;
	public float secondsUntilDestroy;
	public int strength;
	private ShipController _source;
	private Transform _target;
	private float _closestDistance = 10e7f;
	private bool _measurementTaken = false;
	
	// Use this for initialization
	void Start () {
		startTime = Time.time;
	}

	public void setupSignal(ShipController source, Transform target)
	{
		_source = source;
		_target = target;
	}
	
	void FixedUpdate () {
		this.transform.position += speed*Time.deltaTime*this.transform.up;
		if(_target && !_measurementTaken){
//			float closest = Vector3.Dot(transform.right,_target.position-transform.position);
//			if(Mathf.Abs(closest) < Mathf.Abs(_closestDistance))
//				_closestDistance = closest;
			if(Vector3.Dot(this.transform.up,_target.position-transform.position) < 0f){
				_measurementTaken = true;
				Attack.updateLead(Vector3.Dot(_target.rigidbody.velocity,_target.position-transform.position));
			}
		}
		if(Time.time - this.startTime > secondsUntilDestroy){
			Destroy(this.gameObject);
		}
	}
	
	void OnCollisionEnter(Collision collision){
		Health health = collision.gameObject.GetComponent<Health>();
		if(health)
			health.takeDamage(strength);
		Destroy(this.gameObject);
	}

//	void updateLead(){
//		if(_source && _target){
//
//		}
//	}
}
