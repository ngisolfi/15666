using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class chatBox : MonoBehaviour {
	
	class ChatEntry{
		public string name = "";
		public string message = "";
		public string timeTag = "";
	}
	
	ArrayList entries;
	Vector2 currentScrollPos = new Vector2();
	string inputField = "";
	bool chatInFocus = false;
	string inputFieldFocus = "CIFT";
	bool absPos = false;
	
	void Awake () {
		InitializeChat();
	}
	
	void InitializeChat(){
		entries = new ArrayList();
		unfocusChat();
	}
	
	//draw the chat box in size relative to your GUIlayout
	void OnGUI(){
//		GUI.BeginGroup(new Rect(5f,5f,Screen.width*0.3f,Screen.width*0.25f));
		GUILayout.BeginArea(new Rect(5f,5f,Screen.width*0.3f+5,Screen.width*0.25f+5));
		ChatWindow(Screen.width*0.3f);
		GUILayout.EndArea();
//		GUI.EndGroup();
	}
	
	void ChatWindow(float width){
		GUILayout.BeginVertical();
		currentScrollPos = GUILayout.BeginScrollView(currentScrollPos, GUILayout.MaxWidth(width), GUILayout.MinWidth(width)); //limits the chat window size to max 1000x1000, remove the restraints if you want
		
		foreach(ChatEntry ent in entries){
			GUILayout.BeginHorizontal();
			GUI.skin.label.wordWrap = true;
			GUILayout.Label(ent.timeTag + " "+ ent.name + ": "+ent.message);
			GUILayout.EndHorizontal();
			GUILayout.Space(3);
		}
		
		GUILayout.EndScrollView();
		if(chatInFocus){
			GUI.SetNextControlName(inputFieldFocus);
			inputField = GUILayout.TextField(inputField, GUILayout.MaxWidth(width), GUILayout.MinWidth(width));
			GUI.FocusControl(inputFieldFocus);
		}
		GUILayout.EndVertical();
		
		if(chatInFocus){
			HandleNewEntries();
		} else {
			checkForInput();
		}
		
	}
	
	void unfocusChat(){
		//Debug.Log("unfocusing chat");
		inputField = "";
		chatInFocus = false;
	}
	
	void checkForInput(){
		if(Event.current.type == EventType.KeyDown && Event.current.character == '\n' && !chatInFocus){
			GUI.FocusControl(inputFieldFocus);
			chatInFocus = true;
			currentScrollPos.y = float.PositiveInfinity;
		}
	}
	
	void HandleNewEntries(){
		if(Event.current.type == EventType.KeyDown && Event.current.character == '\n'){
			if(inputField.Length <= 0){
				unfocusChat();
				Debug.Log("unfocusing chat (empty entry)");
				return;
			}
			if(Network.isServer){
				AddChatEntry("Cookie monster", inputField); //for offline testing
//				networkView.RPC ("AddChatEntry", RPCMode.Others, "Cookie monster", inputField);
			}else
				networkView.RPC ("AddChatEntry", RPCMode.All, "Cookie monster", inputField);
			unfocusChat();
			//Debug.Log("unfocusing chat and entry sent");
		}
	}
	
	[RPC]
	void AddChatEntry(string name, string msg){
		ChatEntry newEntry = new ChatEntry();
		newEntry.name = name;
		newEntry.message = msg;
		newEntry.timeTag = "["+System.DateTime.Now.Hour.ToString()+":"+System.DateTime.Now.Minute.ToString()+"]";
		entries.Add(newEntry);
		currentScrollPos.y = float.PositiveInfinity;
	}
}