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
	public GameObject orbiter;
	private GameObject ship;
	public GameObject cam;
	public GameObject backgroundCam;
	public GameObject titleCam;
//	public GameObject crosshair_gameobject;
	public GameObject payload;
	public GameObject UI;
	private GameObject titleCamHandle;
	private GameObject titleBackgroundHandle;
	
	void Start(){


//		titleCamHandle = Instantiate (titleCam, Vector3.zero, Quaternion.identity)as GameObject;
//		titleBackgroundHandle = GameObject.FindGameObjectWithTag ("BKGCam");
//		titleBackgroundHandle.GetComponent<paintBackground> ().parentCamera = GameObject.Find ("spectator").camera;
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

		//Get rid of orbiting title camera
		Destroy (GameObject.Find("MenuCamera"));
		Destroy (GameObject.Find ("Backdrop/backgroundCamera"));
		Network.RemoveRPCsInGroup(0);
		Network.RemoveRPCsInGroup(1);


		GameObject homeOrbiter;
		//Different meshes for alien/human
		if (Network.isServer) {
			ship = human_ship;
			homeOrbiter = Network.Instantiate (orbiter, Vector3.zero, Quaternion.identity, 0) as GameObject;
			homeOrbiter.name = "humanOrbiter";
			GameObject homePlanet = GameObject.Find("Environment/RedSolarSystem/redOrbitingPlanets/redO2/redP2");
			if(homePlanet)
				homeOrbiter.GetComponent<planetOrbit>().planet = homePlanet.transform;
//			homeOrbiter.transform.localPosition = new Vector3(28046.57f,0f,0f);
//			homeOrbiter.transform.localRotation = Quaternion.Euler(293.0699f,90f,270f);
//			player.transform.localScale = Vector3.one*7500f;
		} else {
			ship = alien_ship;
			homeOrbiter = Network.Instantiate (orbiter, Vector3.zero, Quaternion.identity, 0) as GameObject;
			homeOrbiter.name = "alienOrbiter";
			GameObject homePlanet = GameObject.Find("Environment/BlueSolarSystem/blueOrbitingPlanets/blueO2/blueP2");
			if(homePlanet)
				homeOrbiter.GetComponent<planetOrbit>().planet = homePlanet.transform;
//			homeOrbiter.transform.localPosition = new Vector3(-27656.99f,0f,0f);
//			homeOrbiter.transform.localRotation = Quaternion.Euler(7.07f,90f,270f);
//			homeOrbiter.transform.localScale = Vector3.one*9500f;
		}
		
		// Determine a spawn location and instantiate a new ship of the player's type
		Transform spawnPoint = homeOrbiter.transform.Find("battleshipPrefab/spawn_point");
		Vector3 spawn_location = spawnPoint.position;
		Quaternion spawn_direction = spawnPoint.rotation;

		// player to be placed in the whorld
		GameObject player = Network.Instantiate (ship, 
		                                                     spawn_location, 
		                                                     spawn_direction, 
		                                                      0) as GameObject;

		if(Network.isServer)
			player.name = "player1";
		else
			player.name = "player2";

		// designate home planet for spawned player
		player.GetComponent<ShipCapacity>().homeShip = homeOrbiter;

		if(Network.isServer){
//			GameObject myUI = Network.Instantiate (UI,
//			                                       new Vector3(0f,0f,-1),
//			                                       Quaternion.LookRotation(Vector3.forward),
//			                                       0) as GameObject;
			GameObject myUI = Instantiate (UI, new Vector3(0f,0f,-1), Quaternion.LookRotation(Vector3.forward)) as GameObject;
			myUI.name = "p1UI";
			GameObject.Find ("p1UI/element_miningProgress/component_wheels/wheel_beryllium/be_progress").GetComponent<progressBar>().container=homeOrbiter;
			GameObject.Find ("p1UI/element_miningProgress/component_wheels/wheel_boron/b_progress").GetComponent<progressBar>().container=homeOrbiter;
			GameObject.Find ("p1UI/element_miningProgress/component_wheels/wheel_deuterium/d_progress").GetComponent<progressBar>().container=homeOrbiter;
			GameObject.Find ("p1UI/element_miningProgress/component_wheels/wheel_helium/he_progress").GetComponent<progressBar>().container=homeOrbiter;
			GameObject.Find ("p1UI/element_miningProgress/component_wheels/wheel_lithium/li_progress").GetComponent<progressBar>().container=homeOrbiter;
			GameObject.Find ("p1UI/element_miningProgress/component_wheels/wheel_tritium/t_progress").GetComponent<progressBar>().container=homeOrbiter;
			GameObject.Find ("p1UI/element_deathrayProgress/component_green/g_progress").GetComponent<progressBar>().container=homeOrbiter;
			GameObject.Find ("p1UI/element_deathrayProgress/component_red/r_progress").GetComponent<enemyProgressBar>().enemyShipName="battleShipOrbiter(Clone)";
			UI_TrackTarget tracker = myUI.transform.Find("Crosshair").gameObject.GetComponent<UI_TrackTarget>();
			if(tracker)
				tracker.target = player.transform;

		} else {
//			GameObject myUI = Network.Instantiate (UI,
//			                                       new Vector3(0f,0f,-2),
//			                                       Quaternion.LookRotation (-Vector3.forward),
//			                                       0) as GameObject;
			GameObject myUI = Instantiate (UI, new Vector3(0f,0f,-1), Quaternion.LookRotation(Vector3.forward)) as GameObject;

			myUI.name="p2UI";
			GameObject.Find ("p2UI/element_miningProgress/component_wheels/wheel_beryllium/be_progress").GetComponent<progressBar>().container=homeOrbiter;
			GameObject.Find ("p2UI/element_miningProgress/component_wheels/wheel_boron/b_progress").GetComponent<progressBar>().container=homeOrbiter;
			GameObject.Find ("p2UI/element_miningProgress/component_wheels/wheel_deuterium/d_progress").GetComponent<progressBar>().container=homeOrbiter;
			GameObject.Find ("p2UI/element_miningProgress/component_wheels/wheel_helium/he_progress").GetComponent<progressBar>().container=homeOrbiter;
			GameObject.Find ("p2UI/element_miningProgress/component_wheels/wheel_lithium/li_progress").GetComponent<progressBar>().container=homeOrbiter;
			GameObject.Find ("p2UI/element_miningProgress/component_wheels/wheel_tritium/t_progress").GetComponent<progressBar>().container=homeOrbiter;
			GameObject.Find ("p2UI/element_deathrayProgress/component_green/g_progress").GetComponent<progressBar>().container=homeOrbiter;
			GameObject.Find ("p2UI/element_deathrayProgress/component_red/r_progress").GetComponent<enemyProgressBar>().enemyShipName="battleShipOrbiter(Clone)";
			UI_TrackTarget tracker = myUI.transform.Find("Crosshair").gameObject.GetComponent<UI_TrackTarget>();
			if(tracker)
				tracker.target = player.transform;
		}

		//setting up this ship with its payload ui stuff
		if (Network.isServer)
			GameObject.Find ("p1UI/element_payloadBar/component_background").GetComponent<UI_payload> ().playerShip = player;
		else
			GameObject.Find ("p2UI/element_payloadBar/component_background").GetComponent<UI_payload> ().playerShip = player;
		// the camera which will follow the player
//		GameObject player_camera = (GameObject)Network.Instantiate (cam, spawn_location,spawn_direction,0);
		GameObject player_camera = Instantiate (cam, spawn_location,spawn_direction) as GameObject;
		player_camera.GetComponent<cameraFollow> ().target = player.transform;
		// have the backgroundCamera now follow the player's network camera
		GameObject player_BGcamera = Instantiate (backgroundCam, Vector3.zero, Quaternion.identity) as GameObject;
//		GameObject player_BGcamera = (GameObject)Network.Instantiate (backgroundCam, Vector3.zero, Quaternion.identity,0);
		player_BGcamera.GetComponent<paintBackground> ().parentCamera = player_camera.camera;



//		if (Network.isServer)
//		{
//			GameObject crosshair = (GameObject)Network.Instantiate (crosshair_gameobject,
//		    	                                                    Vector3.zero,
//		                                                            Quaternion.identity,
//		                                                            0);
//		   	crosshair.name = "p1_crosshair";
//		    crosshair.GetComponent<UI_TrackTarget>().target = GameObject.Find("player1").transform;
//		}
//		else
//		{
//			GameObject crosshair = (GameObject)Network.Instantiate (crosshair_gameobject,
//			                                                        Vector3.zero,
//			                                                        Quaternion.identity,
//			                                                        0);
//			crosshair.name = "p2_crosshair";
//			crosshair.GetComponent<UI_TrackTarget>().target = GameObject.Find("player2").transform;
//		}
		


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

//	private Vector3 GetSpawnLocation()
//	{	
//		Transform spawner;
//		if (Network.isServer)
//		{
//			spawner = GameObject.Find("Environment/RedSolarSystem/redOrbitingPlanets/redO2/battleShipOrbiter/battleshipPrefab/spawn_point").transform;
//		}
//		else
//		{
//			spawner = GameObject.Find("Environment/BlueSolarSystem/blueOrbitingPlanets/blueO2/battleShipOrbiter/battleshipPrefab/spawn_point").transform;
//		}
//		
//		return spawner.position;
//	}
//
//	private Quaternion GetSpawnDirection()
//	{
//		Transform spawner;
//		if (Network.isServer)
//		{
//			spawner = GameObject.Find("Environment/RedSolarSystem/redOrbitingPlanets/redO2/battleShipOrbiter/battleshipPrefab/spawn_point").transform;	
//		}
//		else
//		{
//			spawner = GameObject.Find("Environment/BlueSolarSystem/blueOrbitingPlanets/blueO2/battleShipOrbiter/battleshipPrefab/spawn_point").transform;
//		}
//		
//		return spawner.rotation;
//	}
}
