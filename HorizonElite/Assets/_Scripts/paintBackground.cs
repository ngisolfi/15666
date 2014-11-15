using UnityEngine;
using System.Collections;

public class paintBackground : MonoBehaviour {

	public Camera parentCamera;
	private Vector3 _originalPosition;

	void Start () {

	//	if(parentCamera==null)
	//		parentCamera = Camera.main;							

	}
	
	void Update () {		
		if (parentCamera != null) {
		
			transform.rotation = parentCamera.transform.rotation;
			camera.fieldOfView = parentCamera.fieldOfView;

		}
	}
}
