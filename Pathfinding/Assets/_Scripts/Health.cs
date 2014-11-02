using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	public Texture2D healthFull;
	public Texture2D healthEmpty;
	private float healthLevel;

	// Use this for initialization
	void Start () {
		healthLevel = 1f;
	}
	
	void OnGUI() {
		Vector3 pos = Camera.main.WorldToScreenPoint(transform.position) - new Vector3(30,-20,0);
		Vector2 size = new Vector2(60,10);
		
//		if(pos.z < 1)
			
	
		// draw the background:
		GUI.BeginGroup (new Rect (pos.x, Screen.height - pos.y, size.x, size.y));
		GUI.DrawTexture (new Rect (0,0, size.x, size.y),healthEmpty,ScaleMode.StretchToFill);
		
		// draw the filled-in part:
		GUI.BeginGroup (new Rect (0, 0, size.x * healthLevel, size.y));
		GUI.DrawTexture (new Rect (0,0, size.x, size.y),healthFull,ScaleMode.StretchToFill);
		GUI.EndGroup ();
		
		GUI.EndGroup ();
	}
	
	public void incrementHealth(float val){
		healthLevel += val;
		if (healthLevel > 1f){
			healthLevel = 1f;
		}else if(healthLevel < 0f){
			healthLevel = 0f;
		}
	}
	
	public float health
	{
		get
		{
			return healthLevel;
		}
		
		set
		{
			healthLevel = value;
		}
	}
}
