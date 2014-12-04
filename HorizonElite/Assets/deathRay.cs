using UnityEngine;
using System.Collections;

public class deathRay : MonoBehaviour {

	public bool DRcomplete = false;
	public bool LIcomplete = false;
	public bool Dcomplete = false;
	public bool Tcomplete = false;
	public bool HEcomplete = false;
	public bool Bcomplete = false;
	public bool BEcomplete = false;

	private bool once = true;

	//private GameObject rotator;

	// Use this for initialization
	void Start () {
		//rotator = GameObject.Find ("/Rotator");
	}
	
	// Update is called once per frame
	void Update () {
		if(LIcomplete)//{
			transform.Find("mirror/Rotator/green").gameObject.GetComponent<ParticleRenderer>().enabled=true;
			//LIcomplete=false;
		//}

		if(Dcomplete)//{
			transform.Find("mirror/Rotator/red").gameObject.GetComponent<ParticleRenderer>().enabled=true;
		//	Dcomplete=false;
		//}

		if(Tcomplete)//{
			transform.Find("mirror/Rotator/blue").gameObject.GetComponent<ParticleRenderer>().enabled=true;
			//Tcomplete=false;
		//}

		if(HEcomplete)//{
			transform.Find("mirror/Rotator/green").gameObject.GetComponent<ParticleRenderer>().enabled=true;
			//HEcomplete=false;
		//}

		if(Bcomplete)//{
			transform.Find("mirror/Rotator/purple").gameObject.GetComponent<ParticleRenderer>().enabled=true;
			//Bcomplete=false;
		//}

		if(BEcomplete)//{
			transform.Find("mirror/Rotator/cyan").gameObject.GetComponent<ParticleRenderer>().enabled=true;
			//BEcomplete=false;
		//}


		if(DRcomplete && once){
			//Don't do this twice
			once=false;

			//turn off the planet orbiter for this ship
			this.GetComponentInParent<planetOrbit>().enabled=false;

			//have the camera follow this battleship
			if(Network.isServer){
				//player 1
				GameObject.Find("camView_p1").GetComponent<cameraFollow>().target = transform;//.Find("humanOrbiter");
				GameObject.Find ("camView_p1").GetComponent<cameraFollow>().distanceBehind = 800;
				GameObject.Find ("camView_p1").GetComponent<cameraFollow>().distanceAbove = 200;
				GameObject.Find ("player1").SetActive(false);
			} else {
				GameObject.Find ("camView_p2").GetComponent<cameraFollow>().target = transform;//.Find ("alienOrbiter");
				GameObject.Find ("camView_p2").GetComponent<cameraFollow>().distanceBehind = 800;
				GameObject.Find ("camView_p2").GetComponent<cameraFollow>().distanceAbove = 200;
				GameObject.Find ("player2").SetActive(false);
			}

			//Turn on thrust script for this ship
			//this.GetComponent<thrustController>().enabled = true;

		}
	}
	
	// If one element is completely filled when it is dumped off, OreCapacity.cs 
	// will call this function
	public void completeElement(string element)
	{
		if (element.ToUpper().CompareTo("BERYLLIUM") == 0)
			BEcomplete = true;	
		if (element.ToUpper().CompareTo("BORON") == 0)
			Bcomplete = true;	
		if (element.ToUpper().CompareTo("DEUTERIUM") == 0)
			Dcomplete = true;	
		if (element.ToUpper().CompareTo("HELIUM") == 0)
			HEcomplete = true;	
		if (element.ToUpper().CompareTo("LITHIUM") == 0)
			LIcomplete = true;	
		if (element.ToUpper().CompareTo("TRITIUM") == 0)
			Tcomplete = true;				
			
		// If all elements are complete, so is the death ray 
		if (BEcomplete && Bcomplete && Dcomplete && HEcomplete && LIcomplete && Tcomplete)
		{
			DRcomplete = true;
			gameObject.GetComponent<bShipController>().hasControl = true;
		}
	}
}
