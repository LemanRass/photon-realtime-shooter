using Photon.Pun;
using UnityEngine;

public class ConnectionManager : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master");
        PhotonNetwork.JoinLobby();
    }
    
    public override void OnJoinedLobby()
    {
        Debug.Log("Joined lobby");
        PhotonNetwork.JoinRandomRoom();
    }
    
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to join random room");
        PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions { MaxPlayers = 2 });
    }
    
    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room");
        PhotonNetwork.LoadLevel("Game");
    }
}
