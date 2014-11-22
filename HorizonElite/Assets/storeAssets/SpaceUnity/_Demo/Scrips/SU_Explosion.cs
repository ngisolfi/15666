using UnityEngine;
using System.Collections;

public class SU_Explosion : MonoBehaviour {
	public float destroyAfterSeconds = 8.0f;

	private float timer = 0;

	void Awake() {

		timer = Time.time;

	}


	void Update(){
				if (Time.time - timer > destroyAfterSeconds) {
						// Destroy gameobject after delay
					if(networkView.isMine)
						Network.Destroy(gameObject); //(gameObject, destroyAfterSeconds);

				}
		}
}
