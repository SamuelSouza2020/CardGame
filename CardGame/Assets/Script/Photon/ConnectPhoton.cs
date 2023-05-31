using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectPhoton : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        DebugConsole.instance.AddText("Connecting to server.", this);
        Debug.Log("Connecting to server.");
        //Carrega as cenas automaticamente entre os jogadores
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.NickName = MasterManager.GameSettings.NickName;
        PhotonNetwork.GameVersion = MasterManager.GameSettings.GameVersion;
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        //Debug.Log("Connected to server.");
        //Debug.Log(PhotonNetwork.LocalPlayer.NickName);
        Debug.Log("Connected to Photon.", this);
        DebugConsole.instance.AddText("Connected to Server.", this);

        Debug.Log("My nickname is " + PhotonNetwork.LocalPlayer.NickName, this);
        if (!PhotonNetwork.InLobby)
            PhotonNetwork.JoinLobby();
        //PhotonNetwork.JoinLobby();
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        DebugConsole.instance.AddText("Failed to connect to Photon: " + cause.ToString(), this);
        Debug.Log("Failed to connect to Photon: " + cause.ToString(), this);
    }
    public override void OnJoinedLobby()
    {
        DebugConsole.instance.AddText("Joined lobby");
        Debug.Log("Joined lobby");
    }
}
