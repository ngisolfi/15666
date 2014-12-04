using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyIndicators : MonoBehaviour {
	public GameObject playerShip;
	public GameObject indicator_prefab;
	private List<GameObject> _indicators;

	// Use this for initialization
	void Start () {
		_indicators = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		List<Transform> visible = playerShip.GetComponent<Sensor>().enemies;
		Transform target;
		GameObject indicator;
		for(int i=_indicators.Count-1;i>=0;i--){
//		foreach(GameObject indicator in _indicators){
			target = _indicators[i].GetComponent<UI_TrackTarget>().target;
			if(visible.Contains(target)){
				visible.Remove(target);
			}else{
				indicator = _indicators[i];
				_indicators.RemoveAt(i);
				Destroy(indicator);
			}
		}

		foreach(Transform v in visible){
			indicator = Instantiate(indicator_prefab) as GameObject;
			indicator.GetComponent<UI_TrackTarget>().target = v;
			_indicators.Add(indicator);
		}
	}
}
