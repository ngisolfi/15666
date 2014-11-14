using UnityEngine;
using System.Collections;


public class mineable : MonoBehaviour {

	//public bool DEUTERIUM,TRITIUM,HELIUM,LITHIUM,BERYLLIUM,BORON;

	//The element we are wanting to fill
	public progressBar overlayProgressBar;
	public float amountPerFrame;
	//private progressBar elementProgress;

	// Use this for initialization
	void Start () {

		//elementProgress = overlayProgressBar.GetComponent<progressBar> ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay( Collider other){

		overlayProgressBar.changeOre (amountPerFrame);

	}
}
