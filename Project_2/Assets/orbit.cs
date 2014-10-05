using UnityEngine;
using System.Collections;

public class orbit : MonoBehaviour {

	public float ang_vel;

	// Update is called once per frame
	void Update () {
	
		Vector3 temp = new Vector3 (0, ang_vel * Time.deltaTime, 0);
		transform.Rotate (temp);

	}
}
