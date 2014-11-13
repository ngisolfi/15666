using UnityEngine;
using System.Collections;

public class wrapTexture : MonoBehaviour {

	public Texture2D surface;
	// Use this for initialization
	void Start () {
	

		Material runtimeMaterial = new Material(Shader.Find("Bumped Diffuse"));
		runtimeMaterial.mainTexture = surface;
		//assuming sphere is a GameObject or Monobehavior
		renderer.material = runtimeMaterial;
	}

}
