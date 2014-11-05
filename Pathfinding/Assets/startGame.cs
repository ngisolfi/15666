using UnityEngine;
using System.Collections;

public class startGame : MonoBehaviour {

	private bool running = false;
//	private SpawnShips enemySpawner;
//	private SpawnShips playerSpawner;
	private const string typeName = "Planetoids";
	private const string gameName = "Game1";
	public Object chatter;
	private HostData[] hostList;
	public GameObject enemySpawner;
	public GameObject playerSpawner;
	private int playerCount = 0;
	private string playerName = "";

	[RPC]
	void setPlayerName(string name)
	{
//		Debug.Log(name);
		playerName = name;
		GameObject box = GameObject.FindGameObjectWithTag("ChatBox");
		if(box)
			box.GetComponent<chatBox>().playerName = name;
	}

	void OnPlayerConnected(NetworkPlayer player) {
		playerCount++;
		networkView.RPC("setPlayerName",player,"Player " + playerCount);
//		Debug.Log("Player " + playerCount + " connected from " + player.ipAddress + ":" + player.port);
	}

	private void RefreshHostList()
	{
		MasterServer.RequestHostList(typeName);
	}
	
	void OnMasterServerEvent(MasterServerEvent msEvent)
	{
		if (msEvent == MasterServerEvent.HostListReceived)
			hostList = MasterServer.PollHostList();
	}

	private void JoinServer(HostData hostData)
	{
		Network.Connect(hostData);
	}
	
	void OnConnectedToServer()
	{
		Debug.Log("Server Joined");
		GameObject box = (GameObject) Instantiate(chatter);
		box.GetComponent<chatBox>().playerName = playerName;
		running = true;
	}

	private void StartServer()
	{
		Network.InitializeServer(4, 25000, !Network.HavePublicAddress());
		MasterServer.RegisterHost(typeName, gameName);

		playerName = "Host";
		GameObject box = (GameObject) Instantiate(chatter);
		box.GetComponent<chatBox>().playerName = playerName;
		StartGame();
		running = true;
	}

	public void StartGame()
	{
		Attack.resetLead();
		GameObject spawner = (GameObject) Instantiate(enemySpawner);
		spawner.GetComponent<SpawnShips>().BeginSpawning();
		spawner = (GameObject) Instantiate(playerSpawner);
		spawner.GetComponent<SpawnShips>().BeginSpawning();
	}
	
	void OnServerInitialized()
	{
		Debug.Log("Server Initializied");
//		running = false;
//		GameObject spawner = GameObject.Find("EnemySpawn");
//		if(spawner)
//			enemySpawner = spawner.GetComponent<SpawnShips>();
	}

	// Use this for initialization
	void Start () {
//		MasterServer.ipAddress = "127.0.0.1";

//		spawner = GameObject.Find("PlayerSpawn");
//		if(spawner)
//			playerSpawner = spawner.GetComponent<SpawnShips>();

	}

	void OnGUI()
	{
		if(!running){
//			GUIStyle centeredStyle = GUI.skin.GetStyle("Label");
//			centeredStyle.alignment = TextAnchor.UpperCenter;

			GUILayout.BeginArea(new Rect(Screen.width*0.4f,Screen.height*0.25f,Screen.width*0.2f,Screen.height*0.5f));
			GUILayout.BeginVertical();
//			GUILayout.Label("Planetoids");
			if (!Network.isClient && !Network.isServer)
			{
				if (GUILayout.Button("Start Server"))
					StartServer();
				
				if (GUILayout.Button("Refresh Hosts"))
					RefreshHostList();
				
				if (hostList != null)
				{
					for (int i = 0; i < hostList.Length; i++)
					{
						if (GUILayout.Button(hostList[i].gameName))
							JoinServer(hostList[i]);
					}
				}
			}

			GUILayout.EndVertical();
			GUILayout.EndArea();
		}else{
			GameObject[] enemySpawners = GameObject.FindGameObjectsWithTag("EnemySpawn");
			bool enemiesGone = enemySpawners.Length > 0;
			foreach(GameObject enemySpawn in enemySpawners){
				enemiesGone &= enemySpawn.GetComponent<SpawnShips>().isDepleted();
			}
			GameObject[] playerSpawners = GameObject.FindGameObjectsWithTag("PlayerSpawn");
			bool playersGone = playerSpawners.Length > 0;
			foreach(GameObject playerSpawn in playerSpawners){
				playersGone &= playerSpawn.GetComponent<SpawnPlayer>().isDepleted();
			}
			if(enemiesGone){
				GUI.Box(new Rect(Screen.width*0.25f,Screen.height*0.25f,Screen.width*0.5f,Screen.height*0.5f),"YOU WIN");
				if(GUI.Button(new Rect(Screen.width*0.4f,Screen.height*0.4f,Screen.width*0.2f,Screen.height*0.2f),"Play Again?"))
					StartGame();
			}else if(playersGone){
				GUI.Box(new Rect(Screen.width*0.25f,Screen.height*0.25f,Screen.width*0.5f,Screen.height*0.5f),"GAME OVER");
				if(GUI.Button(new Rect(Screen.width*0.4f,Screen.height*0.4f,Screen.width*0.2f,Screen.height*0.2f),"Play Again?"))
					StartGame();
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
