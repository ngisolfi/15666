using UnityEngine;
using System.Collections;

public class shot : MonoBehaviour {
	public float speed;
	private float startTime;
	public float secondsUntilDestroy;
	public int strength;
//	private ShipController source;
	
	// Use this for initialization
	void Start () {
		startTime = Time.time;
	}
	
	void FixedUpdate () {
		this.transform.position += speed*Time.deltaTime*this.transform.up;
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
}
