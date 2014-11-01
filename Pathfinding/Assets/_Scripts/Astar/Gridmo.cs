using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Gridmo : MonoBehaviour
{
	private bool running = false;
	private Planner[] plans;
	private GridHandler handler;

	void Start()
	{
		this.running = true;
//		GameObject[] chasers = GameObject.FindGameObjectsWithTag("Chaser");
//		this.plans = new Planner[chasers.GetLength(0)];
//		for(int i=0;i<chasers.GetLength(0);i++){
//			plans[i] = chasers[i].GetComponent<Planner>();
//		}
		this.handler = GetComponent<GridHandler>();
	
	}

	void OnDrawGizmos()
	{
		if(this.running){
//			Vector3 sTog = plan.path[plan.path.Count-1]-plan.path[0];
//			float mVal = (Mathf.Abs(sTog.x)+Mathf.Abs(sTog.y)+Mathf.Abs(sTog.z))/handler.resolution;
//			float mVal = sTog.magnitude/handler.resolution;
			for(int i=0;i<handler.GetLength(0);i++){
				for(int j=0;j<handler.GetLength(1);j++){
//					Vector3 center = handler.gridToWorld(i,j) + handler.resolution*Vector3.one*0.5f;
					Vector3 center = handler.gridToWorld(i,j);
					center.y = 0.1f;
					Vector3 size = handler.resolution*Vector3.one;
					size.y = 0f;
					if(handler.occupied(i,j)){
						Gizmos.color = new Color(0f,0f,0f,0.5f);
					}else{
						int state = 0;
						Gizmos.color = new Color(1f,0f,0f,0.5f);
//						foreach(Planner p in this.plans){
//							if(p.drawGrid && p.gridNodes[i,j].state > state){
//								state = p.gridNodes[i,j].state;
//							}
//						}
//						switch(state){
//							case 0:
//								Gizmos.color = new Color(1f,0f,0f,0.5f);
//								break;
//							case 1:
//								Gizmos.color = new Color(0f,1f,0f,0.5f);
//								break;
//							case 2:
//								Gizmos.color = new Color(0f,0f,1f,0.5f);
//								break;
//						}
					}
					Gizmos.DrawWireCube(center,size);
				}
			}
		}
	}
}
