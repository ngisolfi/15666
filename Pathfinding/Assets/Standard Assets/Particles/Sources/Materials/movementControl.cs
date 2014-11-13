using UnityEngine;
using System.Collections;

public class movementControl: MonoBehaviour {

	private Vector3 mouse;



	void InputMovement(){
		if (Input.GetKey (KeyCode.W)){
	//		if(rigidbody.position.x>0)
			rigidbody.MovePosition (rigidbody.position + new Vector3 (transform.forward.x * 3.18f, 0f, transform.forward.z * 3.18f));
		}
		if (Input.GetKey (KeyCode.S)){
			rigidbody.MovePosition (rigidbody.position + new Vector3 (-transform.forward.x * 3.18f, 0f, -transform.forward.z * 3.18f));
		}
		if (Input.GetKey (KeyCode.A)){
			rigidbody.MovePosition (rigidbody.position + new Vector3 (-transform.right.x * 3.18f, 0f, -transform.right.z * 3.18f));
		}
		if (Input.GetKey (KeyCode.D)){
			rigidbody.MovePosition (rigidbody.position + new Vector3 (transform.right.x * 3.18f, 0f, transform.right.z * 3.18f));
		}
	}

	
	private void InputColorChange(){
		if(Input.GetKeyDown (KeyCode.C))
			ChangeColorTo(new Vector3 (Random.Range (0f, 1f), Random.Range (0f, 1f), Random.Range (0f, 1f)));
	}
	
	[RPC] void ChangeColorTo(Vector3 color){
		renderer.material.color = new Color(color.x,color.y,color.z,1f);
		if(networkView.isMine)
			networkView.RPC("ChangeColorTo",RPCMode.OthersBuffered,color);
	}


//	void OnMouseDrag() {
//		//if(networkView.isMine){
//			Vector3 mouse = Input.mousePosition;
//			mouse.z = Vector3.Distance (Camera.main.transform.position, transform.position);
//			Debug.Log (mouse.z);
//	
//			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
//			RaycastHit hit;
//			if (Physics.Raycast (ray, out hit)) {
//				mouse = ray.GetPoint (hit.distance);
//					}
//			//mouse.z = Camera.main.transform.position.y - transform.position.y;//Vector3.Distance(Camera.main.transform.position, transform.position.y);
//			/* Get the mouse world corrdinates from where you click on screen */
//			//mouse = Camera.main.ScreenToWorldPoint (mouse);
//			//Debug.Log ("screen: " + Input.mousePosition + "world: " + mouse);
//	
//			/* Snap to XZ plane at board surface */
//			mouse.y = 5.0f;
//			mouse.x = Mathf.Round (mouse.x / 3.18f) * 3.18f;
//			mouse.z = Mathf.Round (mouse.z / 3.18f) * 3.18f;
//			if (mouse.x < -8f * 3.18f)
//				mouse.x = -8f * 3.18f;
//			if (mouse.z > 8f * 3.18f)
//				mouse.z = 8f * 3.18f;
//			if(mouse.x>0f)
//				mouse.x = 0;
//			if (mouse.z < 0f)
//				mouse.z = 0;
//			//Debug.Log (mouse);
//			/* Move this gameobject to where the mouse is */
//			transform.position = mouse;//new Vector3( mouse.x, mouse.y, transform.position.z);
//		//}
//	}
}
