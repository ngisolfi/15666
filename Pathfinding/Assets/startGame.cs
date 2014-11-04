using UnityEngine;
using System.Collections;

public class startGame : MonoBehaviour {

	private bool running = false;
	private SpawnShips enemySpawner;
//	private SpawnShips playerSpawner;
	private const string typeName = "Planetoids";
	private const string gameName = "Game1";
	public Object chatBox;
	private HostData[] hostList;
	public GameObject playerSpawner;
	
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
		Instantiate(chatBox);
		running = true;
	}

	private void StartServer()
	{
		Network.InitializeServer(4, 25000, !Network.HavePublicAddress());
		MasterServer.RegisterHost(typeName, gameName);

		Instantiate(chatBox);
		enemySpawner.BeginSpawning();
		GameObject spawner = (GameObject) Instantiate(playerSpawner);
		spawner.GetComponent<SpawnShips>().BeginSpawning();
		running = true;
	}
	
	void OnServerInitialized()
	{
		Debug.Log("Server Initializied");
//		running = false;
		GameObject spawner = GameObject.Find("EnemySpawn");
		if(spawner)
			enemySpawner = spawner.GetComponent<SpawnShips>();
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
			GUIStyle centeredStyle = GUI.skin.GetStyle("Label");
			centeredStyle.alignment = TextAnchor.UpperCenter;

			GUILayout.BeginArea(new Rect(Screen.width*0.4f,Screen.height*0.25f,Screen.width*0.2f,Screen.height*0.5f));
			GUILayout.BeginVertical();
			GUILayout.Label("Planetoids");
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
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
