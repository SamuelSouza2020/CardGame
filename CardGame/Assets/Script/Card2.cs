using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card2 : MonoBehaviour
{
    public int InstCardForGame = 5;
    private void Start()
    {
        GerarCarta();
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
            int randomCard = Random.Range(0, GameManager.instance.DeckPlayer.Count);
            var cardspawn = Instantiate(GameManager.instance.CardPrefab);
            cardspawn.transform.SetParent(GameManager.instance.LocalCard.transform, false);
            //Verifica se a carta sorteada é sem o texto, para ativar.
            if (GameManager.instance.DeckPlayer[randomCard] == 0 || 
                GameManager.instance.DeckPlayer[randomCard] == 1)
                cardspawn.transform.GetChild(0).gameObject.SetActive(true);
            else
                cardspawn.transform.GetChild(0).gameObject.SetActive(false);
            //troca o sprite
            //cardspawn.GetComponent<Image>().sprite = GameManager.instance.TypeCard[randomCard];
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
        for(int cont = 0; cont < InstCardForGame; cont++)
        {
            int randomCard = Random.Range(0, GameManager.instance.DeckInimigo.Count);
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
            //troca o sprite
            //cardspawn.GetComponent<Image>().sprite = GameManager.instance.TypeCard[randomCard];
            //Numero do tipo da carta
            cardspawn.GetComponent<TesteInterno>().NumberTypeCard = GameManager.instance.DeckInimigo[randomCard];
            GameManager.instance.CardsGerationIni.Add(cardspawn);
            //Numero da carta
            cardspawn.GetComponent<TesteInterno>().NumberThis = GameManager.instance.CardsGerationIni.Count - 1;
            cardspawn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().enabled = false;
            GameManager.instance.DeckInimigo.RemoveAt(randomCard);
        }
        GameManager.instance.BtGerador.interactable = false;
    }
}
