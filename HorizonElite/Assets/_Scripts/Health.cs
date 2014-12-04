using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	public int max_health = 100;
	public int healthLevel {get; set;}

	public GameObject explosion;
	public Transform spawn;

	private float temp_drag, temp_ang_drag;
	
	// Use this for initialization
	void Start () {
		healthLevel = 100;
	}
	
	// Update is called once per frame
	void Update () {
	
		if(healthLevel<=0){
			Network.Instantiate (explosion,transform.position,Quaternion.identity,0);
			if(spawn!=null){
				
				disableShip();
				StartCoroutine("respawnTimer");
				healthLevel = 100;

			}else
				Network.Destroy (gameObject);


		}

	}
	
	IEnumerator respawnTimer(){
	
		yield return new WaitForSeconds(3);
		enableShip();
	}

	void disableShip()
	{
		transform.FindChild("laserSpawner").GetComponent<laserFire>().can_fire=false;
		gameObject.GetComponent<ShipCapacity>().destroyLoad();
		transform.Find ("Thruster").gameObject.GetComponent<TrailRenderer>().time=0f;
		temp_drag=rigidbody.drag;
		temp_ang_drag=rigidbody.angularDrag;
		renderer.enabled=false;
		transform.Find("Thruster").GetComponent<ParticleSystem>().renderer.enabled=false;
		rigidbody.drag=1e08f;
		rigidbody.angularDrag=1e08f;
		Invoke("trailReset",0.01f);
	}
	
	void enableShip()
	{
		transform.FindChild("laserSpawner").GetComponent<laserFire>().can_fire=true;
		transform.position = spawn.position;
		renderer.enabled=true;
		rigidbody.drag=temp_drag;
		rigidbody.angularDrag=temp_ang_drag;
		transform.Find("Thruster").GetComponent<ParticleSystem>().renderer.enabled=true;
	}

	void trailReset(){


		transform.Find ("Thruster").gameObject.GetComponent<TrailRenderer>().time = 15f;

	}
}
