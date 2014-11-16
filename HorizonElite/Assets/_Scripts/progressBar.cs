using UnityEngine;
using System.Collections;

public class progressBar : MonoBehaviour {

	public float oreLevel;
	public MeshRenderer fullIcon;
	public Color start,end;
	private int cutoffLevel=1;


void Start(){
		fullIcon.enabled = false;
	}
void FixedUpdate () { 
		//renderer.material.SetFloat ("_Cutoff", cutoffLevel);
		renderer.material.SetFloat ("_Cutoff",Mathf.InverseLerp(0, Screen.width, Input.mousePosition.x)); 
		//renderer.material.color = Color.Lerp (Color.blue, Color.green, oreLevel/256f);
		renderer.material.color = Color.Lerp (start, end, Mathf.InverseLerp(Screen.width, 0,  Input.mousePosition.x));


		if(Mathf.InverseLerp(0, Screen.width, Input.mousePosition.x)>0){
			fullIcon.enabled = false;
		}else{
			fullIcon.enabled = true;
		}


}

	public void changeOre( float amount ){
		if (oreLevel <= 256f) {
			oreLevel += amount;
			cutoffLevel = Mathf.RoundToInt(oreLevel);
			Debug.Log (fullIcon);
			fullIcon.enabled = false;
		} else {
			fullIcon.enabled = true;
		}
	}

}