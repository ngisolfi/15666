using UnityEngine;
using System.Collections;

public class planetOrbit : planetRotation {

	private Transform _planet;

	public Transform planet
	{
		get
		{
			return _planet;
		}
		
		set{
			_planet = value;
			transform.position = value.position;
			transform.rotation = value.rotation;
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(networkView.isMine){
			Vector3 temp = new Vector3 ( 0f, ang_vel * Time.deltaTime,0f);//new Vector3 (ang_vel * Time.deltaTime, 0F, 0F);
			transform.position = _planet.position;
			transform.Rotate (temp);
		}
	}
}
