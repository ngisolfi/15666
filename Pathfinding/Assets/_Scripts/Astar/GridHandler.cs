using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridHandler : MonoBehaviour {

	protected int gridX;
	protected int gridY;
	protected int[] gridOffset;
	protected bool[,] grid;
	protected Node[,] gridNodes;
	public float resolution;
	public float safetyRadius;
	public float clearance;
	protected GameObject[] chasers;
	protected GameObject[] obstacles;
//	[HideInInspector]
//	public float timeBuffer = 0f;
	
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

		Vector3 groundHalfExtents = ground.transform.lossyScale*0.5f;

		gridNodes = new Node[gridX,gridY];

		for(int i=0;i<gridNodes.GetLength(0);i++)
			for(int j=0;j<gridNodes.GetLength(1);j++)
				gridNodes[i,j] = new Node(i,j, new Vector3(((float)i)*resolution - groundHalfExtents.x,0.05f,((float)j)*resolution - groundHalfExtents.z));
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

	public List<Vector3> computePath(Vector3 startPoint, Vector3 endPoint, int maxPlanSteps){
//		if (openNodes.isEmpty() || bestNode.h == 0f)
//			return;
		NodeQueue openNodes = new NodeQueue();

		int[] gridStart = this.worldToGrid(startPoint);
		int[] gridGoal = this.worldToGrid(endPoint);
		
		for(int i=0;i<this.gridNodes.GetLength(0);i++){
			for(int j=0;j<this.gridNodes.GetLength(1);j++){
				this.gridNodes[i,j].reset();
				//				if(handler.occupied(this.gridNodes[i,j].x,this.gridNodes[i,j].y))
				//					this.gridNodes[i,j].occupied = true;
				this.gridNodes[i,j].setGoal(gridGoal);
			}
		}

		gridNodes[gridStart[0],gridStart[1]].g = 0f;
		openNodes.Push(gridNodes[gridStart[0],gridStart[1]]);
		Node bestNode = openNodes.Peek();
		
//		handler.timeBuffer = plannerCount*Time.deltaTime;
		
		float[,] edgeUnitCosts = new float[3,3]{{1.414f,1f,1.414f},{1f,0f,1f},{1.414f,1f,1.414f}};
		Node n;
		float cost;
		//		while(openNodes.Peek() != startNode){
		for(int count=0;count<maxPlanSteps;count++){
			n = openNodes.Pop();
			n.state = 2;
			if (n.h < bestNode.h){
				bestNode = n;
				if (bestNode.h == 0f)
					break;
			}
			
			for(int i=n.x-1;i<=n.x+1;i++){
				for(int j=n.y-1;j<=n.y+1;j++){
					if (n.checkLoc(i,j))
						continue;
					if (i < 0 || j < 0 || i >= this.GetLength(0) || j >= this.GetLength(1))
						continue;
					cost = gridNodes[n.x,n.y].g + edgeUnitCosts[i-n.x+1,j-n.y+1];
					if (this.occupied(i,j))
						cost += 10e7f;
					if(cost < gridNodes[i,j].g){
						gridNodes[i,j].g = cost;
						gridNodes[i,j].xPrev = n.x;
						gridNodes[i,j].yPrev = n.y;
						gridNodes[i,j].state = 1;
						openNodes.Push(gridNodes[i,j]);
					}
				}
			}
			if (openNodes.isEmpty()){
				//				path.Clear();
				//				path.Add(this.GetNode(transform.position));
				//				Debug.Log("Path not found");
				//				return;
				break;
			}
		}
		
		Node cNode = bestNode;
		Node startNode = gridNodes[gridStart[0],gridStart[1]];
		List<Vector3> path = new List<Vector3>();
//		path.Add(cNode.wPos);
		while(cNode != startNode){
			//			point.x = ((float) cNode.xPrev)*handler.resolution-groundHalfExtents.x;
			//			point.z = ((float) cNode.yPrev)*handler.resolution-groundHalfExtents.z;
			//			path.Add(cNode.wPos);
			path.Add(cNode.wPos);
			cNode = gridNodes[cNode.xPrev,cNode.yPrev];
		}
		path.Reverse();
		Shortcut(ref path);
		drawLine(path);
		return path;
	}

	void drawLine(List<Vector3> path){
		LineRenderer line = GetComponent<LineRenderer>();
		if(line){
			line.SetVertexCount(path.Count);
			for(int i=0;i<path.Count;i++){
				line.SetPosition(i,path[i]);
			}
		}
	}

	public void Shortcut(ref List<Vector3> path){
		if(path.Count > 2){
			int index = 0;
			while(index < path.Count-2){
				if(this.checkObstructed(path[index],path[index+2])){
					index++;
				}else{
					path.RemoveAt(index+1);
				}
			}
		}
	}
}

public class Node {
	private int[] gridPos;
	private int[] goalPos;
	public Vector3 wPos;
	public int xPrev;
	public int yPrev;
	private float leastcost;
	private float heuristic;
	public int state;
	private bool hSet;
	
	public Node(int i, int j, Vector3 worldPos){
		this.gridPos = new int[2]{i,j};
		this.wPos = worldPos;
		this.reset();
	}
	
	public void reset(float threshold = 0f){
		if (this.g >= threshold){
			this.xPrev = 0;
			this.yPrev = 0;
			this.leastcost = 10e7f;
			this.heuristic = 0f;
			this.hSet = false;
			this.state = 0;
		}
	}
	
	public void setGoal(int goalX, int goalY){
		this.setGoal(new int[2]{goalX,goalY});
	}
	
	public void setGoal(int[] goal){
		this.goalPos = goal;
		this.hSet = false;
	}
	
	public float h{
		get{
			if(!this.hSet){
				float dx = (float) (this.goalPos[0]-this.gridPos[0]);
				float dy = (float) (this.goalPos[1]-this.gridPos[1]);
				this.heuristic =  Mathf.Sqrt(dx*dx+dy*dy);
				this.hSet = true;
			}
			return this.heuristic;
		}
		set{
			this.heuristic = value;
			this.hSet = true;
		}
	}
	
	public float g {
		get{
			return this.leastcost;
		}
		set{
			this.leastcost = value;
		}
	}
	
	public float f{
		get{
			return g+h;
		}
	}
	
	public int x{
		get{
			return gridPos[0];
		}
	}
	
	public int y{
		get{
			return gridPos[1];
		}
	}
	
	public bool checkLoc(int row, int col){
		return row == gridPos[0] && col == gridPos[1];
	}
}

public class NodeQueue
{
	private List <Node> data;
	private bool ascending;
	
	public NodeQueue(bool isAscending = true)
	{
		this.data = new List <Node>();
		this.ascending = isAscending;
	}
	
	public Node Peek(){
		return data[0];
	}
	
	public Node Pop(){
		Node d = data[0];
		data.RemoveAt(0);
		return d;
	}
	
	public void Push(Node d){
		this.data.Remove(d);
		int insert = this.data.Count;
		bool add;
		for(int i=0;i<this.data.Count;i++){
			if(ascending)
				add = d.f < this.data[i].f;
			else
				add = d.f > this.data[i].f;
			if ( add ){
				insert = i;
				break;
			}
		}
		this.data.Insert(insert,d);
	}
	
	public bool isEmpty(){
		return this.data.Count == 0;
	}
	
	public void Clear(){
		this.data.Clear();
	}
}

