using UnityEngine;
using System.Collections;

public class shot : MonoBehaviour {
	public float speed;
	private float startTime;
	public float secondsUntilDestroy;
	
	// Use this for initialization
	void Start () {
		startTime = Time.time;
	}
	
	void FixedUpdate () {
		this.transform.position += speed*this.transform.up;
		if(Time.time - this.startTime > secondsUntilDestroy){
			Destroy(this.gameObject);
		}
	}
	
	void OnCollisionEnter(Collision collision){
		Destroy(this.gameObject);
	}
}
