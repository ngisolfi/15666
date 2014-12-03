using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	public int max_health = 100;
	public int healthLevel {get; set;}

	public GameObject explosion;
	public Transform spawn;

	// Use this for initialization
	void Start () {
		healthLevel = 100;
	}
	
	// Update is called once per frame
	void Update () {
	
		if(healthLevel<=0){
			Network.Instantiate (explosion,transform.position,Quaternion.identity,0);
			if(spawn!=null)
				transform.position = spawn.position;
			else
				Network.Destroy (gameObject);

		}

	}
}
