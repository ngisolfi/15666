using UnityEngine;
using System.Collections;

public class UI_ClipToBottom : MonoBehaviour {
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
		uiPos.y = ((2f*(float)offset-Screen.height)/(float)Screen.height)*UIcamera.orthographicSize+renderer.bounds.extents.y;
		transform.position = uiPos;
	}
}
