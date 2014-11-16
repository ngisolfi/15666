using UnityEngine;
using System.Collections;


public class mineable : MonoBehaviour {

	public string element;
	public float collect_rate = 0.f;
	private float time_left = 0.0f;

	// Use this for initialization
	void Start () {
			
	}

	void OnTriggerEnter(Collider other)
	{
		other.GetComponent<payloadBar> ().enteredAtmosphere (element);
	}

	void OnTriggerStay(Collider other){

		time_left -= Time.deltaTime;
		if (time_left <= 0.0f) {
			time_left = collect_rate;
			other.GetComponent<payloadBar> ().collectResource (element, 1);
		}
	}
}
