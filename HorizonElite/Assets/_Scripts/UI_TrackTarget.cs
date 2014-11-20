using UnityEngine;
using System.Collections;

public class UI_TrackTarget : MonoBehaviour {
	public Transform target;
	public float leadDistance;
	private Camera UIcamera;

	// Use this for initialization
	void Start () {
//		foreach(Transform sibling in transform.parent){
//			if(sibling.name == "Camera"){
//				UIcamera = sibling.camera;
//			}
//		}

		UIcamera = GameObject.FindGameObjectWithTag ("UICam").camera;

	}
	
	// Update is called once per frame
	void Update () {
		if(Camera.main){
			Vector3 screenPos = Camera.main.WorldToScreenPoint(target.forward*leadDistance + target.position);
			Vector3 uiPos = new Vector3((2f*screenPos.x-Screen.width)/Screen.height,2f*screenPos.y/Screen.height - 1f,0f);
			transform.position = uiPos*UIcamera.orthographicSize;
		}
	}
}
