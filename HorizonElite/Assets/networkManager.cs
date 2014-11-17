using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class networkManager : MonoBehaviour {

	private const string typeName = "Horizon Elite";
	private const string gameName = "Universe 1";

	private HostData[] hostList;

	public GameObject alien_ship;
	public GameObject human_ship;
	private GameObject ship;
	public GameObject cam;
	public GameObject backgroundCam;
	public GameObject titleCam;
	public GameObject crosshair;
	public GameObject payload;
	
	private GameObject titleCamHandle;

	void Start(){

		titleCamHandle = Instantiate (titleCam, Vector3.zero, Quaternion.identity) as GameObject;

	}

	void OnServerInitialized(){
	}

	void OnGUI(){
	     //Game Title
          GUIStyle titleFont = new GUIStyle();
          titleFont.fontSize = 100;
          titleFont.normal.textColor = Color.white;

			if(!Network.isClient && !Network.isServer){
				GUI.Label ( new Rect(100,100,100,50),"Horizon Elite",titleFont);
				if(GUI.Button (new Rect (100,300,100,50),"Start Server"))
					StartServer();
				if(GUI.Button (new Rect(100,500,100,50),"Refresh Hosts"))
					RefreshHostList();
				if(hostList!=null){
					for(int i=0;i<hostList.Length;i++){
						if(GUI.Button (new Rect(400,100+(110*i),300,100),hostList[i].gameName))
							JoinServer(hostList[i]);
					}
				}
			}

	}

	private void RefreshHostList(){
		MasterServer.RequestHostList (typeName);
	}

	void OnMasterServerEvent(MasterServerEvent msEvent){
		if (msEvent == MasterServerEvent.HostListReceived)
			hostList = MasterServer.PollHostList ();
	}
	
	private void JoinServer(HostData hostData){
		Network.Connect (hostData);
		
	}
	
	void OnConnectedToServer( ){
		Spawn ();
	}

	private void StartServer(){
		
		Network.InitializeServer (4, 25000, !Network.HavePublicAddress ());
		MasterServer.RegisterHost(typeName,gameName);

		Spawn ();
	}
	
	private void Spawn(){
		Destroy (titleCamHandle);
		if (Network.isServer) {
					
						ship = human_ship;
						Debug.Log ("server spawning ship" + ship.ToString ());
				} else {
						ship = alien_ship;
						Debug.Log ("server spawning ship" + ship.ToString ());
				}
		// Determine a spawn location and instantiate a new ship of the player's type
		Vector3 spawn_location = GetSpawnLocation();
		Quaternion spawn_direction = GetSpawnDirection ();
		GameObject spawned = (GameObject)Network.Instantiate (ship, 
		                                                     spawn_location, 
		                                                     spawn_direction, 
		                                                      0);
		// Turn off the ship's tractor beam at the beginning of the game
		spawned.transform.FindChild("tractorBeam").gameObject.SetActive(false);

		GameObject cam_spawned = (GameObject)Network.Instantiate (cam,
		             		        							  spawn_location,
		                    		                              spawn_direction,
		                                                          0);

		GameObject backGround_camSpawn = (GameObject)Network.Instantiate (backgroundCam,
		                                                                  Vector3.zero,
		                                                                  spawn_direction,
		                                                                  0);

		cam_spawned.GetComponent<cameraFollow> ().target = spawned.transform;
		backGround_camSpawn.GetComponent<paintBackground> ().parentCamera = cam_spawned.camera;


		/*GameObject my_crosshair = (GameObject)Network.Instantiate (crosshair,
		                                                          Vector3.zero,
		                                                          Quaternion.identity,
		                                                          0);

		my_crosshair.GetComponent<UI_TrackTarget> ().target = spawned.transform;
		*/

		/*GameObject payload_spawned = (GameObject)Network.Instantiate (payload,
		                                                             Vector3.zero,
		                                                             Quaternion.identity,
		                                                             0);*/


		// If the spawn was successful, set the camera to point to the spawned object
		/*if (spawned)
		{
			if(Camera.main)
				Camera.main.enabled = false;
			Transform spawned_camera = transform.Find("Camera");
			if (spawned_camera)
			{
				spawned_camera.camera.GetComponent<SmoothFollowCSharp>().target = spawned.transform;
				spawned_camera.camera.enabled = true;
			}
			
			active_ships.Add(spawned);
		}*/
	}

	private Vector3 GetSpawnLocation()
	{
		return Vector3.zero;
	}

	private Quaternion GetSpawnDirection()
	{
		return Quaternion.identity;
	}

}
