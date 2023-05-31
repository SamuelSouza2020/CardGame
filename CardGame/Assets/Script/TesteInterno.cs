using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TesteInterno : MonoBehaviourPunCallbacks
{
    TextMeshProUGUI txtNumber;
    public int NumberThis, numberRandom, NumberTypeCard;
    //public bool achouCard = false;

    private byte testeEnviarCard = 10;
    void Start()
    {
        txtNumber = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        numberRandom = UnityEngine.Random.Range(10, 16);
        txtNumber.text = numberRandom.ToString("0");
    }
    //Multiplayer
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
        try
        {
            if(GameManager.instance.Online)
            {
                if (obj.Code == testeEnviarCard)
                {
                    object[] datas = (object[])obj.CustomData;

                    //Acrescentar se a carta tem valor se for tipo 0 ou 1, caso n tenha, deixa o valor como 0,
                    //e fazer um if se for 0 para desativar o text

                    GameManager.instance.TwoPlayers = Convert.ToInt32(datas[0]);//Se o player ja jogou
                    TestViewCard.instance.CartaJogada = Convert.ToInt32(datas[1]);//Qual carta jogou
                    TestViewCard.instance.ValueRandomAD1 = Convert.ToInt32(datas[2]);//Valor da Carta
                    TestViewCard.instance.ValueRandomAD2 = Convert.ToInt32(datas[3]);//Valor da Carta

                }
            }
        }
        catch (Exception e)
        {
            GameManager.instance.ErrorGame.SetActive(true);
            GameManager.instance.ErrorGame.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = e.Message;
        }
    }
    public void Teste()
    {
        if (!GameManager.instance.AcabouRound)
        {
            GameManager.instance.Aguardando = true;
            GameManager.instance.TwoPlayers++;
            TestViewCard.instance.CartaJogada = NumberTypeCard;
            GameManager.instance.CardsGeration[NumberThis].transform.SetParent
                (GameManager.instance.MesaJ1.transform, false);

            //Contar tempo da jogada
            if(GameManager.instance.Online)
            {
                GameManager.instance.TempoDeJogar.TimeReset(20);
                GameManager.instance.TempoDeJogar.enabled = false;
                GameManager.instance.TempoDeJogar.gameObject.SetActive(false);
            }

            if (GameManager.instance.TwoPlayers == 1)
            {
                //TestViewCard.instance.CartaJogadaPlayer1 = TestViewCard.instance.CartaJogada;
                if (NumberTypeCard == 0 || NumberTypeCard == 1)
                {
                    TestViewCard.instance.ValueRandomAD1 = numberRandom;
                }
                //Contar tempo da jogada
                if(GameManager.instance.Online)
                {
                    GameManager.instance.TempoDeJogar.TimeReset(20);
                    GameManager.instance.TempoDeJogar.enabled = true;
                    GameManager.instance.TempoDeJogar.gameObject.SetActive(true);
                }
            }
            else if (GameManager.instance.TwoPlayers == 2)
            {
                if (NumberTypeCard == 0 || NumberTypeCard == 1)
                {
                    TestViewCard.instance.ValueRandomAD2 = numberRandom;
                }
            }

            GameManager.instance.CardsGeration.RemoveAt(NumberThis);

            foreach (var card in GameManager.instance.CardsGeration)
            {
                if (card.transform.GetComponent<TesteInterno>().NumberThis > NumberThis)
                {
                    card.transform.GetComponent<TesteInterno>().NumberThis =
                    card.transform.GetComponent<TesteInterno>().NumberThis - 1;
                }
            }

            GameManager.instance.VezInimigo = true;
            //colocar o valor aqui


            if (GameManager.instance.Online)
            {
                //Teste colocar no objeto
                object[] datas = new object[] { GameManager.instance.TwoPlayers,
                    TestViewCard.instance.CartaJogada, TestViewCard.instance.
                    ValueRandomAD1, TestViewCard.instance.ValueRandomAD2};
                //Teste passar p photon
                PhotonNetwork.RaiseEvent(testeEnviarCard, datas, RaiseEventOptions.Default,
                    SendOptions.SendUnreliable);
            }
            //Teste Chamar IA
            GameManager.instance.TwoPlayers++;
            GameManager.instance.AcabouTempo = false;
        }
    }
    public void TesteInimigo()
    {
        //troca o sprite
        gameObject.GetComponent<Image>().sprite = GameManager.instance.TypeCard[NumberTypeCard];
        GameManager.instance.InimigoJogou = true;
    }
    public void CallTypeCard(int value, GameObject quemJogou)
    {
        float valueInterno = 0f, valueSum = 0f;
        switch (value)
        {
            case 0:
                //Adiciona ataque aleatorio no inimigo
                valueInterno = float.Parse(quemJogou.transform.GetChild(1).
                    GetComponent<TextMeshProUGUI>().text) + numberRandom;
                quemJogou.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = valueInterno.ToString("0.0");
                break;
            case 1:
                //Adiciona defesa aleatoria no inimigo
                valueInterno = float.Parse(quemJogou.transform.GetChild(2).
                    GetComponent<TextMeshProUGUI>().text) + numberRandom;
                quemJogou.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = valueInterno.ToString("0.0");
                break;
            case 2:
                valueInterno = float.Parse(quemJogou.transform.GetChild(1).
                GetComponent<TextMeshProUGUI>().text);
                valueSum = ((valueInterno / 100) * 50) + valueInterno;
                quemJogou.transform.GetChild(1).GetComponent<TextMeshProUGUI>().
                    text = valueSum.ToString("0.0");
                break;
            case 3:
                valueInterno = float.Parse(quemJogou.transform.GetChild(2).
                        GetComponent<TextMeshProUGUI>().text);
                valueSum = ((valueInterno / 100) * 50) + valueInterno;
                quemJogou.transform.GetChild(2).GetComponent<TextMeshProUGUI>().
                    text = valueSum.ToString("0.0");
                break;
            case 4:
                //Adiciona vida por porcentagem
                valueInterno = float.Parse(quemJogou.transform.GetChild(3).GetChild(2).
                GetComponent<TextMeshProUGUI>().text);
                if (valueInterno < 200)
                {
                    valueSum = ((valueInterno / 100) * 20) + valueInterno;
                    if (valueSum > 200)
                    {
                        valueSum = 200;
                    }
                    else if (valueSum > 100)
                    {
                        quemJogou.transform.GetChild(3).GetChild(4).
                            GetComponent<TextMeshProUGUI>().text = "200";
                        quemJogou.transform.GetChild(3).GetChild(1).gameObject.SetActive(true);
                        quemJogou.transform.GetChild(3).GetChild(1).GetComponent<Image>().
                            fillAmount = (valueSum - 100) / 100;
                        quemJogou.transform.GetChild(3).GetChild(0).GetComponent<Image>().fillAmount = 1;
                    }
                    else
                    {
                        quemJogou.transform.GetChild(3).GetChild(4).
                            GetComponent<TextMeshProUGUI>().text = "100";
                        quemJogou.transform.GetChild(3).GetChild(1).gameObject.SetActive(false);
                    }
                    quemJogou.transform.GetChild(3).GetChild(2).
                        GetComponent<TextMeshProUGUI>().text = valueSum.ToString("0.0");
                }
                else
                {
                    quemJogou.transform.GetChild(3).GetChild(1).
                        GetComponent<TextMeshProUGUI>().text = "200";
                }
                break;
            case 5:
                valueInterno = float.Parse(quemJogou.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text);
                valueSum = valueInterno - ((valueInterno / 100) * 50);
                quemJogou.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text =
                    valueSum.ToString("0.0");
                break;
            case 6:
                valueInterno = float.Parse(quemJogou.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text);
                valueSum = valueInterno - ((valueInterno / 100) * 50);
                quemJogou.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
                    valueSum.ToString("0.0");
                break;
        }
    }
    
}
