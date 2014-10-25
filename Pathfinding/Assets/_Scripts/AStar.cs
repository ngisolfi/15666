using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class AStar : MonoBehaviour {
	
	public class Node : IEquatable<Node> {//, IComparable<Node>{
		
		public int gridx, gridz;
		public float g, h;
		public List<Node> succ;
		//		public List<float> cost2succ;
		public float succCost;
		public Node parent;
		public Node(int newgx, int newgz, float newg, float newh, List<Node> newsucc) {
			
			gridx = newgx;
			gridz = newgz;
			g=newg;
			h=newh;
			succ = new List<Node>();
			//cost2succ = new List<float>();
			if(newsucc!=null){
				for(int i=0;i<newsucc.Count;i++){
					succ.Add (newsucc[i]);
					//					cost2succ.Add (cost[i]);
				}
			}
			parent = null;
			
		}
		
		public bool Equals(Node other){
			if (other == null) {
				return false;
			}
			return(this.gridx == other.gridx && this.gridz == other.gridz);
		}
		
		//		public int CompareTo(Node other){
		//			if((this.g+this.h) > (other.g+other.h)){
		//				return -1;
		//			} else if((this.g + this.h)==(other.g+other.h)){
		//				return 0;
		//			} else {
		//				return 1;
		//			}
		//		}
	}
	
	
	//public List<Node> PATH;
	public int ID;
	public GameObject player;
	public GameObject gridobject;
	public List<Vector2> plan;
	public float epsilon;

	private bool completePathFound;
	private int counter;
	private Node suboptimalGoal;

	private Node[,] graph;
	private List<Node> OPEN;
	private List<Node> CLOSED;
	make_grid getGrid;
	private int[,] occGrid;
	public float[,] hMap;
	private float[,] gMap;
	//	private int[,] pMap;
	private int width, height;
	
	private Vector2 initGrid;
	private Vector3 initWorld;
	private Vector2 goalGrid;
	private Vector3 goalWorld;
	//private List<int[]> path;
	
	private int frameNum;
	void Start () {
		
		//Declare the Gameobject that we will be following
		//player = GameObject.Find ("player");
		
		
		//Get the grid to perform A*
		getGrid = gridobject.GetComponentInChildren<make_grid> ();
		occGrid = getGrid.grid;
		
		//Declare width and height of grid
		width = (int)((getGrid.endx - getGrid.startx) / getGrid.cellsize);
		height = (int)((getGrid.endz - getGrid.startz) / getGrid.cellsize);
		
		//		for(int i=0; i<width;i++){
		//			for(int j=0;j<height;j++){
		//				getGrid.pMap[i,j]=1;
		//			}
		//		}
		OPEN = new List<Node> ();
		CLOSED = new List<Node> ();
		frameNum = 0;
		
	}
	
	// Update is called once per frame
	void Update () {
		//	plan.Clear ();
		frameNum++;
		//Debug.Log (frameNum);
		if (frameNum%10==ID){
			//Declare initial positions
			//while(true){
			
			
			initWorld = this.transform.position;
			initGrid = new Vector2(
				(initWorld.x - getGrid.startx)/getGrid.cellsize,
				(initWorld.z - getGrid.startz)/getGrid.cellsize
				);
			
			//Declare goal positions
			goalWorld = player.transform.position;
			goalGrid = new Vector2(
				(goalWorld.x - getGrid.startx)/getGrid.cellsize,
				(goalWorld.z - getGrid.startz)/getGrid.cellsize
				);
			
			//Make the grid containing heuristic costs
			MK_Maps ();
			
			occGrid = getGrid.grid;
			gMap [(int)initGrid.x, (int)initGrid.y] = 0;
			
			//Make the initial graph node
			Node start = new Node (	(int)initGrid.x,
			                       (int)initGrid.y,
			                       gMap [(int)initGrid.x, (int)initGrid.y],
			                       hMap [(int)initGrid.x, (int)initGrid.y],
			                       null //GT_Successors ((int)initGrid.x, (int)initGrid.y)
			                       );
			
			//Initialize Priority Queue with only the start position in List
	//		Debug.Log (getGrid.grid[start.gridx,start.gridz]);
			OPEN.Add(start);
			getGrid.oMap[start.gridx,start.gridz]=1;
			
			ComputePath ();
			if(!completePathFound){

				float bestDistance = 100000000;
				for(int i=0; i<width; i++){
					for(int j=0;j<height; j++){
						if(occGrid[ i, j ] == 0 && getGrid.cMap[i,j]==1){
							float distance = Mathf.Abs(i - goalGrid.x)+Mathf.Abs (j - goalGrid.y);
							if(distance<bestDistance){
								bestDistance=distance;
								suboptimalGoal= new Node (i,j,0.0f,0.0f,null);
							}
						}
					}
				}
				Debug.Log (suboptimalGoal);
						PublishSolution(suboptimalGoal);
			} else{
			//Debug.Log ("open size: " + OPEN.Count + "closed size: " + CLOSED.Count);
			PublishSolution(null);
			}
			OPEN.Clear();
			CLOSED.Clear();
			
		}
	}
	
	void ComputePath() {
		plan.Clear ();
		counter = 0;
		while( counter<750 && !OPEN.Contains(new Node((int)goalGrid.x,(int)goalGrid.y,0.0f,0.0f,null))){
			//while(counter<20){
			
			if(OPEN.Count>0){
				OPEN.Sort( delegate( Node x, Node y ){return (x.g+x.h).CompareTo(y.g+y.h);});
				//Remove the lowest cost sample, which should be put to front of OPEN
				
				Node currState = OPEN[0];

				CLOSED.Add(OPEN[0]);

				getGrid.cMap[OPEN[0].gridx,OPEN[0].gridz]=1;
				OPEN.RemoveAt(0);
				ExpandState(currState);
				//counter++;
			}else{
				if(OPEN.Contains (new Node((int)goalGrid.x,(int)goalGrid.y,0.0f,0.0f,null))){
					completePathFound=true;
				}else{
					completePathFound=false;
				}
				break;
			}
		}
		//	Debug.Log (counter);
	}
	
	void PublishSolution( Node incoming) {
		Node temp;

		if((incoming==null && CLOSED.Count>0) || incoming!=null && CLOSED.Count>1){
			if(incoming==null){
				temp = CLOSED[CLOSED.Count-1];
			}else{
				Predicate<Node> nodeFinder = delegate(Node p){return p.gridx==incoming.gridx && p.gridz==incoming.gridz;};
				int idx = CLOSED.FindIndex(nodeFinder);//delegate( Node x, Node y ){return (x.g+x.h).CompareTo(y.g+y.h);}
				temp = CLOSED[idx];
			}
			while (temp.parent != null) {
				getGrid.pMap[temp.gridx,temp.gridz] = 1;
				//			Debug.Log ("cell: " +getGrid.pMap[temp.gridx,temp.gridz]);
				temp = temp.parent;
				plan.Add(new Vector2((temp.gridx+0.5f)*getGrid.cellsize+getGrid.startx, (temp.gridz+0.5f)*getGrid.cellsize+getGrid.startz));
			}
		}
	}
	
	void ExpandState( Node state ){
		List<Node> successors = GT_Successors(state.gridx, state.gridz);
		
		for(int i=0;i<successors.Count;i++){
			if(!CLOSED.Contains (successors[i])){
				if(successors[i].g > state.g + successors[i].succCost){//cMap[state.gridx,state.gridz]){
					counter++;
					successors[i].g = state.g + successors[i].succCost;//cMap[state.gridx,state.gridz];
					gMap[successors[i].gridx,successors[i].gridz] = state.g + successors[i].succCost;//hMap[state.gridx,state.gridz];
					successors[i].parent = state;
					OPEN.Add (successors[i]);
					getGrid.oMap[successors[i].gridx,successors[i].gridz]=1;
				}
			}
		}
	}
	
	void MK_Maps() {
		hMap = new float[width,height];
		gMap = new float[width, height];
		//	pMap = new int[width, height];
		for (int i=0; i<width; i++) {
			for (int j=0; j<height; j++) {
				hMap [i, j] = 	epsilon*(0.5f*(float)Mathf.Max (
					Mathf.Abs (i - goalGrid.x),
					Mathf.Abs (j - goalGrid.y)) +
				                        0.5f*(float)Mathf.Min (
					Mathf.Abs (i - goalGrid.x),
					Mathf.Abs (j - goalGrid.y)));
				//+getGrid.grid [i, j] * 1000.0f);
				gMap [i, j] = 100000000000000;
				//getGrid.pMap [i,j] = 0;
			}
		}
	}
	
	List<Node> GT_Successors( int i, int j ) {
		float cost;
		List<Node> successors = new List<Node>();
		//		List<float> costs = new List<float> ();
		for(int m = -1; m<=1; m++){
			for(int n = -1; n<=1; n++){
				if( occGrid[ i+m, j+n ] == 0 ){
					//					float cost = 	(float)Mathf.Max (
					//						Mathf.Abs (i - m),
					//						Mathf.Abs (j - n)) +
					//						(float).4 * Mathf.Min (
					//							Mathf.Abs (i - m),
					//							Mathf.Abs (j - n)
					//							);
					if(m==0 && n==0){
						cost = 0.0f;
					}else if(m==0 || n==0){
						cost = 1.0f;
					}else{
						cost = 1.42f;
					}
					successors.Add ( new Node(i+m, j+n, gMap[i+m,j+n], hMap[i+m,j+n], null) );
					successors[successors.Count-1].succCost=cost;//costs.Add (cost);
				}
			}
		}
		return successors;
	}
}
