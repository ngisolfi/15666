using UnityEngine;
using System.Collections;

public class respawnOtherIfTouching : MonoBehaviour {

	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.networkView.isMine)
		{
			//See unity cods for onCollisionEnter if we want this script to instantiate explosion
			collision.gameObject.GetComponent<Health> ().healthLevel = 0;
		}
	}
}
