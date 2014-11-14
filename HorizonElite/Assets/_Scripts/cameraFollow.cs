using UnityEngine;
using System.Collections;

public class cameraFollow : MonoBehaviour {


	public Transform target;

	public float distanceBehind;
	public float distanceAbove;

	public float followDamping;
	public float lookAtDamping;



	// Use this for initialization
	void Start () {
	
	}

//	void LateUpdate(){
//		CameraUpdate ();
//	}
//	void Update(){
//				CameraUpdate ();
//		}
	void FixedUpdate(){
				CameraUpdate ();
		}
	// Update is called once per frame
	void CameraUpdate () {
	
		Quaternion lookDirection = target.rotation;//Quaternion.LookRotation (target.position - transform.position);
		transform.rotation = Quaternion.Lerp (transform.rotation,lookDirection, Time.deltaTime*lookAtDamping);

		//if(Vector3.Distance (transform.position,target.position)>distanceBehind){
			transform.position = Vector3.Lerp (transform.position,target.position-target.forward*distanceBehind+target.up*distanceAbove,Time.deltaTime*followDamping);
		//}
	}
}
