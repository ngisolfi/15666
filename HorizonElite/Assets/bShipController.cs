using UnityEngine;
using System.Collections;

public class bShipController : MonoBehaviour {
	
	public float speed;
	public float pitchSpeed;
	public float rollSpeed;

	public bool hasControl =false;
	//public float fireRate = 0.5F;
	//private float nextFire = 0.0F;
	
	//private Vector3 thrusterLocation;
	//private laserFire laserSpawn;
	//private AimLaser laserSight;
	// Use this for initialization
	void Start () {
		
		//		thrusterLocation = transform.Find("Thruster").position;
		//		laserSpawn = transform.Find ("laserSpawner").gameObject.GetComponent<laserFire>();
		//		laserSight = gameObject.GetComponent<AimLaser>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(hasControl){
			if (networkView.isMine) {
				if (Input.GetButton("Fire1")) {
					//Thrust On
					rigidbody.AddForce (speed * transform.forward);//(transform.position - thrusterLocation).normalized);
				}
				if (Input.GetButton("Fire2") ){//&& Time.time > nextFire) {
					Debug.Log ("firing deathray");
					//nextFire = Time.time + fireRate;
					//				laserSpawn.fireLaser (laserSight.target);
					//transform.Find("battleshipPrefab").gameObject.GetComponent<ParticleSystem>().enableEmission=true;
					gameObject.GetComponent<ParticleSystem>().enableEmission=true;
	
					transform.Find("mirror/Rotator/green").gameObject.GetComponent<ParticleEmitter>().minSize=200;
					transform.Find("mirror/Rotator/green").gameObject.GetComponent<ParticleEmitter>().maxSize=200;
					transform.Find("mirror/Rotator/green").gameObject.GetComponent<ParticleEmitter>().minEnergy=5;
					transform.Find("mirror/Rotator/green").gameObject.GetComponent<ParticleEmitter>().maxEnergy=5;
					transform.Find("mirror/Rotator/green").gameObject.GetComponent<ParticleEmitter>().minEmission=100;
					transform.Find("mirror/Rotator/green").gameObject.GetComponent<ParticleEmitter>().maxEmission=100;
					transform.Find("mirror/Rotator/green").gameObject.GetComponent<ParticleEmitter>().localVelocity=new Vector3(0f,0f,-1500f);
					transform.Find("mirror/Rotator/green").gameObject.GetComponent<ParticleAnimator>().rndForce=Vector3.one*300f;
	
					transform.Find("mirror/Rotator/red").gameObject.GetComponent<ParticleEmitter>().minSize=200;
					transform.Find("mirror/Rotator/red").gameObject.GetComponent<ParticleEmitter>().maxSize=200;
					transform.Find("mirror/Rotator/red").gameObject.GetComponent<ParticleEmitter>().minEnergy=5;
					transform.Find("mirror/Rotator/red").gameObject.GetComponent<ParticleEmitter>().maxEnergy=5;
					transform.Find("mirror/Rotator/red").gameObject.GetComponent<ParticleEmitter>().minEmission=100;
					transform.Find("mirror/Rotator/red").gameObject.GetComponent<ParticleEmitter>().maxEmission=100;
					transform.Find("mirror/Rotator/red").gameObject.GetComponent<ParticleEmitter>().localVelocity=new Vector3(0f,0f,-1500f);
					transform.Find("mirror/Rotator/red").gameObject.GetComponent<ParticleAnimator>().rndForce=Vector3.one*300f;
	
					transform.Find("mirror/Rotator/blue").gameObject.GetComponent<ParticleEmitter>().minSize=200;
					transform.Find("mirror/Rotator/blue").gameObject.GetComponent<ParticleEmitter>().maxSize=200;
					transform.Find("mirror/Rotator/blue").gameObject.GetComponent<ParticleEmitter>().minEnergy=5;
					transform.Find("mirror/Rotator/blue").gameObject.GetComponent<ParticleEmitter>().maxEnergy=5;
					transform.Find("mirror/Rotator/blue").gameObject.GetComponent<ParticleEmitter>().minEmission=100;
					transform.Find("mirror/Rotator/blue").gameObject.GetComponent<ParticleEmitter>().maxEmission=100;
					transform.Find("mirror/Rotator/blue").gameObject.GetComponent<ParticleEmitter>().localVelocity=new Vector3(0f,0f,-1500f);
					transform.Find("mirror/Rotator/blue").gameObject.GetComponent<ParticleAnimator>().rndForce=Vector3.one*300f;
	
					transform.Find("mirror/Rotator/yellow").gameObject.GetComponent<ParticleEmitter>().minSize=200;
					transform.Find("mirror/Rotator/yellow").gameObject.GetComponent<ParticleEmitter>().maxSize=200;
					transform.Find("mirror/Rotator/yellow").gameObject.GetComponent<ParticleEmitter>().minEnergy=5;
					transform.Find("mirror/Rotator/yellow").gameObject.GetComponent<ParticleEmitter>().maxEnergy=5;
					transform.Find("mirror/Rotator/yellow").gameObject.GetComponent<ParticleEmitter>().minEmission=100;
					transform.Find("mirror/Rotator/yellow").gameObject.GetComponent<ParticleEmitter>().maxEmission=100;
					transform.Find("mirror/Rotator/yellow").gameObject.GetComponent<ParticleEmitter>().localVelocity=new Vector3(0f,0f,-1500f);
					transform.Find("mirror/Rotator/yellow").gameObject.GetComponent<ParticleAnimator>().rndForce=Vector3.one*300f;
	
					transform.Find("mirror/Rotator/purple").gameObject.GetComponent<ParticleEmitter>().minSize=200;
					transform.Find("mirror/Rotator/purple").gameObject.GetComponent<ParticleEmitter>().maxSize=200;
					transform.Find("mirror/Rotator/purple").gameObject.GetComponent<ParticleEmitter>().minEnergy=5;
					transform.Find("mirror/Rotator/purple").gameObject.GetComponent<ParticleEmitter>().maxEnergy=5;
					transform.Find("mirror/Rotator/purple").gameObject.GetComponent<ParticleEmitter>().minEmission=100;
					transform.Find("mirror/Rotator/purple").gameObject.GetComponent<ParticleEmitter>().maxEmission=100;
					transform.Find("mirror/Rotator/purple").gameObject.GetComponent<ParticleEmitter>().localVelocity=new Vector3(0f,0f,-1500f);
					transform.Find("mirror/Rotator/purple").gameObject.GetComponent<ParticleAnimator>().rndForce=Vector3.one*300f;
	
					transform.Find("mirror/Rotator/cyan").gameObject.GetComponent<ParticleEmitter>().minSize=200;
					transform.Find("mirror/Rotator/cyan").gameObject.GetComponent<ParticleEmitter>().maxSize=200;
					transform.Find("mirror/Rotator/cyan").gameObject.GetComponent<ParticleEmitter>().minEnergy=5;
					transform.Find("mirror/Rotator/cyan").gameObject.GetComponent<ParticleEmitter>().maxEnergy=5;
					transform.Find("mirror/Rotator/cyan").gameObject.GetComponent<ParticleEmitter>().minEmission=100;
					transform.Find("mirror/Rotator/cyan").gameObject.GetComponent<ParticleEmitter>().maxEmission=100;
					transform.Find("mirror/Rotator/cyan").gameObject.GetComponent<ParticleEmitter>().localVelocity=new Vector3(0f,0f,-1500f);
					transform.Find("mirror/Rotator/cyan").gameObject.GetComponent<ParticleAnimator>().rndForce=Vector3.one*300f;
	
	
	
				} else {
	
					//transform.Find("battleshipPrefab").gameObject.GetComponent<ParticleSystem>().enableEmission=false;
					gameObject.GetComponent<ParticleSystem>().enableEmission=false;

					transform.Find("mirror/Rotator/green").gameObject.GetComponent<ParticleEmitter>().minSize=25;
					transform.Find("mirror/Rotator/green").gameObject.GetComponent<ParticleEmitter>().maxSize=25;
					transform.Find("mirror/Rotator/green").gameObject.GetComponent<ParticleEmitter>().minEnergy=2;
					transform.Find("mirror/Rotator/green").gameObject.GetComponent<ParticleEmitter>().maxEnergy=2;
					transform.Find("mirror/Rotator/green").gameObject.GetComponent<ParticleEmitter>().minEmission=100;
					transform.Find("mirror/Rotator/green").gameObject.GetComponent<ParticleEmitter>().maxEmission=100;
					transform.Find("mirror/Rotator/green").gameObject.GetComponent<ParticleEmitter>().localVelocity=Vector3.zero;
					transform.Find("mirror/Rotator/green").gameObject.GetComponent<ParticleAnimator>().rndForce=Vector3.zero;
	
					transform.Find("mirror/Rotator/red").gameObject.GetComponent<ParticleEmitter>().minSize=25;
					transform.Find("mirror/Rotator/red").gameObject.GetComponent<ParticleEmitter>().maxSize=25;
					transform.Find("mirror/Rotator/red").gameObject.GetComponent<ParticleEmitter>().minEnergy=2;
					transform.Find("mirror/Rotator/red").gameObject.GetComponent<ParticleEmitter>().maxEnergy=2;
					transform.Find("mirror/Rotator/red").gameObject.GetComponent<ParticleEmitter>().minEmission=100;
					transform.Find("mirror/Rotator/red").gameObject.GetComponent<ParticleEmitter>().maxEmission=100;
					transform.Find("mirror/Rotator/red").gameObject.GetComponent<ParticleEmitter>().localVelocity=Vector3.zero;
					transform.Find("mirror/Rotator/red").gameObject.GetComponent<ParticleAnimator>().rndForce=Vector3.zero;
	
					transform.Find("mirror/Rotator/blue").gameObject.GetComponent<ParticleEmitter>().minSize=25;
					transform.Find("mirror/Rotator/blue").gameObject.GetComponent<ParticleEmitter>().maxSize=25;
					transform.Find("mirror/Rotator/blue").gameObject.GetComponent<ParticleEmitter>().minEnergy=2;
					transform.Find("mirror/Rotator/blue").gameObject.GetComponent<ParticleEmitter>().maxEnergy=2;
					transform.Find("mirror/Rotator/blue").gameObject.GetComponent<ParticleEmitter>().minEmission=100;
					transform.Find("mirror/Rotator/blue").gameObject.GetComponent<ParticleEmitter>().maxEmission=100;
					transform.Find("mirror/Rotator/blue").gameObject.GetComponent<ParticleEmitter>().localVelocity=Vector3.zero;
					transform.Find("mirror/Rotator/blue").gameObject.GetComponent<ParticleAnimator>().rndForce=Vector3.zero;
	
					transform.Find("mirror/Rotator/yellow").gameObject.GetComponent<ParticleEmitter>().minSize=25;
					transform.Find("mirror/Rotator/yellow").gameObject.GetComponent<ParticleEmitter>().maxSize=25;
					transform.Find("mirror/Rotator/yellow").gameObject.GetComponent<ParticleEmitter>().minEnergy=2;
					transform.Find("mirror/Rotator/yellow").gameObject.GetComponent<ParticleEmitter>().maxEnergy=2;
					transform.Find("mirror/Rotator/yellow").gameObject.GetComponent<ParticleEmitter>().minEmission=100;
					transform.Find("mirror/Rotator/yellow").gameObject.GetComponent<ParticleEmitter>().maxEmission=100;
					transform.Find("mirror/Rotator/yellow").gameObject.GetComponent<ParticleEmitter>().localVelocity=Vector3.zero;
					transform.Find("mirror/Rotator/yellow").gameObject.GetComponent<ParticleAnimator>().rndForce=Vector3.zero;
	
					transform.Find("mirror/Rotator/purple").gameObject.GetComponent<ParticleEmitter>().minSize=25;
					transform.Find("mirror/Rotator/purple").gameObject.GetComponent<ParticleEmitter>().maxSize=25;
					transform.Find("mirror/Rotator/purple").gameObject.GetComponent<ParticleEmitter>().minEnergy=2;
					transform.Find("mirror/Rotator/purple").gameObject.GetComponent<ParticleEmitter>().maxEnergy=2;
					transform.Find("mirror/Rotator/purple").gameObject.GetComponent<ParticleEmitter>().minEmission=100;
					transform.Find("mirror/Rotator/purple").gameObject.GetComponent<ParticleEmitter>().maxEmission=100;
					transform.Find("mirror/Rotator/purple").gameObject.GetComponent<ParticleEmitter>().localVelocity=Vector3.zero;
					transform.Find("mirror/Rotator/purple").gameObject.GetComponent<ParticleAnimator>().rndForce=Vector3.zero;
	
					transform.Find("mirror/Rotator/cyan").gameObject.GetComponent<ParticleEmitter>().minSize=25;
					transform.Find("mirror/Rotator/cyan").gameObject.GetComponent<ParticleEmitter>().maxSize=25;
					transform.Find("mirror/Rotator/cyan").gameObject.GetComponent<ParticleEmitter>().minEnergy=2;
					transform.Find("mirror/Rotator/cyan").gameObject.GetComponent<ParticleEmitter>().maxEnergy=2;
					transform.Find("mirror/Rotator/cyan").gameObject.GetComponent<ParticleEmitter>().minEmission=100;
					transform.Find("mirror/Rotator/cyan").gameObject.GetComponent<ParticleEmitter>().maxEmission=100;
					transform.Find("mirror/Rotator/cyan").gameObject.GetComponent<ParticleEmitter>().localVelocity=Vector3.zero;
					transform.Find("mirror/Rotator/cyan").gameObject.GetComponent<ParticleAnimator>().rndForce=Vector3.zero;
	
	
	
	
	
				}
				if (Input.GetAxis("Vertical") > 0f) {
					
					rigidbody.AddRelativeTorque (new Vector3 (pitchSpeed, 0, 0));
					//rotate to fly down
				}
				if (Input.GetAxis("Vertical") < 0f) {
					//rotate to fly up
					rigidbody.AddRelativeTorque (new Vector3 (-pitchSpeed, 0, 0));
				}
				if (Input.GetAxis("Horizontal") < 0f) {
					//roll to the left
					rigidbody.AddRelativeTorque (new Vector3 (0, 0, rollSpeed));
				}
				if (Input.GetAxis("Horizontal") > 0f) {
					//roll to the right
					rigidbody.AddRelativeTorque (new Vector3 (0, 0, -rollSpeed));
				}
			}
		}
	}
}