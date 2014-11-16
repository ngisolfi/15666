using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	public int max_health = 100;
	public int healthLevel {get; set;}

	// Use this for initialization
	void Start () {
		healthLevel = 100;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
