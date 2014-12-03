using UnityEngine;
using System.Collections;

public class bShipController : MonoBehaviour {
	
	public float speed;
	public float pitchSpeed;
	public float rollSpeed;

	public bool hasControl =false;
	//public float fireRate = 0.5F;
	//private float nextFire = 0.0F;

	private bool crosshairEnabled = true;
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
			if (crosshairEnabled)
			{
				if (Network.isServer)
					GameObject.Find("p1UI/Crosshair").GetComponent<MeshRenderer>().enabled = false;
				else
					GameObject.Find("p2UI/Crosshair").GetComponent<MeshRenderer>().enabled = false;

				crosshairEnabled = false;
			}

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
					if(!gameObject.GetComponent<ParticleSystem>().isPlaying)
						gameObject.GetComponent<ParticleSystem>().Play();

					gameObject.GetComponent<ParticleSystem>().enableEmission=true;

					float size = 200.0f;
					Vector3 force = Vector3.one*300.0f;
					float time = 5.0f;
					float speed = 1500.0f;
					transform.Find("mirror/Rotator/green").gameObject.GetComponent<ParticleEmitter>().minSize=size;
					transform.Find("mirror/Rotator/green").gameObject.GetComponent<ParticleEmitter>().maxSize=size;
					transform.Find("mirror/Rotator/green").gameObject.GetComponent<ParticleEmitter>().minEnergy=time;
					transform.Find("mirror/Rotator/green").gameObject.GetComponent<ParticleEmitter>().maxEnergy=time;
					transform.Find("mirror/Rotator/green").gameObject.GetComponent<ParticleEmitter>().minEmission=100;
					transform.Find("mirror/Rotator/green").gameObject.GetComponent<ParticleEmitter>().maxEmission=100;
					transform.Find("mirror/Rotator/green").gameObject.GetComponent<ParticleEmitter>().localVelocity=new Vector3(0f,0f,-speed);
					transform.Find("mirror/Rotator/green").gameObject.GetComponent<ParticleAnimator>().rndForce=force;
	
					transform.Find("mirror/Rotator/red").gameObject.GetComponent<ParticleEmitter>().minSize=size;
					transform.Find("mirror/Rotator/red").gameObject.GetComponent<ParticleEmitter>().maxSize=size;
					transform.Find("mirror/Rotator/red").gameObject.GetComponent<ParticleEmitter>().minEnergy=time;
					transform.Find("mirror/Rotator/red").gameObject.GetComponent<ParticleEmitter>().maxEnergy=time;
					transform.Find("mirror/Rotator/red").gameObject.GetComponent<ParticleEmitter>().minEmission=100;
					transform.Find("mirror/Rotator/red").gameObject.GetComponent<ParticleEmitter>().maxEmission=100;
					transform.Find("mirror/Rotator/red").gameObject.GetComponent<ParticleEmitter>().localVelocity=new Vector3(0f,0f,-speed);
					transform.Find("mirror/Rotator/red").gameObject.GetComponent<ParticleAnimator>().rndForce=force;
	
					transform.Find("mirror/Rotator/blue").gameObject.GetComponent<ParticleEmitter>().minSize=size;
					transform.Find("mirror/Rotator/blue").gameObject.GetComponent<ParticleEmitter>().maxSize=size;
					transform.Find("mirror/Rotator/blue").gameObject.GetComponent<ParticleEmitter>().minEnergy=time;
					transform.Find("mirror/Rotator/blue").gameObject.GetComponent<ParticleEmitter>().maxEnergy=time;
					transform.Find("mirror/Rotator/blue").gameObject.GetComponent<ParticleEmitter>().minEmission=100;
					transform.Find("mirror/Rotator/blue").gameObject.GetComponent<ParticleEmitter>().maxEmission=100;
					transform.Find("mirror/Rotator/blue").gameObject.GetComponent<ParticleEmitter>().localVelocity=new Vector3(0f,0f,-speed);
					transform.Find("mirror/Rotator/blue").gameObject.GetComponent<ParticleAnimator>().rndForce=force;
	
					transform.Find("mirror/Rotator/yellow").gameObject.GetComponent<ParticleEmitter>().minSize=size;
					transform.Find("mirror/Rotator/yellow").gameObject.GetComponent<ParticleEmitter>().maxSize=size;
					transform.Find("mirror/Rotator/yellow").gameObject.GetComponent<ParticleEmitter>().minEnergy=time;
					transform.Find("mirror/Rotator/yellow").gameObject.GetComponent<ParticleEmitter>().maxEnergy=time;
					transform.Find("mirror/Rotator/yellow").gameObject.GetComponent<ParticleEmitter>().minEmission=100;
					transform.Find("mirror/Rotator/yellow").gameObject.GetComponent<ParticleEmitter>().maxEmission=100;
					transform.Find("mirror/Rotator/yellow").gameObject.GetComponent<ParticleEmitter>().localVelocity=new Vector3(0f,0f,-speed);
					transform.Find("mirror/Rotator/yellow").gameObject.GetComponent<ParticleAnimator>().rndForce=force;
	
					transform.Find("mirror/Rotator/purple").gameObject.GetComponent<ParticleEmitter>().minSize=size;
					transform.Find("mirror/Rotator/purple").gameObject.GetComponent<ParticleEmitter>().maxSize=size;
					transform.Find("mirror/Rotator/purple").gameObject.GetComponent<ParticleEmitter>().minEnergy=time;
					transform.Find("mirror/Rotator/purple").gameObject.GetComponent<ParticleEmitter>().maxEnergy=time;
					transform.Find("mirror/Rotator/purple").gameObject.GetComponent<ParticleEmitter>().minEmission=100;
					transform.Find("mirror/Rotator/purple").gameObject.GetComponent<ParticleEmitter>().maxEmission=100;
					transform.Find("mirror/Rotator/purple").gameObject.GetComponent<ParticleEmitter>().localVelocity=new Vector3(0f,0f,-speed);
					transform.Find("mirror/Rotator/purple").gameObject.GetComponent<ParticleAnimator>().rndForce=force;
	
					transform.Find("mirror/Rotator/cyan").gameObject.GetComponent<ParticleEmitter>().minSize=size;
					transform.Find("mirror/Rotator/cyan").gameObject.GetComponent<ParticleEmitter>().maxSize=size;
					transform.Find("mirror/Rotator/cyan").gameObject.GetComponent<ParticleEmitter>().minEnergy=time;
					transform.Find("mirror/Rotator/cyan").gameObject.GetComponent<ParticleEmitter>().maxEnergy=time;
					transform.Find("mirror/Rotator/cyan").gameObject.GetComponent<ParticleEmitter>().minEmission=100;
					transform.Find("mirror/Rotator/cyan").gameObject.GetComponent<ParticleEmitter>().maxEmission=100;
					transform.Find("mirror/Rotator/cyan").gameObject.GetComponent<ParticleEmitter>().localVelocity=new Vector3(0f,0f,-speed);
					transform.Find("mirror/Rotator/cyan").gameObject.GetComponent<ParticleAnimator>().rndForce=force;
	
	
	
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