using UnityEngine;
using System.Collections;

public class UI_ClipToRight : MonoBehaviour {
	public int offset;
	private Camera UIcamera;

	// Use this for initialization
	void Start () {
		foreach(Transform sibling in transform.parent){
			if(sibling.name == "Camera"){
				UIcamera = sibling.camera;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 uiPos = transform.position;
		uiPos.x = ((Screen.width-2f*(float)offset)/(float)Screen.height)*UIcamera.orthographicSize;
		transform.position = uiPos;
	}
}
