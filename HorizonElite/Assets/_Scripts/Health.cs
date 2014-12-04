using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	public int max_health = 100;
	public int healthLevel {get; set;}

	public GameObject explosion;
	public Transform spawn;

	private float temp_drag, temp_ang_drag;

	[HideInInspector]
	public bool ship_disabled = false;

	public float healthFraction(){
		return (float) healthLevel/(float) max_health;
	}

	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info) {
		int health = 0;
//		bool ship_broke = false;
		
		if (stream.isWriting) {
			health = healthLevel;
//			ship_broke = ship_disabled;
			stream.Serialize(ref health);
//			stream.Serialize(ref ship_broke);
		} else {
			stream.Serialize(ref health);
//			stream.Serialize(ref ship_broke);
			healthLevel = health;
//			ship_disabled = ship_broke;
		}
	}


	[RPC]
	public void dealDamage( int damage ){

		if(gameObject.GetComponent<StateHandler>().playerControlled)
			healthLevel -= damage;
		else
			healthLevel -= 100*damage;
	//	if(healthLevel<0)
	//		healthLevel=0;

	}


	// Use this for initialization
	void Start () {
//		healthLevel = 100;
		healthLevel = max_health;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		if (networkView.isMine)
		{
			Debug.Log ("health: " + healthLevel.ToString() + " netView ID: " + networkView.viewID);
			if(healthLevel==0 && !ship_disabled){
				Network.Instantiate (explosion,transform.position,Quaternion.identity,0);
				if(spawn!=null){
					
					disableShip();
					StartCoroutine("respawnTimer");
						
		
				}else
					Network.Destroy (gameObject);

			}
		}
	}
	
	IEnumerator respawnTimer(){
	
		yield return new WaitForSeconds(3);
		enableShip();
	}

	void disableShip()
	{
		gameObject.tag = "Untagged";
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
		ship_disabled = true;
//		healthLevel = -10;//wtf? bit shift hax


	}
	
	void enableShip()
	{

		transform.FindChild("laserSpawner").GetComponent<laserFire>().can_fire=true;		
		transform.position = spawn.Find("mirror/spawn_point").position;
		transform.LookAt(spawn.position);
		renderer.enabled=true;
		rigidbody.drag=temp_drag;
		rigidbody.angularDrag=temp_ang_drag;
		transform.Find("Thruster").GetComponent<ParticleSystem>().renderer.enabled=true;
		gameObject.tag = "Player";
		healthLevel = max_health;
		ship_disabled = false;
//		healthLevel = 100;
	}

	void trailReset(){


		transform.Find ("Thruster").gameObject.GetComponent<TrailRenderer>().time = 15f;

	}
}
