using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathFollowerState : ApproachState {
	protected List<Vector3> _targetList = new List<Vector3>();
//	public float distance_threshold;
//	public float avoidance_distance = 1000f;
	public float progression_threshold = 5000f;

	public override void execute ()
	{
		if((_target-_controller.transform.position).sqrMagnitude < distance_threshold*distance_threshold && _targetList.Count > 1){
//			index = Mathf.Min(_targetList.Count-1,index+1);
			_targetList.RemoveAt(0);
			_target = _targetList[0];
		}
		avoidObstacles();
		LineRenderer line = GetComponent<LineRenderer>();
		if(line){
			line.SetVertexCount(_targetList.Count + 1);
			line.SetPosition(0,_controller.transform.position);
			for(int i=0;i<_targetList.Count;i++){
				line.SetPosition(i+1,_targetList[i]);
			}
		}
		base.execute();
	}

	private void avoidObstacles (){
		clearWaypoints();

		RaycastHit hit;
		Vector3 forward = _target-_controller.transform.position;
		for(int i=1;i<100;i++){
			if(!Physics.Raycast(_controller.transform.position, forward, out hit, forward.magnitude,~LayerMask.GetMask("Player")))
				break;

			Vector3 tangent = hit.point-hit.transform.position;
			Vector3 proj = Vector3.Project(tangent,forward);
			tangent = tangent - Vector3.Project(tangent,forward);
			float tmag = tangent.magnitude;
			if(tmag <= 0f){
				tangent = Quaternion.AngleAxis(30f,_controller.transform.right)*(hit.point-hit.transform.position);
				tangent = tangent - Vector3.Project(tangent,forward);
				tmag = tangent.magnitude;
			}
			tangent /= tmag;
			_targetList.Insert(0,hit.collider.bounds.extents.x*1.5f*tangent + hit.transform.position);
			_target = _targetList[0];
			forward = _target-_controller.transform.position;
		}

//		RaycastHit hit;
//		Vector3 forward = _target-_controller.transform.position;
//		while(Physics.Raycast(_controller.transform.position, forward, out hit, forward.magnitude,~LayerMask.GetMask("Player"))){
//			Vector3 closestToCenter = Vector3.Project(hit.transform.position - _controller.transform.position, forward) + _controller.transform.position;
//			float distToCenter = (closestToCenter-hit.transform.position).magnitude;
//			Vector3 closestToVertical = (_controller.transform.up - Vector3.Project(_controller.transform.up,forward)).normalized;
//			_targetList.Insert(0,closestToCenter + closestToVertical*(hit.collider.bounds.extents.x*1.5f-distToCenter));
//			_target = _targetList[0];
//			forward = _target-_controller.transform.position;
//		}
	}

	protected void setTarget(Vector3 target){
		_targetList.Clear();
		_targetList.Add(target);
		_target = target;
//		avoidObstacles();
	}

	protected void clearWaypoints(){
		if(_targetList.Count==0)
			return;
		_target = _targetList[_targetList.Count-1];
		_targetList.Clear();
		_targetList.Add(_target);
	}
}
