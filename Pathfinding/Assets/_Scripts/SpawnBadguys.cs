using UnityEngine;
using System.Collections;

public class SpawnBadguys : MonoBehaviour {

	public Object badguy;
	public int count;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		if(count > enemies.Length){
			Instantiate(badguy,this.transform.position,this.transform.rotation);
		}
	}
}
