using UnityEngine;
using System.Collections;

public class UI_ClipToLeft : MonoBehaviour {
	public int offset;
	private Camera UIcamera;
	
	// Use this for initialization
	void Start () {
		foreach(Transform sibling in transform.parent){
			if(sibling.name == "uiCamera"){
				UIcamera = sibling.camera;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 uiPos = transform.position;
		float extent = transform.Find("BoundingBox").lossyScale.x*0.5f;
		uiPos.x = ((2f*(float)offset-Screen.width)/(float)Screen.height)*UIcamera.orthographicSize+extent;
		transform.position = uiPos;
	}
}
