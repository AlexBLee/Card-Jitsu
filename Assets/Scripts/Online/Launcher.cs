using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;
using TMPro;

public class Launcher : MonoBehaviourPunCallbacks
{
    // Photon Settings
    private const string playerNamePrefKey = "PlayerName";
    private byte maxPlayersPerRoom = 2;
    private string gameVersion = "1";
    private bool isConnecting;

    // Scene
    [SerializeField] private GameObject controlPanel = null;
    [SerializeField] private GameObject progressLabel = null;
    [SerializeField] private Button connectButton = null;
    [SerializeField] private InputField inputField;

    private void Awake()
    {
        connectButton.onClick.AddListener(Connect);
        PhotonNetwork.AutomaticallySyncScene = true;

        string defaultName = string.Empty;

        if (inputField != null)
        {
            if (PlayerPrefs.HasKey(playerNamePrefKey))
            {
                defaultName = PlayerPrefs.GetString(playerNamePrefKey);
                inputField.text = defaultName;
            }
        }

        PhotonNetwork.NickName = defaultName;

    }

    private void Connect()
    {
        progressLabel.SetActive(true);
        controlPanel.SetActive(false);

        // Check if we're connected or not, we join if we are, else we initiate connection to server.
        if (PhotonNetwork.IsConnected)
        {
            // we need at this point to attempt joining a Random Room, if Fail, we'll get notified in OnJoinRandomFailed() and we'll create one.
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            // we must first and foremost connect to the Photon Online Server.
            // keep track of the will to join a room, because when we come back from the game, we will get a callback that we are connected
            // so we need to know what to do then
            isConnecting = PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
        }
    }

    public void SetPlayerName(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            Debug.LogError("Player name is null or empty.");
            return;
        }

        PhotonNetwork.NickName = value;
        PlayerPrefs.SetString(playerNamePrefKey, value);
    }


    public void LoadLevel()
    {
        PhotonNetwork.LoadLevel("Main Multiplayer Test");
    }


    #region MonoBehaviourPunCallBacks Callbacks

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master called by Pun");

        // we don't want to do anything if we are not attempting to join a room.
        // this case where isConnecting is false is typically when you lost or quit the game, when this level is loaded, OnConnectedToMaster will be called, in that case
        // we don't want to do anything.
        if (isConnecting)
        {
            // #Critical: The first we try to do is to join a potential existing room. If there is, good, else, we'll be called back with OnJoinRandomFailed()
            PhotonNetwork.JoinRandomRoom();
            isConnecting = false;

        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        progressLabel.SetActive(false);
        controlPanel.SetActive(true);
        isConnecting = false;

        Debug.LogWarningFormat("OnDisconnected called with reason: {0}", cause);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("No random room available.. creating");

        // #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Client is now in room..");
    }


    #endregion

}
