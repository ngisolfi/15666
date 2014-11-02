using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	private ShipController controller;
	private bool attractToPoint;
	private Vector3 attractPoint;
	public float closenessThreshold;

	// Use this for initialization
	void Start () {
		controller = gameObject.GetComponent<ShipController>();
	}
	
	// Update is called once per frame
	void Update () {
		if(attractToPoint){
			Vector3 targetDirection = attractPoint-transform.position;
			targetDirection.y = 0f;
			
			float torque = 0.1f*AngleSigned(transform.forward, targetDirection, Vector3.up) - 0.5f*rigidbody.angularVelocity.y;

			controller.Torque(torque);
			if(Mathf.Abs(torque) < 30f)
				controller.Thrust();
		}
	}

	public static float AngleSigned(Vector3 v1, Vector3 v2, Vector3 n)
	{
		return Mathf.Atan2(
			Vector3.Dot(n, Vector3.Cross(v1, v2)),
			Vector3.Dot(v1, v2)) * Mathf.Rad2Deg;
	}

	public void setPointAttractor(Vector3 point){
		attractPoint = point;
		attractToPoint = true;
	}

	public void stopAttractor(){
		attractToPoint = false;
	}

	public bool arrivedAtPoint(){
		if(attractToPoint)
			return (transform.position-attractPoint).sqrMagnitude < closenessThreshold*closenessThreshold;
		else
			return true;
	}

	public Vector3 currentPoint{
		get
		{
			return transform.position;
		}
	}
}
