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
	public GameObject humanWin;
	public GameObject alienWin;
	public GameObject asField;
	
	private AudioSource title_music;
	private AudioSource game_music;
	private AudioSource prologue;
	private AudioSource begin_mining;
	private AudioSource end_mining;
	private AudioSource deathray_instructions;
	private bool played_prologue_audio = false;
	private bool played_begin_mining_audio = false;
	private bool played_end_mining_audio = false;
	private bool played_death_ray_audio = false;
	
	void Start(){
		getAudioClips();
		toggleTitleMusic(true);
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
		// Turn off the background music, play the prologue clip and
		// begin playing game music
		toggleTitleMusic(false);
		toggleGameMusic(true);
		playPrologueAudio();

		//Get rid of orbiting title camera
		Destroy (GameObject.Find("MenuCamera"));
		Destroy (GameObject.Find ("Backdrop/backgroundCamera"));
		Network.RemoveRPCsInGroup(0);
		Network.RemoveRPCsInGroup(1);


		GameObject homeOrbiter;
		//Different meshes for alien/human
		if (Network.isServer) {

			//Only the server should network instantiate the asteroid field
			//Network.Instantiate (asField,Vector3.zero,Quaternion.identity,0);

			//Win condition involves blowing up blue star
			GameObject humansWin = Network.Instantiate (humanWin,new Vector3(0f,0f,-75000f),Quaternion.identity,0) as GameObject;
			humansWin.GetComponent<winCondition>().enemySun = GameObject.Find ("Environment/BlueSolarSystem/blueSun");
			humansWin.networkView.RPC ("rename", RPCMode.AllBuffered, "humanWinCondition");
			ship = human_ship;
			GameObject homePlanet = GameObject.Find("Environment/RedSolarSystem/redOrbitingPlanets/redO2/redP2");			
			homeOrbiter = Network.Instantiate (orbiter, homePlanet.transform.position, Quaternion.identity, 0) as GameObject;
			homeOrbiter.networkView.RPC ("rename", RPCMode.AllBuffered, "humanOrbiter");
			if(homePlanet)
				homeOrbiter.GetComponent<planetOrbit>().planet = homePlanet.transform;

		} else {
			//Win condition involves blowing up red star
			GameObject aliensWin = Network.Instantiate (alienWin,new Vector3(0f,0f,75000f),Quaternion.identity,0) as GameObject;
			aliensWin.GetComponent<winCondition>().enemySun = GameObject.Find ("Environment/RedSolarSystem/redSun");
			aliensWin.networkView.RPC ("rename", RPCMode.AllBuffered, "alienWinCondition");
			ship = alien_ship;
			GameObject homePlanet = GameObject.Find("Environment/BlueSolarSystem/blueOrbitingPlanets/blueO2/blueP2");
			homeOrbiter = Network.Instantiate (orbiter, homePlanet.transform.position, Quaternion.identity, 0) as GameObject;
			homeOrbiter.networkView.RPC("rename", RPCMode.AllBuffered, "alienOrbiter");
			if(homePlanet)
				homeOrbiter.GetComponent<planetOrbit>().planet = homePlanet.transform;
		}
		
		// Determine a spawn location and instantiate a new ship of the player's type
		Vector3 spawn_location = homeOrbiter.transform.Find("mirror/spawn_point").position;

		// player to be placed in the whorld
		GameObject player = Network.Instantiate (ship, spawn_location, Quaternion.identity, 0) as GameObject;
		
		// Look at the battleship orbiter to begin
		player.transform.LookAt(homeOrbiter.transform.position);

		if(Network.isServer)
			player.networkView.RPC ("rename", RPCMode.AllBuffered, "player1");
		else
			player.networkView.RPC ("rename", RPCMode.AllBuffered, "player2");

		// designate home planet for spawned player
		player.GetComponent<ShipCapacity>().homeShip = homeOrbiter;
		
		// designate respawn point
		player.GetComponent<Health>().spawn = homeOrbiter.transform.Find("mirror/spawn_point");

		if(Network.isServer){

			GameObject myUI = Instantiate (UI, new Vector3(0f,0f,-1), Quaternion.LookRotation(Vector3.forward)) as GameObject;
			myUI.name = "p1UI";
			GameObject.Find ("p1UI/element_miningProgress/component_wheels/wheel_beryllium/be_progress").GetComponent<progressBar>().container=homeOrbiter;
			GameObject.Find ("p1UI/element_miningProgress/component_wheels/wheel_boron/b_progress").GetComponent<progressBar>().container=homeOrbiter;
			GameObject.Find ("p1UI/element_miningProgress/component_wheels/wheel_deuterium/d_progress").GetComponent<progressBar>().container=homeOrbiter;
			GameObject.Find ("p1UI/element_miningProgress/component_wheels/wheel_helium/he_progress").GetComponent<progressBar>().container=homeOrbiter;
			GameObject.Find ("p1UI/element_miningProgress/component_wheels/wheel_lithium/li_progress").GetComponent<progressBar>().container=homeOrbiter;
			GameObject.Find ("p1UI/element_miningProgress/component_wheels/wheel_tritium/t_progress").GetComponent<progressBar>().container=homeOrbiter;
			GameObject.Find ("p1UI/element_deathrayProgress/component_green/g_progress").GetComponent<progressBar>().container=homeOrbiter;
			GameObject.Find ("p1UI/element_deathrayProgress/component_red/r_progress").GetComponent<enemyProgressBar>().enemyShipName="alienOrbiter";
			UI_TrackTarget tracker = myUI.transform.Find("Crosshair").gameObject.GetComponent<UI_TrackTarget>();
			if(tracker)
				tracker.target = player.transform;

		} else {

			GameObject myUI = Instantiate (UI, new Vector3(0f,0f,-1), Quaternion.LookRotation(Vector3.forward)) as GameObject;
			myUI.name="p2UI";
			GameObject.Find ("p2UI/element_miningProgress/component_wheels/wheel_beryllium/be_progress").GetComponent<progressBar>().container=homeOrbiter;
			GameObject.Find ("p2UI/element_miningProgress/component_wheels/wheel_boron/b_progress").GetComponent<progressBar>().container=homeOrbiter;
			GameObject.Find ("p2UI/element_miningProgress/component_wheels/wheel_deuterium/d_progress").GetComponent<progressBar>().container=homeOrbiter;
			GameObject.Find ("p2UI/element_miningProgress/component_wheels/wheel_helium/he_progress").GetComponent<progressBar>().container=homeOrbiter;
			GameObject.Find ("p2UI/element_miningProgress/component_wheels/wheel_lithium/li_progress").GetComponent<progressBar>().container=homeOrbiter;
			GameObject.Find ("p2UI/element_miningProgress/component_wheels/wheel_tritium/t_progress").GetComponent<progressBar>().container=homeOrbiter;
			GameObject.Find ("p2UI/element_deathrayProgress/component_green/g_progress").GetComponent<progressBar>().container=homeOrbiter;
			GameObject.Find ("p2UI/element_deathrayProgress/component_red/r_progress").GetComponent<enemyProgressBar>().enemyShipName="humanOrbiter";
			UI_TrackTarget tracker = myUI.transform.Find("Crosshair").gameObject.GetComponent<UI_TrackTarget>();
			if(tracker)
				tracker.target = player.transform;
		}

		// setting up this ship with its payload ui stuff
		if (Network.isServer)
			GameObject.Find ("p1UI/element_payloadBar/component_background").GetComponent<UI_payload> ().playerShip = player;
		else
			GameObject.Find ("p2UI/element_payloadBar/component_background").GetComponent<UI_payload> ().playerShip = player;
		// the camera which will follow the player
		
		//GameObject player_camera = Instantiate (cam, spawn_location,Quaternion.LookRotation(homeOrbiter.transform.position)) as GameObject;
		GameObject player_camera = Instantiate (cam, spawn_location,Quaternion.identity) as GameObject;
		
		if (Network.isServer)
			player_camera.name = "camView_p1";
		else
			player_camera.name = "camView_p2";
		
		player_camera.GetComponent<cameraFollow> ().target = player.transform;
		// have the backgroundCamera now follow the player's network camera
		GameObject player_BGcamera = Instantiate (backgroundCam, Vector3.zero, Quaternion.identity) as GameObject;
		player_BGcamera.GetComponent<paintBackground> ().parentCamera = player_camera.camera;
	}
	
	
	// Handle audio clips and music
	void getAudioClips()
	{
		AudioSource[] audio_sources = GetComponents<AudioSource>();
		
		title_music           = audio_sources[0];
		game_music            = audio_sources[1];
		prologue              = audio_sources[2];
		begin_mining          = audio_sources[3];
		end_mining            = audio_sources[4];
		deathray_instructions = audio_sources[5];
	}
	
	public void toggleTitleMusic(bool on)
	{
		if (on)
			title_music.Play ();
		else
			title_music.Stop ();
	}
	
	public void toggleGameMusic(bool on)
	{
		if (on)
			game_music.Play ();
		else
			game_music.Stop ();
	}
	
	public void playPrologueAudio()
	{
		if (played_prologue_audio)
			return;
			
		if (begin_mining.isPlaying)
			begin_mining.Stop();
		if (end_mining.isPlaying)
			end_mining.Stop();
		if (deathray_instructions.isPlaying)
			deathray_instructions.Stop();
		prologue.Play ();
		played_prologue_audio = true;
	}
	
	public void playMiningBeginAudio()
	{
		if (played_begin_mining_audio)
			return;
			
		if (prologue.isPlaying)
			prologue.Stop();
		if (end_mining.isPlaying)
			end_mining.Stop();
		if (deathray_instructions.isPlaying)
			deathray_instructions.Stop();
		begin_mining.Play ();
		played_begin_mining_audio = true;
	}
	
	public void playMiningEndAudio()
	{
		if (played_end_mining_audio)
			return;
			
		if (begin_mining.isPlaying)
			begin_mining.Stop();
		if (prologue.isPlaying)
			prologue.Stop();
		if (deathray_instructions.isPlaying)
			deathray_instructions.Stop();
		end_mining.Play ();
		played_end_mining_audio = true;
	}
	
	public void playDeathRayInstructionsAudio()
	{
		if (played_death_ray_audio)
			return;
			
		if (begin_mining.isPlaying)
			begin_mining.Stop();
		if (end_mining.isPlaying)
			end_mining.Stop();
		if (prologue.isPlaying)
			prologue.Stop();
		deathray_instructions.Play();
		played_death_ray_audio = true;
	}
}
