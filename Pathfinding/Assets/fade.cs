using UnityEngine;
using System.Collections;

public class fade : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
		StartCoroutine ("wait");
	}


	IEnumerator wait() {
	
		yield return new WaitForSeconds(2);
	
		Network.Destroy (this.gameObject.networkView.viewID);
	
	}

}