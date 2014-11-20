﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UI_payload : MonoBehaviour {
	private Dictionary<string,GameObject> quads;
	public Material beryllium;
	public Material boron;
	public Material deuterium;
	public Material helium;
	public Material lithium;
	public Material tritium;
	private TextMesh payloadText;
	private Light payloadLight;
	private bool lightIncreasing=true;

	public GameObject playerShip;

	GameObject setQuad(Material material){
		GameObject quad = GameObject.CreatePrimitive (PrimitiveType.Quad);
		
		// Set the color of the new quad based on its material
		quad.GetComponent<MeshRenderer> ().material = material;
		
		// Set the quad to be in the GUI rendering layer
		quad.layer = 21;
		
		// Fix this once we begin snapping to screen size
//		quad.transform.position = new Vector3 (.6f, -0.9f, 0.0f);
		quad.transform.localScale = new Vector3 (0.0f, 0.1f, 0.001f);
		return quad;
	}

	void resetQuads(){
		Vector3 startPos = transform.position;
//		startPos.x -= renderer.bounds.extents.x*0.5f;
		foreach(GameObject quad in quads.Values){
			quad.transform.position = startPos;
			quad.transform.localScale = new Vector3 (0.0f, 0.1f, 0.001f);
		}
	}

	// Use this for initialization
	void Awake () {
		quads = new Dictionary<string, GameObject>();
		quads["BERYLLIUM"] = setQuad(beryllium);
		quads["BORON"] = setQuad(boron);
		quads["DEUTERIUM"] = setQuad(deuterium);
		quads["HELIUM"] = setQuad(helium);
		quads["LITHIUM"] = setQuad(lithium);
		quads["TRITIUM"] = setQuad(tritium);
	}

	void Start(){
		payloadText = GameObject.Find ("payloadPercent").GetComponent<TextMesh>();
		payloadLight = GameObject.Find ("payloadLight").GetComponent<Light> ();
	}

	// Update is called once per frame
	void Update () {
		if(playerShip){
			ShipCapacity capacity = playerShip.GetComponent<ShipCapacity>();
			if(capacity){
				resetQuads();
				float size = transform.renderer.bounds.extents.x*2f;
				float xpos = transform.position.x - renderer.bounds.extents.x;
				Vector3 quadPos;
				Vector3 quadScale;
				float barSize;
				foreach(string element in capacity.fill_order){
					barSize = capacity.elementFraction(element)*size;
					quadPos = quads[element].transform.position;
					quadPos.x = xpos + barSize*0.5f;
					quads[element].transform.localPosition = quadPos;
					quadScale = quads[element].transform.localScale;
					quadScale.x = barSize;
					quads[element].transform.localScale = quadScale;
					xpos += quadScale.x;
				}
				payloadText.text = "("+capacity.percentFull().ToString()+"%)";
			}
		}
	}

	void FixedUpdate(){
		if(playerShip){
			ShipCapacity capacity = playerShip.GetComponent<ShipCapacity>();
			if(capacity){
				if (capacity.percentFull() == 100) {
					if(payloadLight.intensity<=0f){
						lightIncreasing=true;
					} else if (payloadLight.intensity>=4f){
						lightIncreasing=false;
					}
					
					if(lightIncreasing){
						payloadLight.intensity+=.2f;
					} else {
						payloadLight.intensity-=.2f;
					}
					
					return;
				}
			}
		}
	}
}