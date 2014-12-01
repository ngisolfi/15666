using UnityEngine;
using System.Collections;

public class laserFire : MonoBehaviour {

	public GameObject laserShot;
	public float laserSpeed = 1000f;

	public void fireLaser(Vector3 target){

		GameObject laser = (GameObject) Instantiate(laserShot, transform.position, Quaternion.LookRotation(target - transform.position));
		SU_LaserShot shot = laser.GetComponent<SU_LaserShot>();
		if(shot){
			shot.firedBy = transform.parent;
			shot.velocity = laserSpeed;
		}
	}
}
