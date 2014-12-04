﻿using UnityEngine;
using System.Collections;


public class mineable : MonoBehaviour {

	public string element;
	public float collect_rate = 0.0f;
	private float time_left = 0.0f;
	public bool human_home;
	public bool alien_home;	
	public GameObject planet;

	// Use this for initialization
	void Start () {
			
	}

	void OnTriggerEnter(Collider other)
	{		
		// Don't do shit if it ain't not our home
		if (!(Network.isServer && alien_home) && !(Network.isClient && human_home))
			return;

		if (other.gameObject.tag.CompareTo ("Player") != 0)
			return;
			
		if (!other.gameObject.networkView.isMine)
			return;

		// Play mining tutorial message if it is the player's first time mining
		GameObject.Find("GameInstance").GetComponent<networkManager>().playMiningBeginAudio();

//		other.GetComponent<payloadBar> ().enteredAtmosphere (element);

		// Set the tractor beam on, clear all particles that were previously in the emitter
		other.transform.FindChild ("tractorBeam").gameObject.SetActive (true);
		other.transform.FindChild ("tractorBeam").FindChild ("rings").gameObject.GetComponent<ParticleEmitter> ().ClearParticles ();
		other.transform.FindChild ("tractorBeam").FindChild ("smoke").gameObject.GetComponent<ParticleEmitter> ().ClearParticles ();
	}

	void OnTriggerExit(Collider other)
	{
		// Don't do shit if it ain't not our home
		if (!(Network.isServer && alien_home) && !(Network.isClient && human_home))
			return;

		if (other.gameObject.tag.CompareTo ("Player") != 0)
			return;

		if (!other.gameObject.networkView.isMine)
			return;
		// Set the tractor beam off
		other.transform.FindChild ("tractorBeam").gameObject.SetActive (false);
	}

	void OnTriggerStay(Collider other){
		
		// Don't do shit if it ain't not our home
		if (!(Network.isServer && alien_home) && !(Network.isClient && human_home))
			return;

		if (other.gameObject.tag.CompareTo ("Player") != 0)
			return;

		if (!other.gameObject.networkView.isMine)
			return;

		ShipCapacity capacity = other.GetComponent<ShipCapacity> ();
		if(capacity){
			time_left -= Time.deltaTime;
			if (time_left <= 0.0f) {
				time_left = collect_rate;
				capacity.depositOre (element.ToUpper(), 1);
			}

			// Make the tractor beam face the planet
			Vector3 planet2ship = (other.transform.position - planet.transform.position).normalized;
			other.transform.FindChild ("tractorBeam").transform.position = 
				other.transform.position - planet2ship * capacity.tractor_beam_distance; 
			
			other.transform.FindChild ("tractorBeam").transform.LookAt (other.transform.position);
		}
	}
}
