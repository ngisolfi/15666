using UnityEngine;
using System.Collections;

public class Die : MonoBehaviour {

	public GameObject onDeathExplosion;
	private Health health;
//	private AudioSource point;
//	public GameObject currentDetonator;
//	private int _currentExpIdx = -1;
//	
//	public float explosionLife = 10;
//	public float timeScale = 1.0f;
//	public float detailLevel = 1.0f;

	// Use this for initialization
	void Start () {
//		point = GetComponent<AudioSource> ();
		health = this.gameObject.GetComponent<Health>();
	}
	
	// Update is called once per frame
	void Update () {
		if(health.health <= 0f){
//			SpawnExplosion();
			Network.Instantiate (onDeathExplosion,transform.position,Quaternion.identity,0);

			Network.Destroy(gameObject);
			if(gameObject.tag=="asteroid"){
			Destroy (gameObject);
			}
		}
	}



//	private void SpawnExplosion()
//	{
//		//Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//		//RaycastHit hit;
//		//if (Physics.Raycast(ray, out hit, 1000))
//		//{
//		Detonator dTemp = (Detonator)currentDetonator.GetComponent("Detonator");
//		
//		float offsetSize = dTemp.size/3;
//		Vector3 hitPoint = transform.position;// hit.point +
//		//((Vector3.Scale(hit.normal, new Vector3(offsetSize, offsetSize, offsetSize))));
//		GameObject exp = (GameObject) Instantiate(currentDetonator, hitPoint, Quaternion.identity);
//		dTemp = (Detonator)exp.GetComponent("Detonator");
//		dTemp.detail = detailLevel;
//		dTemp.size = 4;
//		Destroy(exp, explosionLife);
//		//}
//		
//		
//	}
}
