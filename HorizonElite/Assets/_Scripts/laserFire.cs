using UnityEngine;
using System.Collections;

public class laserFire : MonoBehaviour {

	public GameObject laserShot;


	public void fireLaser(){

		GameObject laser = (GameObject) Instantiate(laserShot, transform.position, Quaternion.LookRotation(transform.forward));
		laser.GetComponent<SU_LaserShot>().firedBy = transform.parent;

	}
}
