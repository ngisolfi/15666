using UnityEngine;
using System.Collections;

public class winCondition : MonoBehaviour {

	public GameObject enemySun;
	public GameObject sunExplosion;

	private int sunDamage = 0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay( Collider other ){
		if(other.tag == "battleship" && other.networkView.isMine)
			sunDamage += 1;
		if (sunDamage > 1000){
			Network.Instantiate (sunExplosion,enemySun.transform.position,Quaternion.identity,0);
			GameObject.Destroy (enemySun);
		}

}
