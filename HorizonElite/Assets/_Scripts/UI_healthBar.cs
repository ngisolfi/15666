using UnityEngine;
using System.Collections;

public class UI_healthBar : MonoBehaviour {
	private GameObject quad;
	public Material health_full;
	private TextMesh level_text;

	public Health player_health;

	void setQuadPos(float fill_fraction){
		float size = transform.renderer.bounds.extents.x*2f;
		float barSize = fill_fraction*size;
		float xpos = transform.position.x - renderer.bounds.extents.x;
		Vector3 quadPos = transform.position;
		quadPos.x = xpos + barSize*0.5f;
		quad.transform.position = quadPos;
		Vector3 quadScale = transform.localScale;
		quadScale.x = barSize;
		quad.transform.localScale = quadScale;
	}

	void Awake (){
		quad = GameObject.CreatePrimitive (PrimitiveType.Quad);
		
		// Set the color of the new quad based on its material
		quad.GetComponent<MeshRenderer> ().material = health_full;
		
		// Set the quad to be in the GUI rendering layer
		quad.layer = 21;
		
		// Fix this once we begin snapping to screen size
		//		quad.transform.position = new Vector3 (.6f, -0.9f, 0.0f);
//		quad.transform.localScale = new Vector3 (0.0f, 0.1f, 0.001f);
		setQuadPos(1f);
	}

	// Use this for initialization
	void Start () {
		if (Network.isServer)
			player_health = GameObject.Find ("player1").GetComponent<Health>();
		else
			player_health = GameObject.Find ("player2").GetComponent<Health>();
		
		level_text = transform.parent.Find("component_text/text_Level").GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
		if(player_health){
			setQuadPos(player_health.healthFraction());
			if (level_text)
				level_text.text = player_health.healthLevel.ToString();
		}
	}
}
