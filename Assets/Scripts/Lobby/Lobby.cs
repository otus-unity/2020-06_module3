using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Lobby : MonoBehaviourPunCallbacks
{
    public TMP_InputField InputField;
    public LobbyButton buttonPrefab;
    public Transform scrollListContents;
    private readonly List<LobbyButton> _buttons = new List<LobbyButton>();

    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.GameVersion = "1.0";
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogError($"Disconnected: {cause}");
    }

    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            PhotonNetwork.LoadLevel("Game");
        }
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (LobbyButton button in _buttons)
        {
            Destroy(button.gameObject);
        }

        _buttons.Clear();
        foreach (RoomInfo room in roomList)
        {
            LobbyButton button = Instantiate(buttonPrefab, scrollListContents);
            button.Init(room);
            _buttons.Add(button);
        }
    }

    public void CreateRoom()
    {
        RoomOptions options = new RoomOptions{ MaxPlayers = 10 };
        PhotonNetwork.CreateRoom(InputField.text, options);
    }
}
