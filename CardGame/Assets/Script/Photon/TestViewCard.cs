using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestViewCard : MonoBehaviourPunCallbacks
{
    public int QuemJogou, CartaJogadaPlayer1 = 8, CartaJogadaPlayer2 = 8, 
        CartaJogada, ValueRandomAD1 = 0, ValueRandomAD2 = 0;

    private byte testeEnviarCard = 11;

    public static TestViewCard instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived;
    }
    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= NetworkingClient_EventReceived;
    }
    private void NetworkingClient_EventReceived(EventData obj)
    {
        if (obj.Code == testeEnviarCard)
        {
            object[] datas = (object[])obj.CustomData;

            //Pega os valores do Aparelho do Player1 e coloca no Player2
            if(!base.photonView.IsMine)
            {
                GameManager.instance.AtkP1  = System.Convert.ToSingle(datas[0]);
                GameManager.instance.AtkP2  = System.Convert.ToSingle(datas[1]);
                GameManager.instance.DefP1  = System.Convert.ToSingle(datas[2]);
                GameManager.instance.DefP2  = System.Convert.ToSingle(datas[3]);
                GameManager.instance.LifeP1 = System.Convert.ToSingle(datas[4]);
                GameManager.instance.LifeP2 = System.Convert.ToSingle(datas[5]);

                GameManager.instance.CardInimigoJ.transform.GetChild(1).GetComponent
                    <TextMeshProUGUI>().text = (GameManager.instance.AtkP1).ToString();
                GameManager.instance.CardPlayerJ.transform.GetChild(1).GetComponent
                    <TextMeshProUGUI>().text = (GameManager.instance.AtkP2).ToString();
                GameManager.instance.CardInimigoJ.transform.GetChild(2).GetComponent
                    <TextMeshProUGUI>().text = (GameManager.instance.DefP1).ToString();
                GameManager.instance.CardPlayerJ.transform.GetChild(2).GetComponent
                    <TextMeshProUGUI>().text = (GameManager.instance.DefP2).ToString();
                GameManager.instance.CardInimigoJ.transform.GetChild(3).GetChild(2).
                    GetComponent<TextMeshProUGUI>().text = (GameManager.instance.LifeP1).ToString();
                GameManager.instance.CardPlayerJ.transform.GetChild(3).GetChild(2).
                    GetComponent<TextMeshProUGUI>().text = (GameManager.instance.LifeP2).ToString();
            }
        }
    }
    public void UpdateValueP2()
    {
        if(GameManager.instance.Online)
        {
            object[] datas = new object[]
            {
                GameManager.instance.AtkP1,
                GameManager.instance.AtkP2,
                GameManager.instance.DefP1,
                GameManager.instance.DefP2,
                GameManager.instance.LifeP1,
                GameManager.instance.LifeP2
            };
            PhotonNetwork.RaiseEvent(testeEnviarCard, datas, RaiseEventOptions.Default,
                SendOptions.SendUnreliable);
        }
    }
}
