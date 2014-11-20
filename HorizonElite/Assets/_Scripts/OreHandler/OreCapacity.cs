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

	protected virtual int amountAddable(string element, int amount){
		return Mathf.Min(levels[element] + amount,levelCap);
	}

	public void depositOre(string element, int amount){
		levels[element] = amountAddable(element, amount);
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
			foreach(int value in levels.Values){
				total += value;
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
