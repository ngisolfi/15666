/*
 * Bounding Box Gizmo
 * Example of a script that draws a simple box around an object with Unity's Gizmos system
 */

using UnityEngine;
using System.Collections;

public class BoxGizmo : MonoBehaviour
{
	void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(this.transform.position, this.transform.lossyScale);
	}
}
