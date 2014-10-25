using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class make_grid : MonoBehaviour {
	
	public GameObject player;
	
	public int startx, startz, endx, endz;
	public float cellsize;
	public int[,] grid;
	public int[,] pMap;
	public int[,] oMap;
	public int[,] cMap;
	
	private int width, height;
	private GameObject[] obstacles;
	
	private float ominx, omaxx, ominz, omaxz;
	private int gminx, gmaxx, gminz, gmaxz;
	private Vector3 obcenter;
	
	private Color linecolor = new Color (0f, 1f, 0f, 1f);
	private Material linematerial;
	
	// Use this for initialization
	void Start () {
		
		width = (int)((endx-startx)/cellsize);
		height = (int)((endz - startz) / cellsize);
		grid = new int[width, height];
		pMap = new int[width, height];
		oMap = new int[width, height];
		cMap = new int[width, height];
		
		GameObject[] Boxstacles = GameObject.FindGameObjectsWithTag ("Box Obstacle");
		GameObject[] Cylstacles = GameObject.FindGameObjectsWithTag ("Cylinder Obstacle");
		
		obstacles = new GameObject[Boxstacles.Length + Cylstacles.Length];
		Boxstacles.CopyTo (obstacles, 0);
		Cylstacles.CopyTo (obstacles, Boxstacles.Length);
		
		for(int i=0; i<width; i++){
			for(int j=0; j<height; j++){
				grid[i,j] = 0;//no obstacle 
				pMap[i,j] = 0;
				oMap[i,j] = 0;
				cMap[i,j] = 0;
			}
		}
		
		for(int i=0; i<obstacles.Length; i++){
			obcenter = obstacles[i].transform.position;
			
			if((obcenter.y - obstacles[i].transform.lossyScale.y/2.0f) < (player.transform.position.y + player.transform.lossyScale.y/2.0f)){
				ominx = obstacles[i].renderer.bounds.center.x - obstacles[i].renderer.bounds.extents.x;//obcenter.x - obstacles[i].transform.lossyScale.x/2.0f;
				omaxx = obstacles[i].renderer.bounds.max.x;//obcenter.x + obstacles[i].transform.lossyScale.x/2.0f;
				ominz = obstacles[i].renderer.bounds.min.z;//obcenter.z - obstacles[i].transform.lossyScale.z/2.0f;
				omaxz = obstacles[i].renderer.bounds.max.z;//obcenter.z + obstacles[i].transform.lossyScale.z/2.0f;
				
				gminx = (int)((ominx - startx)/cellsize);
				gmaxx = (int)((omaxx - startx)/cellsize);
				gminz = (int)((ominz - startz)/cellsize);
				gmaxz = (int)((omaxz - startz)/cellsize);
				
				for (int j=gminx; j<=gmaxx; j++){
					for(int k=gminz; k<=gmaxz; k++){
						grid[j,k] = 1;//obstacle
					}
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		width = (int)((endx-startx)/cellsize);
		height = (int)((endz - startz) / cellsize);
		grid = new int[width, height];
		
		GameObject[] Boxstacles = GameObject.FindGameObjectsWithTag ("Box Obstacle");
		GameObject[] Cylstacles = GameObject.FindGameObjectsWithTag ("Cylinder Obstacle");
		
		obstacles = new GameObject[Boxstacles.Length + Cylstacles.Length];
		Boxstacles.CopyTo (obstacles, 0);
		Cylstacles.CopyTo (obstacles, Boxstacles.Length);
		
		for(int i=0; i<width; i++){
			for(int j=0; j<height; j++){
				grid[i,j] = 0;//no obstacle 
				//pMap[i,j]=0;
			}
		}

//		for (int i=0; i<obstacles.Length; i++) {
//						if (obstacles [i].activeSelf == false) {
//								Object.Destroy (obstacles [i]);
//						}
//				}

		
		for(int i=0; i<obstacles.Length; i++){
			obcenter = obstacles[i].transform.position;
			
			if((obcenter.y - obstacles[i].transform.lossyScale.y/2.0f) < (player.transform.position.y + player.transform.lossyScale.y/2.0f) && obstacles[i].activeSelf==true){
				ominx = obcenter.x - obstacles[i].transform.lossyScale.x/2.0f;
				omaxx = obcenter.x + obstacles[i].transform.lossyScale.x/2.0f;
				ominz = obcenter.z - obstacles[i].transform.lossyScale.z/2.0f;
				omaxz = obcenter.z + obstacles[i].transform.lossyScale.z/2.0f;
				
				gminx = (int)((ominx - startx)/cellsize);
				gmaxx = (int)((omaxx - startx)/cellsize);
				gminz = (int)((ominz - startz)/cellsize);
				gmaxz = (int)((omaxz - startz)/cellsize);
				
				for (int j=gminx; j<=gmaxx; j++){
					for(int k=gminz; k<=gmaxz; k++){
						grid[j,k] = 1;//obstacle
					}
				}
			}
		}
	}
	
}

