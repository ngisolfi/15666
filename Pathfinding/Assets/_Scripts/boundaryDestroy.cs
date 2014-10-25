using UnityEngine;
using System.Collections;

public class boundaryDestroy : MonoBehaviour
{
	void OnTriggerExit (Collider other) 
	{
		Destroy(other.gameObject);
	}
}