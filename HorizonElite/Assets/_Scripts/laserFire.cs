using UnityEngine;
using System.Collections;

public class laserFire : MonoBehaviour {

	public GameObject laserShot;
	public float laserSpeed;
	
	public bool can_fire = true;

	public void fireLaser(Vector3 target){
		
		// Allow other scripts to turn this on and off
		if (!can_fire)
			return;
			
		GameObject laser = (GameObject) Network.Instantiate(laserShot, transform.position, Quaternion.LookRotation(target-transform.position),0);
		SU_LaserShot shot = laser.GetComponent<SU_LaserShot>();
		if(shot){
			shot.firedBy = transform.parent;
			shot.velocity = laserSpeed + 2.0f * transform.parent.rigidbody.velocity.magnitude;
		}
	}
}
