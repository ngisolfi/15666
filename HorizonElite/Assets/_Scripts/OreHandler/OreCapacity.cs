using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OreCapacity : MonoBehaviour {
	protected Dictionary<string,int> levels;
	public int levelCap = 256;
//	public float totalCap = 256f;

	// Use this for initialization
	public virtual void Awake () {
		levels = new Dictionary<string, int>();
		levels["BERYLLIUM"] = 0;
		levels["BORON"] = 0;
		levels["DEUTERIUM"] = 0;
		levels["HELIUM"] = 0;
		levels["LITHIUM"] = 0;
		levels["TRITIUM"] = 0;
	}
	
	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info) {
//		int beryllium = 0;
//		int boron = 0;
//		int deuterium = 0;
//		int helium = 0;
//		int lithium = 0;
//		int tritium = 0;
//		
//		if (stream.isWriting) {
//			beryllium = levels["BERYLLIUM"];
//			boron = levels["BORON"];
//			deuterium = levels["DEUTERIUM"];
//			helium = levels["HELIUM"];
//			lithium = levels["LITHIUM"];
//			tritium = levels["TRITIUM"];
//			stream.Serialize(ref beryllium);
//			stream.Serialize(ref boron);
//			stream.Serialize(ref deuterium);
//			stream.Serialize(ref helium);
//			stream.Serialize(ref lithium);
//			stream.Serialize(ref tritium);
//		} else {
//			stream.Serialize(ref beryllium);
//			stream.Serialize(ref boron);
//			stream.Serialize(ref deuterium);
//			stream.Serialize(ref helium);
//			stream.Serialize(ref lithium);
//			stream.Serialize(ref tritium);
//			levels["BERYLLIUM"] = beryllium;
//			levels["BORON"] = boron;
//			levels["DEUTERIUM"] = deuterium;
//			levels["HELIUM"] = helium;
//			levels["LITHIUM"] = lithium;
//			levels["TRITIUM"] = tritium;
//		}
	}

	protected virtual int amountAddable(string element, int amount){
		return Mathf.Min(levels[element] + amount,levelCap);
	}

	public void depositOre(string element, int amount){
		levels[element] = amountAddable(element, amount);
		
		// Is this element at maximum? If so, inform the death ray
		if (levels[element] == levelCap && gameObject.GetComponent<deathRay>() != null)
			gameObject.GetComponent<deathRay>().completeElement(element);
	}

	public int checkLevel(string element){
		if(element.ToUpper().CompareTo("ALL") == 0){
			int total = 0;
			foreach(int value in levels.Values){
				total += value;
			}
			return total;
		}else{
			return levels[element];
		}
	}
	
	public virtual float elementFraction(string element){
		if(element.CompareTo("ALL") == 0){
			int total = 0;
			//foreach(int value in levels.Values){
			foreach(KeyValuePair<string, int> kvp in levels){
				//total += value;
				total += kvp.Value;
			}
			return (float)total/(float)(levelCap*6);
		}else{
			return (float)levels[element]/(float)levelCap;
		}
	}
	
	public virtual int percentFull()
	{
		int total = 0;
		foreach(int value in levels.Values){
			total += value;
		}
		return total*100/levelCap;
	}
}
