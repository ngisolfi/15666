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
		foreach(GameObject indicator in _indicators){
			target = indicator.GetComponent<UI_TrackTarget>().target;
			if(visible.Contains(target)){
				visible.Remove(target);
			}else{
				_indicators.Remove(indicator);
				Destroy(indicator);
			}
		}

		foreach(Transform v in visible){
			GameObject indicator = Instantiate(indicator_prefab) as GameObject;
			indicator.GetComponent<UI_TrackTarget>().target = v;
			_indicators.Add(indicator);
		}
	}
}
