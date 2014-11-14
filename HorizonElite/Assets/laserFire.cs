using UnityEngine;
using System.Collections;

public class laserFire : MonoBehaviour {

	public GameObject laserShot;
	
	public void fireLaser(){

		Instantiate(laserShot, transform.position, Quaternion.LookRotation(transform.forward));

	}
}
