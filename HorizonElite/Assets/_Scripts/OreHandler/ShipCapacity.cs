using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipCapacity : OreCapacity {
	public int totalCap = 256;
	public GameObject homeShip;
	public int drainSpeed;
	public float shipProximityThreshold;
	[HideInInspector]
	public List<string> fill_order;
	public float tractor_beam_distance = 420f;

	public override void Awake(){
		base.Awake();
		fill_order = new List<string>();
	}

	void FixedUpdate(){
		if(homeShip){
			float d = (transform.position-homeShip.transform.position).sqrMagnitude;
			//			Debug.Log(Mathf.Sqrt(d));
			if(d < shipProximityThreshold*shipProximityThreshold){
				dumpLoad();
			}
		}
	
		if (percentFull () > 2)
			Debug.Log("percent: " + percentFull().ToString());
		if (percentFull() >= 99)
			// We have a full payload
			// Play the payload dropoff instructions audio clip if it has not been played before
			GameObject.Find("GameInstance").GetComponent<networkManager>().playMiningEndAudio();
	}

	protected override int amountAddable(string element, int amount){
		int other = 0;
		foreach(KeyValuePair<string,int> kvp in levels){
			if(kvp.Key.CompareTo(element) != 0){
				other += kvp.Value;
			}
		}
		int depositedAmount = Mathf.Min(base.amountAddable(element,amount) + other,totalCap) - other;
		if(depositedAmount > 0){
			if(!fill_order.Contains(element)){
				fill_order.Add(element);
			}
		}

		return depositedAmount;
	}

	// Remove amount from element and return remainder
	public int drainElement(string element, int amount)
	{
		int start_level = levels[element];
		levels[element] = Mathf.Max(levels[element] - amount,0);
		if(levels[element] == 0)
			fill_order.Remove(element);
		return Mathf.Max(amount - start_level + levels[element],0);
	}

	
	void dumpLoad()
	{
		int amountToDrain = drainSpeed;
		int startAmount;
		string element;
		for(int i=fill_order.Count-1;i>=0;i--){
			startAmount = amountToDrain;
			element = fill_order[i];
			amountToDrain = drainElement(element,amountToDrain);
			homeShip.GetComponent<OreCapacity>().depositOre(element,startAmount-amountToDrain);
			if(amountToDrain == 0)
				break;
		}
	}
	
	public void destroyLoad()
	{
		int amountToDrain = 256;
		int startAmount;
		string element;
		for(int i=fill_order.Count-1;i>=0;i--){
			startAmount = amountToDrain;
			element = fill_order[i];
			amountToDrain = drainElement(element,amountToDrain);
			if(amountToDrain == 0)
				break;
		}
	}

	public override float elementFraction(string element){
		if(element.CompareTo("ALL") == 0){
			int total = 0;
			foreach(int value in levels.Values){
				total += value;
			}
			return (float)total/(float)totalCap;
		}else{
			return (float)levels[element]/(float)totalCap;
		}
	}

	
	public override int percentFull()
	{
		int total = 0;
		foreach(int value in levels.Values){
			total += value;
		}
		return total*100/totalCap;
	}
}
