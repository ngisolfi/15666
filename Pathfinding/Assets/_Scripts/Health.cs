using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	public Texture2D healthFull;
	public Texture2D healthEmpty;
	private int healthLevel;
	private int fullHealth = 100;
	private float timeDamaged;

	// Use this for initialization
	void Start () {
		timeDamaged = -10e7f;
		healthLevel = fullHealth;
	}
	
	void OnGUI() {
		Camera cam = Camera.current;
		if( healthLevel!=fullHealth){
			Vector3 pos = cam.WorldToScreenPoint(transform.position) - new Vector3(30,-20,0);
			Vector2 size = new Vector2(60,10);
			
	//		if(pos.z < 1)
				
		
			// draw the background:
			GUI.BeginGroup (new Rect (pos.x, Screen.height - pos.y, size.x, size.y));
			GUI.DrawTexture (new Rect (0,0, size.x, size.y),healthEmpty,ScaleMode.StretchToFill);
			
			// draw the filled-in part:
			GUI.BeginGroup (new Rect (0, 0, size.x * healthRatio, size.y));
			GUI.DrawTexture (new Rect (0,0, size.x, size.y),healthFull,ScaleMode.StretchToFill);
			GUI.EndGroup ();
			
			GUI.EndGroup ();
		}
	}
	
	public void takeDamage(int strength){
		if(strength > 0){
			healthLevel -= strength;
			if(healthLevel < 0){
				healthLevel = 0;
			}
			timeDamaged = Time.time;
		}
	}
	
	public float timeSinceLastHit
	{
		get
		{
			return Time.time-timeDamaged;
		}
	}
	
	public float healthRatio
	{
		get
		{
			return (float) healthLevel/(float) fullHealth;
		}
	}
	
	public int health
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
