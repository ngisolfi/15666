using UnityEngine;
using System.Collections;

public class progressBar : MonoBehaviour {

//	public float oreLevel=0f;
	public MeshRenderer fullIcon;
	public Color start,end;
//	private int cutoffLevel=0;
	public string element;
	public GameObject container;


	void Start(){
		fullIcon.enabled = false;
	}

	void FixedUpdate () {
		OreCapacity collector = container.GetComponent<OreCapacity>();
		float capacity_fraction = 0f;
		if(collector){
			capacity_fraction = collector.elementFraction(element);
		}



		renderer.material.SetFloat ("_Cutoff", Mathf.Max((int)(capacity_fraction*256f),1));
//		renderer.material.SetFloat ("_Cutoff",Mathf.InverseLerp(0, Screen.width, Input.mousePosition.x)); 
		renderer.material.color = Color.Lerp (start, end, capacity_fraction);
//		renderer.material.color = Color.Lerp (start, end, Mathf.InverseLerp(Screen.width, 0,  Input.mousePosition.x));


//		if(Mathf.InverseLerp(0, Screen.width, Input.mousePosition.x)>0){
//			fullIcon.enabled = false;
//		}else{
//			fullIcon.enabled = true;
//		}
	}

//	public void changeOre( int amount ){
//		if (oreLevel <= 256f) {
//			oreLevel = (float)amount;
//			cutoffLevel = amount;
////			Debug.Log (fullIcon);
//			fullIcon.enabled = false;
//		} else {
//			fullIcon.enabled = true;
//		}
//	}

}