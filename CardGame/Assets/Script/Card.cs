using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviourPunCallbacks
{
    public int InstCardForGame = 5;
    bool _criaDeckIA = true;
    private void Start()
    {
        _criaDeckIA = true;
        GerarCarta();
        GameManager.instance.TempoDeJogar.gameObject.SetActive(true);
    }
    public void GerarCarta()
    {
        GameManager.instance.AtkRoundPlayer = float.Parse(GameManager.instance.
            CardPlayerJ.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text);
        GameManager.instance.AtkRoundInimigo = float.Parse(GameManager.instance.
            CardInimigoJ.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text);
        GameManager.instance.DefRoundPlayer = float.Parse(GameManager.instance.
            CardPlayerJ.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text);
        GameManager.instance.DefRoundInimigo = float.Parse(GameManager.instance.
            CardInimigoJ.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text);

        //Cartas do Player
        for (int cont = 0; cont < InstCardForGame; cont++)
        {
            int randomCard = UnityEngine.Random.Range(0, GameManager.instance.DeckPlayer.Count);
            var cardspawn = Instantiate(GameManager.instance.CardPrefab);
            cardspawn.transform.SetParent(GameManager.instance.LocalCard.transform, false);
            //Verifica se a carta sorteada é sem o texto, para ativar.
            if (GameManager.instance.DeckPlayer[randomCard] == 0 ||
                GameManager.instance.DeckPlayer[randomCard] == 1)
                cardspawn.transform.GetChild(0).gameObject.SetActive(true);
            else
                cardspawn.transform.GetChild(0).gameObject.SetActive(false);
            cardspawn.GetComponent<Image>().sprite = GameManager.instance.
                TypeCard[GameManager.instance.DeckPlayer[randomCard]];
            //Numero do tipo da carta
            cardspawn.GetComponent<TesteInterno>().NumberTypeCard = GameManager.instance.DeckPlayer[randomCard];
            GameManager.instance.CardsGeration.Add(cardspawn);
            //Numero da carta
            cardspawn.GetComponent<TesteInterno>().NumberThis = GameManager.instance.CardsGeration.Count - 1;
            GameManager.instance.DeckPlayer.RemoveAt(randomCard);
        }
        //Cartas do Inimigo
        for (int cont = 0; cont < InstCardForGame; cont++)
        {
            int randomCard = UnityEngine.Random.Range(0, GameManager.instance.DeckInimigo.Count);
            var cardspawn = Instantiate(GameManager.instance.CardPrefab);
            cardspawn.GetComponent<Button>().interactable = false;
            cardspawn.transform.SetParent(GameManager.instance.LocalCardIni.transform, false);
            cardspawn.GetComponent<Image>().sprite = GameManager.instance.CardVerso;

            //Verifica se a carta sorteada é sem o texto, para ativar.
            if (GameManager.instance.DeckInimigo[randomCard] == 0 ||
                GameManager.instance.DeckInimigo[randomCard] == 1)
                cardspawn.transform.GetChild(0).gameObject.SetActive(true);
            else
                cardspawn.transform.GetChild(0).gameObject.SetActive(false);
            //Numero do tipo da carta
            cardspawn.GetComponent<TesteInterno>().NumberTypeCard = GameManager.instance.DeckInimigo[randomCard];
            GameManager.instance.CardsGerationIni.Add(cardspawn);
            //Numero da carta
            cardspawn.GetComponent<TesteInterno>().NumberThis = GameManager.instance.CardsGerationIni.Count - 1;
            cardspawn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().enabled = false;
            GameManager.instance.DeckInimigo.RemoveAt(randomCard);
        }
        
        if(_criaDeckIA)
        {
            //Decks para IA dos dois players
            for (int count = 0; count < GameManager.instance.TypeCard.Count; count++)
            {
                GameManager.instance.DeckSub1[count] = 5;
                GameManager.instance.DeckSub2[count] = 5;
            }
            _criaDeckIA = false;
        }

        GameManager.instance.BtGerador.interactable = false;

        //Contar tempo da jogada
        if (GameManager.instance.Online)
        {
            GameManager.instance.TempoDeJogar.TimeReset(5);
            GameManager.instance.TempoDeJogar.enabled = true;
            GameManager.instance.TempoDeJogar.gameObject.SetActive(true);
        }
    }
    public void InvokeCard()
    {
        if(GameManager.instance.Online)
        {
            if (!base.photonView.IsMine && GameManager.instance.TwoPlayers == 1)
            {
                GameManager.instance.LocalCardIni.transform.GetChild(4).transform.SetParent(GameManager.instance.MesaIni.transform, false);
                GameManager.instance.CardsGerationIni.RemoveAt(4);
                GameManager.instance.MesaIni.transform.GetChild(0).GetComponent<TesteInterno>().NumberTypeCard = TestViewCard.instance.CartaJogadaPlayer1;
                GameManager.instance.MesaIni.transform.GetChild(0).GetComponent<Image>().sprite = GameManager.instance.TypeCard[TestViewCard.instance.CartaJogadaPlayer1];

                //Retirar do deck da IA1 a carta jogada pelo Player1
                GameManager.instance.DeckSub1[TestViewCard.instance.CartaJogadaPlayer1]--;
                GameManager.instance.MesaIni.transform.GetChild(0).GetComponent<TesteInterno>().TesteInimigo();
                GameManager.instance.MesaIni.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);

                if (TestViewCard.instance.CartaJogadaPlayer1 == 0 || TestViewCard.instance.CartaJogadaPlayer1 == 1)
                {
                    GameManager.instance.MesaIni.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().
                        text = TestViewCard.instance.ValueRandomAD1.ToString();
                    GameManager.instance.MesaIni.transform.GetChild(0).GetComponent<TesteInterno>().
                       numberRandom = TestViewCard.instance.ValueRandomAD1;
                }
                GameManager.instance.MesaIni.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);

                //Resetar o tempo do Player2
                GameManager.instance.TempoDeJogar.TimeReset(5);
                GameManager.instance.TempoDeJogar.enabled = true;
                GameManager.instance.TempoDeJogar.gameObject.SetActive(true);
            }
            if (base.photonView.IsMine && GameManager.instance.TwoPlayers == 2)
            {
                GameManager.instance.LocalCardIni.transform.GetChild(4).transform.SetParent(GameManager.instance.MesaIni.transform, false);
                GameManager.instance.CardsGerationIni.RemoveAt(4);
                GameManager.instance.MesaIni.transform.GetChild(0).GetComponent<TesteInterno>().NumberTypeCard = TestViewCard.instance.CartaJogadaPlayer2;
                GameManager.instance.MesaIni.transform.GetChild(0).GetComponent<Image>().sprite = GameManager.instance.TypeCard[TestViewCard.instance.CartaJogadaPlayer2];

                //Retirar do deck da IA1 a carta jogada pelo Player1
                GameManager.instance.DeckSub2[TestViewCard.instance.CartaJogadaPlayer2]--;

                GameManager.instance.MesaIni.transform.GetChild(0).GetComponent<TesteInterno>().TesteInimigo();
                GameManager.instance.MesaIni.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);

                if (TestViewCard.instance.CartaJogadaPlayer2 == 0 || TestViewCard.instance.CartaJogadaPlayer2 == 1)
                {
                    GameManager.instance.MesaIni.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().
                        text = TestViewCard.instance.ValueRandomAD2.ToString();
                    GameManager.instance.MesaIni.transform.GetChild(0).GetComponent<TesteInterno>().
                       numberRandom = TestViewCard.instance.ValueRandomAD2;
                }
            }
        }
        else
        {
            Debug.Log("EntrouElse2");
            int spawnRandom = UnityEngine.Random.Range(0, GameManager.instance.DeckIA.Count);
            GameManager.instance.LocalCardIni.transform.GetChild(4).transform.
                SetParent(GameManager.instance.MesaIni.transform, false);
            GameManager.instance.CardsGerationIni.RemoveAt(4);
            GameManager.instance.MesaIni.transform.GetChild(0).GetComponent<TesteInterno>().
                NumberTypeCard = GameManager.instance.DeckIA[spawnRandom];
            GameManager.instance.MesaIni.transform.GetChild(0).GetComponent<Image>().sprite =
                GameManager.instance.TypeCard[GameManager.instance.DeckIA[spawnRandom]];
            GameManager.instance.MesaIni.transform.GetChild(0).GetComponent<TesteInterno>().TesteInimigo();
            GameManager.instance.MesaIni.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
            GameManager.instance.MesaIni.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
            if(GameManager.instance.DeckIA[spawnRandom] == 0 || GameManager.instance.DeckIA[spawnRandom] == 1)
            {
                Debug.Log("Entrou IF01");
                GameManager.instance.MesaIni.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                GameManager.instance.MesaIni.transform.GetChild(0).GetChild(0).
                    GetComponent<TextMeshProUGUI>().enabled = true;
            }

            if (GameManager.instance.CardPlayerJ.gameObject.name == "Inimigo")
            {
                //Retirar do deck da IA1 a carta jogada pelo Player1
                //GameManager.instance.DeckSub1[GameManager.instance.DeckIA[spawnRandom]]--;

                //if (GameManager.instance.DeckIA[spawnRandom] == 0 
                //    || GameManager.instance.DeckIA[spawnRandom] == 1)
                //    GameManager.instance.MesaIni.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                //GameManager.instance.MesaIni.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);

                //Resetar o tempo do Player2
                //if (GameManager.instance.Online)
                //{
                //    GameManager.instance.TempoDeJogar.TimeReset(5);
                //    GameManager.instance.TempoDeJogar.enabled = true;
                //    GameManager.instance.TempoDeJogar.gameObject.SetActive(true);
                //}
            }
            //P1
            if (GameManager.instance.CardPlayerJ.gameObject.name == "Player")
            {
                //Retirar do deck da IA1 a carta jogada pelo Player1
                //GameManager.instance.DeckSub1[GameManager.instance.DeckIA[spawnRandom]]--;

                //if (GameManager.instance.DeckIA[spawnRandom] == 0 || GameManager.instance.DeckIA[spawnRandom] == 1)
                //{
                //    GameManager.instance.MesaIni.transform.GetChild(0).GetChild(0).GetComponent
                //        <TextMeshProUGUI>().text = TestViewCard.instance.ValueRandomAD1.ToString();
                //    GameManager.instance.MesaIni.transform.GetChild(0).GetComponent<TesteInterno>().
                //       numberRandom = TestViewCard.instance.ValueRandomAD1;
                //}
                //GameManager.instance.MesaIni.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);

                //Resetar o tempo do Player2
                //if (GameManager.instance.Online)
                //{
                //    GameManager.instance.TempoDeJogar.TimeReset(5);
                //    GameManager.instance.TempoDeJogar.enabled = true;
                //    GameManager.instance.TempoDeJogar.gameObject.SetActive(true);
                //}
                //GameManager.instance.TwoPlayers++;
                //GameManager.instance.Aguardando = true;
            }
        }
    }
}
