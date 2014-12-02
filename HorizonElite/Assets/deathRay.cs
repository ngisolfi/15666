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
		if(LIcomplete){
			transform.Find("mirror/Rotator/green").gameObject.GetComponent<ParticleRenderer>().enabled=true;
			LIcomplete=false;
		}

		if(Dcomplete){
			transform.Find("mirror/Rotator/red").gameObject.GetComponent<ParticleRenderer>().enabled=true;
			Dcomplete=false;
		}

		if(Tcomplete){
			transform.Find("mirror/Rotator/blue").gameObject.GetComponent<ParticleRenderer>().enabled=true;
			Tcomplete=false;
		}

		if(HEcomplete){
			transform.Find("mirror/Rotator/green").gameObject.GetComponent<ParticleRenderer>().enabled=true;
			HEcomplete=false;
		}

		if(Bcomplete){
			transform.Find("mirror/Rotator/purple").gameObject.GetComponent<ParticleRenderer>().enabled=true;
			Bcomplete=false;
		}

		if(BEcomplete){
			transform.Find("mirror/Rotator/cyan").gameObject.GetComponent<ParticleRenderer>().enabled=true;
			BEcomplete=false;
		}


		if(DRcomplete && once){
			Debug.Log ("hopefully only once");
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
}
