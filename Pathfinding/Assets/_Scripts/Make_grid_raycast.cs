using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Make_grid_raycast : MonoBehaviour {
	
	public class point : IEquatable<point>{
		
		public int x;
		public int z;
		
		public point(int newx, int newz) {
			x = newx;
			z = newz;
			
		}
		public bool Equals(point other){
			if (other == null) {
				return false;
			}
			return(this.x == other.x && this.z == other.z);
		}
	}


	public GameObject player;
	public LayerMask layerMask;
	public int startx, startz, endx, endz;
	//public float cellsize;
	public int[,] grid;
	public int[,] pMap;
	public int[,] oMap;
	public int[,] cMap;
	
	//private int width, height;
	private GameObject[] obstacles;
	
	private float ominx, omaxx, ominz, omaxz;
	private int gminx, gmaxx, gminz, gmaxz;
	private Vector3 obcenter;
	
	private Color linecolor = new Color (0f, 1f, 0f, 1f);
	private Material linematerial;



	//public float startx, startz, endx, endz;
	public int width, height;
	public float cellsize;
//	public int[,] grid;
	private List<point> cells_to_check;
	private List<point> cells_to_add;
	
	// Use this for initialization
	void Start () {
		width = (int)((endx - startx) / cellsize);
		height = (int)((endz - startz) / cellsize);
		grid = new int[width, height];
		//Debug.Log ("width: " + width + "height: " + height);
		cells_to_check = new List<point> ();
		cells_to_add = new List<point> ();
		
		grid = new int[width, height];
		pMap = new int[width, height];
		oMap = new int[width, height];
		cMap = new int[width, height];

		for(int i=0; i<width; i++){
			for(int j=0; j<height; j++){
				grid[i,j] = 0;//no obstacle 
				pMap[i,j] = 0;
				oMap[i,j] = 0;
				cMap[i,j] = 0;
			}
		}

		for (int i=0; i<width; i++) {
			for (int j=0; j<height; j++) {
				cells_to_check.Add (new point (i, j));
			}
		}
		//Debug.Log (cells_to_check.Count);
		
	}
	
	// Update is called once per frame
	void Update () {
//		Debug.Log (width + height);
		width = (int)((endx-startx)/cellsize);
		height = (int)((endz - startz) / cellsize);
		grid = new int[width, height];

		//	for (int i=0; i<width; i++) {
		//					for (int j=0; j<height; j++) {
		//							grid [i, j] = 1;
		//					}
		//			}
		int temp = cells_to_check.Count;
		//	Debug.Log (temp);
		for (int i=0; i<temp; i++) {
			
			Vector3 curr_cell_center = new Vector3(startz,0.0f,startz) + new Vector3((cells_to_check[i].x+0.5f)*cellsize, 25, (cells_to_check[i].z+0.5f)*cellsize); 
			if(Physics.Raycast (curr_cell_center, -Vector3.up,layerMask)) {
				
				grid[cells_to_check[i].x,cells_to_check[i].z]=1;
				if(cells_to_check[i].x>2 && cells_to_check[i].z>2 && cells_to_check[i].x<width-2 && cells_to_check[i].z<height-2){
					
					for(int j=cells_to_check[i].x-1;j<=cells_to_check[i].x+1;j++){
						for(int k=cells_to_check[i].z-1;k<=cells_to_check[i].z+1;k++){
							//if(j>0 && k>0 && j<width && k<height){
							int side_count = 0;
							for(int l=-1;l<=1;l++){
								for(int m=-1;m<=1;m++){
									if(grid[j+l,k+m]==0){
										side_count++;
									}
								}
							}
							if(side_count<9){
								if(!cells_to_add.Contains(new point(j,k))){
									cells_to_add.Add(new point (j,k));
								}
								
							}
							//}
						}
					}
				}
			} else {
				//Debug.Log (cells_to_check[i].x + " " + cells_to_check[i].z);
				grid[cells_to_check[i].x,cells_to_check[i].z]=0;
			}
			
		}
		//		cells_to_check = cells_to_add;
		cells_to_check.Clear();
		for (int j=0; j<cells_to_add.Count;j++){
			cells_to_check.Add (cells_to_add[j]);
		}
		cells_to_add.Clear();
	}
	
}

