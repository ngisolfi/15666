using UnityEngine;
using System.Collections;

public class planetRotation : MonoBehaviour {
	
	public float ang_vel;
	
	// Update is called once per frame
	void Update () {
		
		Vector3 temp = new Vector3 ( 0f, ang_vel * Time.deltaTime,0f);//new Vector3 (ang_vel * Time.deltaTime, 0F, 0F);
		transform.Rotate (temp);
		
	}
}
