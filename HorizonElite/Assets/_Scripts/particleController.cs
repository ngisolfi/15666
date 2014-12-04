using UnityEngine;
using System.Collections;

public class particleController : MonoBehaviour {

	public int max_particles = 100;
	public float max_velocity = 2500.0f;
	private GameObject particles;
	private Camera cam;

	// Use this for initialization
	void Start () {
		cam = Camera.main;
		particles = transform.Find ("Sparks").gameObject;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		// Make the ship's trail renderer end width a function of velocity
		float vel = this.rigidbody.velocity.magnitude;

		if (!gameObject.GetComponent<StateHandler> ().playerControlled)
		{
			particles.SetActive(false);
			gameObject.networkView.RPC("disableTrailRenderer", RPCMode.All);
			return;
		}
		else
		{
			gameObject.GetComponentInChildren<TrailRenderer> ().endWidth = 2900.0f * vel / max_velocity + 100.0f;
		}

		if (!networkView.isMine)
			return;

		// Find the camera, place the particle system 
		// directly in front of the lookat vector
		particles.transform.position = cam.transform.position + cam.transform.forward * 150.0f;
		
		// Update the particles on the ship's thruster
		ParticleSystem thruster = transform.Find ("Thruster").gameObject.particleSystem;
		if (Network.isServer)
			thruster.startSize = vel / 150.0f + 2.0f;
		else
			thruster.startSize = vel / 300.0f + 0.8f;
		Color slow = new Color (1.0f, 0.2f, 0.1f);
		Color fast = new Color (0.2f, 0.4f, 1.0f);
		thruster.startColor = Color.Lerp (slow, fast, vel / max_velocity);
	
		// Should we provide purple space clouds?
		if (vel > 0.3f * max_velocity) 
			particles.SetActive(true);
		else
			particles.SetActive(false);

		// Max velocity is roughly 1300
		int n_particles = Mathf.RoundToInt (max_particles * vel / max_velocity);

		particles.particleEmitter.minEmission = n_particles;
		particles.particleEmitter.maxEmission = n_particles;

		particles.particleEmitter.localVelocity = new Vector3(0.0f, 0.0f, vel / 1.25f);
		particles.particleEmitter.rndVelocity = new Vector3(vel / 3.0f, vel / 3.0f, 0.0f);
	}

	[RPC]
	public void disableTrailRenderer()
	{
		if (!gameObject.GetComponent<StateHandler>().playerControlled)
			gameObject.GetComponentInChildren<TrailRenderer> ().enabled = false;
	}
}
