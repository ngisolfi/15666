using UnityEngine;
using System.Collections;

public class networkRename : MonoBehaviour {

	[RPC]
	public void rename(string new_name)
	{
		gameObject.name = new_name;
	}
}
