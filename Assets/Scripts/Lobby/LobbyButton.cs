using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LobbyButton : MonoBehaviour
{
    private RoomInfo _info;
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    public void Init(RoomInfo info)
    {
        _info = info;
        GetComponentInChildren<TextMeshProUGUI>().text = info.Name;
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(Join);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(Join);
    }

    private void Join()
    {
        PhotonNetwork.JoinRoom(_info.Name);
    }
}
