using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Planner : MonoBehaviour {

	public Transform target;
	public int maxPlanSteps;
	protected Vector3 groundHalfExtents;
	[HideInInspector]
	public PathHandler path;
	protected GridHandler handler;
	[HideInInspector]
	public Node[,] gridNodes;
	protected int[] gridStart;
	protected int[] gridGoal;
	protected NodeQueue openNodes;
	protected Node bestNode;
	protected int frameOffset;
	[HideInInspector]
	public int plannerCount;
	public bool drawGrid;
	[HideInInspector]
	public bool running;

	// Use this for initialization
	void Start () {
		if(target == null){
			GameObject player = GameObject.FindGameObjectWithTag("Player");
			if(player)
				target = player.transform;
		}
		setUniqueOffset();
		
		path = new PathHandler();
		handler = GameObject.Find("Grid").GetComponent<GridHandler>();
		GameObject ground = GameObject.Find("Logic/Boundary");
		groundHalfExtents = ground.transform.lossyScale*0.5f;
		gridNodes = new Node[handler.GetLength(0),handler.GetLength(1)];
		openNodes = new NodeQueue();
		for(int i=0;i<gridNodes.GetLength(0);i++)
			for(int j=0;j<gridNodes.GetLength(1);j++)
				gridNodes[i,j] = new Node(i,j, new Vector3(((float)i)*handler.resolution - groundHalfExtents.x,0.05f,((float)j)*handler.resolution - groundHalfExtents.z));

		path.Add(this.GetNode(transform.position));
		resetGrid(transform.position, target.position);
	}
	
	// Update is called once per frame
	void Update () {
		if(target == null){
			GameObject player = GameObject.FindGameObjectWithTag("Player");
			if(player)
				target = player.transform;
		}
//		testPath();
		if((Time.frameCount + this.frameOffset) % this.plannerCount == 0){
			if ((handler.gridToWorld(gridGoal) - target.position).sqrMagnitude > 2f*handler.resolution*handler.resolution || (handler.gridToWorld(gridStart) - transform.position).sqrMagnitude > 2f*handler.resolution*handler.resolution){
//				updateGoal(target.position);
				resetGrid(transform.position,target.position);
			}
			
			updatePath();
//			drawLine();
		}
	}
	
	void setUniqueOffset(){
		GameObject[] planObjects = GameObject.FindGameObjectsWithTag("Enemy");
		this.plannerCount = planObjects.GetLength(0);
		bool taken;
		for(int i=0;i<this.plannerCount;i++){
			taken = false;
			foreach(GameObject o in planObjects){
				if(i == o.GetComponent<Planner>().frameOffset){
					taken = true;
					break;
				}
			}
			if(!taken){
				this.frameOffset = i;
				break;
			}
		}
	}
	
	void drawLine(){
		LineRenderer line = GetComponent<LineRenderer>();
		line.SetVertexCount(path.Count);
		for(int i=0;i<path.Count;i++){
			line.SetPosition(i,path.GetPoint(i));
		}
	}
	
//	void updateStart(Vector3 start){
//		gridStart = handler.worldToGrid(start);
//		
//		for(int i=0;i<this.gridNodes.GetLength(0);i++){
//			for(int j=0;j<this.gridNodes.GetLength(1);j++){
//				this.gridNodes[i,j].reset();
//			}
//		}
//		
//		gridNodes[gridStart[0],gridStart[1]].g = 0f;
//		openNodes.Clear();
//		openNodes.Push(gridNodes[gridStart[0],gridStart[1]]);
//		bestNode = openNodes.Peek();
//	}
//	
//	void updateGoal(Vector3 goal){
//		gridGoal = handler.worldToGrid(goal);
//		
//		for(int i=0;i<this.gridNodes.GetLength(0);i++){
//			for(int j=0;j<this.gridNodes.GetLength(1);j++){
//				if(handler.occupied(this.gridNodes[i,j].x,this.gridNodes[i,j].y))
//					this.gridNodes[i,j].occupied = true;
//				this.gridNodes[i,j].setGoal(gridGoal);
//			}
//		}
//		
//		if(gridNodes[gridGoal[0],gridGoal[1]].state == 2){
//			bestNode = gridNodes[gridGoal[0],gridGoal[1]];
//		}
//	}
	
	void resetGrid(Vector3 start, Vector3 goal){
		gridStart = handler.worldToGrid(start);
		gridGoal = handler.worldToGrid(goal);
		
		for(int i=0;i<this.gridNodes.GetLength(0);i++){
			for(int j=0;j<this.gridNodes.GetLength(1);j++){
				this.gridNodes[i,j].reset();
//				if(handler.occupied(this.gridNodes[i,j].x,this.gridNodes[i,j].y))
//					this.gridNodes[i,j].occupied = true;
				this.gridNodes[i,j].setGoal(gridGoal);
			}
		}
				
		gridNodes[gridStart[0],gridStart[1]].g = 0f;
		openNodes.Clear();
		openNodes.Push(gridNodes[gridStart[0],gridStart[1]]);
		bestNode = openNodes.Peek();
		path.Clear();
		running = false;
	}
	
	void testPath(){
		for(int i=0;i<this.path.Count;i++){
			if(handler.occupied(handler.worldToGrid(this.path.GetPoint(i)))){
				resetGrid(transform.position, target.position);
			}
		}
	}
	
	void updatePath(){
		if (openNodes.isEmpty() || bestNode.h == 0f)
			return;
		
		path.Clear();
		
		handler.timeBuffer = plannerCount*Time.deltaTime;
		
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
					if (i < 0 || j < 0 || i >= handler.GetLength(0) || j >= handler.GetLength(1))
						continue;
					cost = gridNodes[n.x,n.y].g + edgeUnitCosts[i-n.x+1,j-n.y+1];
					if (handler.occupied(i,j))
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
//		path.Add(cNode.wPos);
		while(cNode != startNode){
//			point.x = ((float) cNode.xPrev)*handler.resolution-groundHalfExtents.x;
//			point.z = ((float) cNode.yPrev)*handler.resolution-groundHalfExtents.z;
//			path.Add(cNode.wPos);
			path.Add(cNode);
			cNode = gridNodes[cNode.xPrev,cNode.yPrev];
		}
		path.Reverse();
		Shortcut();
	}
	
	public Node GetNode(int[] pos){
		return gridNodes[pos[0],pos[1]];
	}
	
	public Node GetNode(Vector3 pos){
		return this.GetNode(handler.worldToGrid(pos));
	}
	
	public void Shortcut(){
		if(path.Count > 2){
			int index = 0;
			Vector3 point1, point3;
			while(index < path.Count-2){
				point1 = this.path.GetPoint(index);
				point3 = this.path.GetPoint(index+2);
				if(handler.checkObstructed(this.path.GetPoint(index),this.path.GetPoint(index+2))){
					index++;
				}else{
					path.Cut(index+1);
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

public class PathHandler{
	private List<Node> nodes;
	private List<Vector3> path;
	private bool changed;
	
	public PathHandler(){
		nodes = new List<Node>();
		path = new List<Vector3>();
		changed = true;
	}

	public void Add(Node item){
		nodes.Add(item);
		changed = true;
	}
	
	public void Clear(){
		nodes.Clear();
		path.Clear();
		changed = true;
	}
	
	public int Count{
		get{
			if(changed)
				GeneratePath();
			return path.Count;
		}
	}
	
	public bool isEmpty(){
		return nodes.Count == 0;
	}
	
	private void GeneratePath(){
		foreach(Node n in nodes){
			path.Add(n.wPos);
		}
		changed = false;
	}
	
	public void Cut(int index){
		if(index < this.Count){
			path.RemoveAt(index);
		}
	}
	
	public Vector3 GetPoint(int index){
		if(index < this.Count){
			return path[index];
		}else{
			return Vector3.zero;
		}
	}
	
	public void Reverse(){
		nodes.Reverse();
		path.Reverse();
	}
}