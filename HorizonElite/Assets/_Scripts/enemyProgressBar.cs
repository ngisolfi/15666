using UnityEngine;
using System.Collections;

public class enemyProgressBar : progressBar {
	
	public string enemyShipName;
	
	void Update () { 
		if(!container){
			container = GameObject.Find(enemyShipName);
		}
	}
}