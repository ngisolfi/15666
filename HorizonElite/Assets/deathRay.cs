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


	[RPC]
	public void turnOnGreen(){

		transform.Find ("mirror/Rotator/green").gameObject.GetComponent<ParticleRenderer> ().enabled = true;

	}

	[RPC]
	public void turnOnRed(){
		
		transform.Find ("mirror/Rotator/red").gameObject.GetComponent<ParticleRenderer> ().enabled = true;
		
	}
	[RPC]
	public void turnOnBlue(){
		
		transform.Find ("mirror/Rotator/blue").gameObject.GetComponent<ParticleRenderer> ().enabled = true;
		
	}
	[RPC]
	public void turnOnYellow(){
		
		transform.Find ("mirror/Rotator/yellow").gameObject.GetComponent<ParticleRenderer> ().enabled = true;
		
	}
	[RPC]
	public void turnOnPurple(){
		
		transform.Find ("mirror/Rotator/purple").gameObject.GetComponent<ParticleRenderer> ().enabled = true;
		
	}
	[RPC]
	public void turnOnCyan(){
		
		transform.Find ("mirror/Rotator/cyan").gameObject.GetComponent<ParticleRenderer> ().enabled = true;
		
	}


	// Update is called once per frame
	void Update () {
		if(LIcomplete){
			//transform.Find("mirror/Rotator/green").gameObject.GetComponent<ParticleRenderer>().enabled=true;
			gameObject.networkView.RPC ("turnOnGreen",RPCMode.AllBuffered);
		}
		if(Dcomplete)
			//transform.Find("mirror/Rotator/red").gameObject.GetComponent<ParticleRenderer>().enabled=true;
			gameObject.networkView.RPC ("turnOnRed",RPCMode.AllBuffered);


		if(Tcomplete)
			//transform.Find("mirror/Rotator/blue").gameObject.GetComponent<ParticleRenderer>().enabled=true;
			gameObject.networkView.RPC ("turnOnBlue",RPCMode.AllBuffered);


		if(HEcomplete)
			//transform.Find("mirror/Rotator/yellow").gameObject.GetComponent<ParticleRenderer>().enabled=true;
			gameObject.networkView.RPC ("turnOnYellow",RPCMode.AllBuffered);


		if(Bcomplete)
			//transform.Find("mirror/Rotator/purple").gameObject.GetComponent<ParticleRenderer>().enabled=true;
			gameObject.networkView.RPC ("turnOnPurple",RPCMode.AllBuffered);


		if(BEcomplete)
			//transform.Find("mirror/Rotator/cyan").gameObject.GetComponent<ParticleRenderer>().enabled=true;
			gameObject.networkView.RPC ("turnOnCyan",RPCMode.AllBuffered);

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
			
			// Play the audio clip for death ray instructions
			GameObject.Find("GameInstance").GetComponent<networkManager>().playDeathRayInstructionsAudio();
		
		}
	}
}
