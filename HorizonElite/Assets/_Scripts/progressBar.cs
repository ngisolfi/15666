using UnityEngine;
using System.Collections;

public class progressBar : MonoBehaviour {

	public float oreLevel;

	private int cutoffLevel=1;

void Update () { 
		renderer.material.SetFloat ("_Cutoff", cutoffLevel);
		                            //Mathf.InverseLerp(0, Screen.width, Input.mousePosition.x)); 
		renderer.material.color = Color.Lerp (Color.black, Color.green, oreLevel/256f);
}

	public void changeOre( float amount ){
		if (oreLevel <= 256f) {
			oreLevel += amount;
			cutoffLevel = Mathf.RoundToInt(oreLevel);
		}
	}

}