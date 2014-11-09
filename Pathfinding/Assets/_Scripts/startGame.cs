using UnityEngine;
using System.Collections;

public class startGame : MonoBehaviour {

	private bool running = false;
	private const string typeName = "Horizon Elite";
	private const string gameName = "Game1";
	public Object chatter;
	private HostData[] hostList;
	public GameObject playerSpawner;
	private int playerCount = 0;

	void OnPlayerConnected(NetworkPlayer player) {
		playerCount++;
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
		running = true;
		
		Instantiate(playerSpawner);
	}

	private void StartServer()
	{
		Network.InitializeServer(4, 25000, !Network.HavePublicAddress());
		MasterServer.RegisterHost(typeName, gameName);

		playerName = "Host";
		StartGame();
		running = true;
	}

	public void StartGame()
	{
		Attack.resetLead();
		Instantiate(playerSpawner);
	}
	
	void OnServerInitialized()
	{
		Debug.Log("Server Initializied");
	}

	void OnGUI()
	{
		if(!running){

			GUILayout.BeginArea(new Rect(Screen.width*0.4f,Screen.height*0.25f,Screen.width*0.2f,Screen.height*0.5f));
			GUILayout.BeginVertical();
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
