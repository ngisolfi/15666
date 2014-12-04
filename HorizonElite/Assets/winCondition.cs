using UnityEngine;
using System.Collections;

public class winCondition : MonoBehaviour {

	public GameObject enemySun;
	public GameObject sunExplosion;
	public GameObject winDecal;
	public GameObject loseDecal;

	private bool iWin = false;
	public bool iLose = false;
	private bool reported = false;

	[HideInInspector]
	public float gameOverTimer = 0.0f;

	private GameObject explosion;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (iWin || iLose)
		{
			gameOverTimer -= Time.deltaTime;
			if (gameOverTimer <= 0)
				Application.Quit();

			// Emit the explosion for 5 - 2 = 3.0 seconds
			if (gameOverTimer < 2.0f)
				explosion.particleEmitter.emit = false;
		}

	}

	void OnTriggerEnter( Collider other ){
		if (other.tag == "battleShip" && other.networkView.isMine && !reported){
			iWin = true;
			youLose();
			gameOverTimer = 5.0f;

			// Explode the enemy's sun
			Vector3 sun_pos = Vector3.zero;
			if (enemySun != null)
			{
				sun_pos = enemySun.transform.position;
				Destroy(enemySun);
			}
			explosion = (GameObject)Network.Instantiate(sunExplosion, sun_pos, Quaternion.identity, 0); 
		}
	}

	void youLose(){
		if(Network.isServer){
			if(GameObject.Find ("alienWinCondition")!=null)
			{
				GameObject.Find ("alienWinCondition").GetComponent<winCondition>().iLose = true;
				GameObject.Find ("alienWinCondition").GetComponent<winCondition>().gameOverTimer = 5.0f;
			}
			GameObject.Find("p1UI").GetComponent<activateWinLoseLogos>().activateWinLogo();
			if(GameObject.Find ("p2UI")!=null)
				GameObject.Find("p2UI").GetComponent<activateWinLoseLogos>().activateLoseLogo();
		}else{
			if(GameObject.Find ("humanWinCondition")!=null)
			{
				GameObject.Find ("humanWinCondition").GetComponent<winCondition>().iLose = true;
				GameObject.Find ("alienWinCondition").GetComponent<winCondition>().gameOverTimer = 5.0f;
			}
			GameObject.Find("p2UI").GetComponent<activateWinLoseLogos>().activateLoseLogo();
			if(GameObject.Find ("p1UI")!=null)
				GameObject.Find("p1UI").GetComponent<activateWinLoseLogos>().activateWinLogo();
		}
	}

//	void OnGUI(){
//		if(iWin){
//			//print win stuff
//			GUI.Label (new Rect(100,100,100,200),"YOU WIN!");
//		}
//		if(iLose){
//			//print lose stuff
//			GUI.Label (new Rect(100,100,100,200),"YOU LOSE!");
//		}
//	}
}