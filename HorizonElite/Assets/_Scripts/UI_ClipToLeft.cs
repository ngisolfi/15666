using UnityEngine;
using System.Collections;

public class UI_ClipToLeft : MonoBehaviour {
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
		uiPos.x = ((2f*(float)offset-Screen.width)/(float)Screen.height)*UIcamera.orthographicSize+renderer.bounds.extents.x;
		transform.position = uiPos;
	}
}
