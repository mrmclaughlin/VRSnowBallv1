using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
[System.Serializable]
public class DefaultRoom
{
	public string Name;
	public int sceneIndex;
	public int maxPlayer;
	
	
}



public class NetworkManager : MonoBehaviourPunCallbacks
{
	public List<DefaultRoom> defaultRooms;
	public GameObject roomUI;
	public GameObject ConnectUI;
    // Start is called before the first frame update
    void Start()
    {
       //ConnectToServer(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public void ConnectToServer(){
		PhotonNetwork.ConnectUsingSettings();
		Debug.Log("Yo!!! Try Connect to Server");
		
	}
	public override void OnConnectedToMaster()
	{
		Debug.Log("I am connected to Server");
		base.OnConnectedToMaster();
		PhotonNetwork.JoinLobby();
	}
	public override void OnJoinedRoom(){
		Debug.Log("Yeah we are in a room");
		base.OnJoinedRoom();
		
	}
public override void OnJoinedLobby(){
	base.OnJoinedLobby();
	Debug.Log("We Joined the Lobby");
	roomUI.gameObject.SetActive(true);
	ConnectUI.gameObject.SetActive(false);
	
}
	
	public void InitiliazeRoom(int defaultRoomIndex){
		DefaultRoom roomSettings = defaultRooms[defaultRoomIndex];
		
		//Load Scene
		PhotonNetwork.LoadLevel(roomSettings.sceneIndex);
		RoomOptions roomOptions = new RoomOptions();
		roomOptions.MaxPlayers = (byte)roomSettings.maxPlayer;
		roomOptions.IsVisible = true;
		roomOptions.IsOpen = true;
		PhotonNetwork.JoinOrCreateRoom(roomSettings.Name,roomOptions,TypedLobby.Default);
	}
	
	
	
	public override void OnPlayerEnteredRoom(Player newPlayer)
	{
			Debug.Log("A new player is in the room now");
			base.OnPlayerEnteredRoom(newPlayer);
	}
	
}
