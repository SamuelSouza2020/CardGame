using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardsManager : MonoBehaviour
{
    [SerializeField]
    List<Sprite> TypeCardImage;
    [SerializeField]
    GameObject FormatCard;
    [SerializeField]
    ModoOffIA _controllIA;

    public int InstCardForGame = 5;

    private void Start()
    {
        GerarCarta();
    }
    public void GerarCarta()
    {
        //Deck IA
        for (int i = 0; i < InstCardForGame; i++)
        {
            //int randomCard = Random.Range(0, _controllIA.CardsIA.Count);
            var cardspawn = Instantiate(FormatCard);
            cardspawn.transform.SetParent(_controllIA.LocalMaoIA.transform, false);
            cardspawn.transform.GetChild(1).gameObject.SetActive(true);
            cardspawn.GetComponent<CardFunctionInterno>().ModoIA = true;
            //_controllIA.CardsIA.Remove(randomCard);
        }
        //Deck Player
        for (int i = 0; i < InstCardForGame; i++)
        {
            int randomCard = Random.Range(0, MainController.instance.DeckPlayerGame.Count);
            var cardspawn = Instantiate(FormatCard);
            cardspawn.transform.SetParent(MainController.instance.LocalMaoP.transform, false);
            cardspawn.GetComponent<Image>().sprite = MainController.instance.
                CardsAllGame[MainController.instance.DeckPlayerGame[randomCard]];
            if(MainController.instance.DeckPlayerGame[randomCard] != 0 &&
                MainController.instance.DeckPlayerGame[randomCard] != 1)
            {
                cardspawn.transform.GetChild(0).gameObject.SetActive(false);
            }

            cardspawn.GetComponent<CardFunctionInterno>().NumberCard =
                MainController.instance.DeckPlayerGame[randomCard];
            MainController.instance.DeckPlayerGame.RemoveAt(randomCard);
        }

        //Chamar primeira carta IA para a mesa
        //int randomCardFirst = Random.Range(0, _controllIA.CardsIA.Count);
        //var cardspawnFirst = Instantiate(FormatCard);
        //cardspawnFirst.transform.SetParent(_controllIA.LocalMesaIA.transform, false);
        //cardspawnFirst.transform.GetChild(1).gameObject.SetActive(true);
        //cardspawnFirst.GetComponent<Image>().sprite = MainController.instance.
        //        CardsAllGame[_controllIA.CardsIA[randomCardFirst]];
        //if (_controllIA.CardsIA[randomCardFirst] != 0 && _controllIA.CardsIA[randomCardFirst] != 1)
        //    cardspawnFirst.transform.GetChild(0).gameObject.SetActive(false);
        //cardspawnFirst.GetComponent<CardFunctionInterno>().NumberCard = _controllIA.CardsIA[randomCardFirst];
        //_controllIA.CardsIA.Remove(randomCardFirst);
        //Destroy(_controllIA.LocalMaoIA.transform.GetChild(2).gameObject);
    }
}
