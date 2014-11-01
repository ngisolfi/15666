using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridHandler : MonoBehaviour {

	protected int gridX;
	protected int gridY;
	protected int[] gridOffset;
	protected bool[,] grid;
//	protected bool[,] dynamicMask;
//	[HideInInspector]
//	public bool maskUpdated;
//	int[] gridSize;
//	protected List<int> occupiedCells;
	public float resolution;
	public float safetyRadius;
	public float clearance;
	protected GameObject[] chasers;
	protected GameObject[] obstacles;
	[HideInInspector]
	public float timeBuffer = 0f;
	
	void Awake() {
		buildGrid();
	}
	
	// Use this for initialization
	void Start () {
		obstacles = GameObject.FindGameObjectsWithTag("Cylinder Obstacle");
		checkStaticObstacles();
//		chasers = GameObject.FindGameObjectsWithTag("Chaser");
	}
	
	// Update is called once per frame
	void Update () {
//		maskUpdated = false;
//		if(Time.frameCount % 1 == 0)
		checkStaticObstacles();
	}
	
	void buildGrid () {
		GameObject ground = GameObject.Find("Logic/Boundary");
		int groundX = (int)(ground.transform.lossyScale.x/resolution);
		int groundY = (int)(ground.transform.lossyScale.z/resolution);
//		gridSize = new int[2]{(int)(ground.transform.lossyScale.x/resolution),(int)(ground.transform.lossyScale.z/resolution)};
//		gridX = Mathf.Min (gridX,groundX);
//		gridY = Mathf.Min (gridY,groundY);
		gridX = groundX;
		gridY = groundY;
		gridOffset = new int[2]{groundX-gridX,groundY-gridY};
		grid = new bool[gridX,gridY];
//		dynamicMask = new bool[this.GetLength(0),this.GetLength(1)];
	}
	
//	void resetMask(){
////		dynamicMask = new bool[this.GetLength(0),this.GetLength(1)];
//		for(int i=0;i<this.dynamicMask.GetLength(0);i++){
//			for(int j=0;j<this.dynamicMask.GetLength(1);j++){
//				dynamicMask[i,j] = false;
//			}
//		}
//	}
	
	void resetGrid(){
//		occupiedCells.Clear();
		for(int i=0;i<this.grid.GetLength(0);i++){
			for(int j=0;j<this.grid.GetLength(1);j++){
				grid[i,j] = false;
			}
		}
//		grid = new bool[this.GetLength(0),this.GetLength(1)];
	}
	
//	int sub2ind(int row, int col){
//		return row + col*gridSize[1];
//	}
	
	public bool occupied(int row, int col){
//		return grid[row,col] || dynamicMask[row,col];
		return grid[row,col];
//		return occupiedCells.Contains(sub2ind(row,col));
	}
	
	public bool occupied(int[] pos){
		return this.occupied(pos[0],pos[1]);
	}
	
	public bool occupied(Vector3 pos){
		return this.occupied(this.worldToGrid(pos));
	}
	
	public int GetLength(int dimension){
//		return gridSize[dimension];
		return grid.GetLength(dimension);
	}
	
	public int[] worldToGrid(Vector3 worldPos){
		int gridHalfX = this.GetLength(0)/2;
		int gridHalfY = this.GetLength(0)/2;
		
		return new int[2]{((int)(worldPos.x/resolution)) + gridHalfX,((int)(worldPos.z/resolution)) + gridHalfY};
	}
	
	public Vector3 gridToWorld(int row, int col){
		int gridHalfX = this.GetLength(0)/2;
		int gridHalfY = this.GetLength(0)/2;
		
		return new Vector3(((float) (row-gridHalfX))*resolution,0.05f,((float) (col-gridHalfY))*resolution);
	}
	
	public Vector3 gridToWorld(int[] gridPos){
		return gridToWorld(gridPos[0],gridPos[1]);
	}
	
	void setOccupied(Vector3 worldPos, bool occupied = true){
		int[] gridPos = worldToGrid(worldPos);
		setOccupied(gridPos[0],gridPos[1]);
	}
	
	void setOccupied(int row, int col, bool occupied = true){
		grid[row,col] = occupied;
//		occupiedCells.Add(sub2ind(row,col));
	}
	
	void checkStaticObstacles(){
		resetGrid();
		int gridHalfX = this.GetLength(0)/2;
		int gridHalfY = this.GetLength(1)/2;
	
//		GameObject[] boxObstacles = GameObject.FindGameObjectsWithTag("Cylinder Obstacle");
		foreach(GameObject o in this.obstacles){
			float radius = Mathf.Max(o.collider.bounds.extents.x,o.collider.bounds.extents.z) + safetyRadius;
			Vector3 start = o.transform.position - radius*Vector3.one;
//			if(start.y > clearance)
//				continue;
			int startX = Mathf.CeilToInt(start.x/resolution) + gridHalfX;
			int startY = Mathf.CeilToInt(start.z/resolution) + gridHalfY;
			Vector3 end = o.transform.position + radius*Vector3.one;
			int endX = Mathf.FloorToInt(end.x/resolution) + gridHalfX;
			int endY = Mathf.FloorToInt(end.z/resolution) + gridHalfY;
			for(int i=startX;i<=endX;i++){
				for(int j=startY;j<=endY;j++){
					Vector3 dwPos = gridToWorld(i,j) - o.transform.position;
					if((dwPos.x*dwPos.x + dwPos.z*dwPos.z) < radius*radius)
						setOccupied(i,j);
//					setOccupied(i,j);
				}
			}
		}
	}
	
	public bool checkObstructed(int[] point1, int[] point2){
		if(occupied(point1) || occupied(point2))
			return true;
		
		int dx = point2[0] - point1[0];
		int dy = point2[1] - point1[1];
		int xstep = 1,ystep = 1,ddx,ddy;
		int error, errorprev;
		int x = point1[0],y = point1[1];
		
		if(dy < 0){
			ystep = -1;
			dy = -dy;
		}
		if(dx < 0){
			xstep = -1;
			dx = -dx;
		}
		ddy = 2*dy;
		ddx = 2*dx;
		
		if(ddx >= ddy){
			error = dx;
			errorprev = error;
			for(int i=0;i<dx;i++){
				x += xstep;
				error += ddy;
				if (error > ddx){  // increment y if AFTER the middle ( > ) 
					y += ystep; 
					error -= ddx; 
					// three cases (octant == right->right-top for directions below): 
					if (error + errorprev < ddx)  // bottom square also 
//						POINT (y-ystep, x);
						if(occupied(x,y-ystep))
							return true;
					else if (error + errorprev > ddx)  // left square also 
//						POINT (y, x-xstep); 
						if(occupied(x-xstep,y))
							return true;
					else{  // corner: bottom and left squares also 
//						POINT (y-ystep, x); 
//						POINT (y, x-xstep); 
						if(occupied(x-xstep,y) || occupied(x,y-ystep))
							return true;
					} 
				} 
				if(occupied(x,y))
					return true;
				errorprev = error;
			} 
		}else{  // the same as above 
			errorprev = error = dy; 
			for (int i=0 ; i < dy ; i++){ 
				y += ystep;
				error += ddx;
				if (error > ddy){ 
					x += xstep; 
					error -= ddy; 
					if (error + errorprev < ddy) 
//						POINT (y, x-xstep); 
						if(occupied(x-xstep,y))
							return true;
					else if (error + errorprev > ddy) 
//						POINT (y-ystep, x);
						if(occupied(x,y-ystep))
							return true;
					else{ 
//						POINT (y, x-xstep); 
//						POINT (y-ystep, x); 
						if(occupied(x,y-ystep) || occupied(x-xstep,y))
							return true;
					} 
				} 
//				POINT (y, x);
				if(occupied(x,y))
					return true;
				errorprev = error; 
			} 
		} 
		return false;
		// assert ((y == y2) && (x == x2));  // the last point (y2,x2) has to be the same with the last point of the algorithm 
	}
	
	public bool checkObstructed(Vector3 point1, Vector3 point2){
		return checkObstructed(worldToGrid(point1),worldToGrid(point2));
	}
}
