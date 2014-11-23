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


		//Different meshes for alien/human
		if (Network.isServer) {
			ship = human_ship;
		} else {
			ship = alien_ship;
		}
		
		// Determine a spawn location and instantiate a new ship of the player's type
		Vector3 spawn_location = GetSpawnLocation();
		Quaternion spawn_direction = GetSpawnDirection ();

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
		if(Network.isServer){
			player.GetComponent<ShipCapacity>().homeShip = GameObject.Find("Environment/RedSolarSystem/redOrbitingPlanets/redO2/battleShipOrbiter");
		} else {
			player.GetComponent<ShipCapacity>().homeShip = GameObject.Find ("Environment/BlueSolarSystem/blueOrbitingPlanets/blueO2/battleShipOrbiter");
		}


		if(Network.isServer){
			GameObject myUI = Network.Instantiate (UI,
			                                       new Vector3(0f,0f,-1),
			                                       Quaternion.LookRotation(Vector3.forward),
			                                       0) as GameObject;
			myUI.name = "p1UI";
			GameObject.Find ("p1UI/element_miningProgress/component_wheels/wheel_beryllium/be_progress").GetComponent<progressBar>().container=GameObject.Find("Environment/RedSolarSystem/redOrbitingPlanets/redO2/battleShipOrbiter");
			GameObject.Find ("p1UI/element_miningProgress/component_wheels/wheel_boron/b_progress").GetComponent<progressBar>().container=GameObject.Find("Environment/RedSolarSystem/redOrbitingPlanets/redO2/battleShipOrbiter");
			GameObject.Find ("p1UI/element_miningProgress/component_wheels/wheel_deuterium/d_progress").GetComponent<progressBar>().container=GameObject.Find("Environment/RedSolarSystem/redOrbitingPlanets/redO2/battleShipOrbiter");
			GameObject.Find ("p1UI/element_miningProgress/component_wheels/wheel_helium/he_progress").GetComponent<progressBar>().container=GameObject.Find("Environment/RedSolarSystem/redOrbitingPlanets/redO2/battleShipOrbiter");
			GameObject.Find ("p1UI/element_miningProgress/component_wheels/wheel_lithium/li_progress").GetComponent<progressBar>().container=GameObject.Find("Environment/RedSolarSystem/redOrbitingPlanets/redO2/battleShipOrbiter");
			GameObject.Find ("p1UI/element_miningProgress/component_wheels/wheel_tritium/t_progress").GetComponent<progressBar>().container=GameObject.Find("Environment/RedSolarSystem/redOrbitingPlanets/redO2/battleShipOrbiter");


		} else {
			GameObject myUI = Network.Instantiate (UI,
			                                       new Vector3(0f,0f,-2),
			                                       Quaternion.LookRotation (-Vector3.forward),
			                                       0) as GameObject;
			myUI.name="p2UI";
			GameObject.Find ("p2UI/element_miningProgress/component_wheels/wheel_beryllium/be_progress").GetComponent<progressBar>().container=GameObject.Find("Environment/BlueSolarSystem/blueOrbitingPlanets/blueO2/battleShipOrbiter");
			GameObject.Find ("p2UI/element_miningProgress/component_wheels/wheel_boron/b_progress").GetComponent<progressBar>().container=GameObject.Find("Environment/BlueSolarSystem/blueOrbitingPlanets/blueO2/battleShipOrbiter");
			GameObject.Find ("p2UI/element_miningProgress/component_wheels/wheel_deuterium/d_progress").GetComponent<progressBar>().container=GameObject.Find("Environment/BlueSolarSystem/blueOrbitingPlanets/blueO2/battleShipOrbiter");
			GameObject.Find ("p2UI/element_miningProgress/component_wheels/wheel_helium/he_progress").GetComponent<progressBar>().container=GameObject.Find("Environment/BlueSolarSystem/blueOrbitingPlanets/blueO2/battleShipOrbiter");
			GameObject.Find ("p2UI/element_miningProgress/component_wheels/wheel_lithium/li_progress").GetComponent<progressBar>().container=GameObject.Find("Environment/BlueSolarSystem/blueOrbitingPlanets/blueO2/battleShipOrbiter");
			GameObject.Find ("p2UI/element_miningProgress/component_wheels/wheel_tritium/t_progress").GetComponent<progressBar>().container=GameObject.Find("Environment/BlueSolarSystem/blueOrbitingPlanets/blueO2/battleShipOrbiter");

		}

		//setting up this ship with its payload ui stuff
		if (Network.isServer)
						GameObject.Find ("p1UI/element_payloadBar/component_background").GetComponent<UI_payload> ().playerShip = player;
				else
						GameObject.Find ("p2UI/element_payloadBar/component_background").GetComponent<UI_payload> ().playerShip = player;
		// the camera which will follow the player
		GameObject player_camera = (GameObject)Network.Instantiate (cam, spawn_location,spawn_direction,0);
		player_camera.GetComponent<cameraFollow> ().target = player.transform;
		// have the backgroundCamera now follow the player's network camera
		GameObject player_BGcamera = (GameObject)Network.Instantiate (backgroundCam, Vector3.zero, Quaternion.identity,0);
		player_BGcamera.GetComponent<paintBackground> ().parentCamera = player_camera.camera;



//		GameObject my_crosshair = (GameObject)Network.Instantiate (crosshair,
//		                                                          Vector3.zero,
//		                                                          Quaternion.identity,
//		                                                          0);
//
//		my_crosshair.GetComponent<UI_TrackTarget> ().target = spawned.transform;
//

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
