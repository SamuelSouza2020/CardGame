using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InventaryManager : MonoBehaviour
{

    public GameObject LocalCards;
    [SerializeField]
    int _cardsActive;
    public Button CardsDeck, AllCards;

    void Start()
    {
        CardsDeck.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void InstantiateCardsPlayer()
    {
        AllCards.interactable = true;
        CardsDeck.interactable = false;
        _cardsActive = LocalCards.transform.childCount;
        if(_cardsActive > 0)
        {
            for (int i = 0; i < _cardsActive; i++)
            {
                Destroy(LocalCards.transform.GetChild(_cardsActive - i - 1).gameObject);
            }
        }
        for (int i = 0; i < MainController.instance.DeckPlayerOfficial.Count; i++)
        {
            var spawn = Instantiate(MainController.instance.CardGameOFF, LocalCards.transform);
            spawn.GetComponent<Image>().sprite = MainController.instance.
                CardsAllGame[MainController.instance.DeckPlayerOfficial[i]];
            if(MainController.instance.DeckPlayerOfficial[i] != 0 &&
                MainController.instance.DeckPlayerOfficial[i] != 1)
            {
                spawn.transform.GetChild(0).gameObject.SetActive(false);
            }
            spawn.transform.GetChild(1).gameObject.SetActive(true);
            //spawn.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().
            //    text = MainController.instance.AmountCard[i].ToString();
            spawn.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().
                text = MainController.instance.AmountCard[MainController.instance.
                DeckPlayerOfficial[i]].ToString();
            spawn.GetComponent<CardInventary>().NumberCard = MainController.instance.DeckPlayerOfficial[i];
            spawn.transform.GetChild(2).GetComponent<Image>().color = 
                new Color(1f, 0.28f, 0.28f, 1f);
            spawn.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "REMOVER";
        }
    }
    public void InstantiateAllCards()
    {
        CardsDeck.interactable = true;
        AllCards.interactable = false;
        _cardsActive = LocalCards.transform.childCount;
        if (_cardsActive > 0)
        {
            for (int i = 0; i < _cardsActive; i++)
            {
                Destroy(LocalCards.transform.GetChild(_cardsActive-i-1).gameObject);
            }
        }
        for (int i = 0; i < MainController.instance.CardsPlayerOfficial.Count; i++)
        {
            var spawn = Instantiate(MainController.instance.CardGameOFF, LocalCards.transform);
            spawn.GetComponent<Image>().sprite = MainController.instance.
                CardsAllGame[MainController.instance.CardsPlayerOfficial[i]];
            if(MainController.instance.CardsPlayerOfficial[i] != 0 &&
                MainController.instance.CardsPlayerOfficial[i] != 1)
            {
                spawn.transform.GetChild(0).gameObject.SetActive(false);
            }
            spawn.transform.GetChild(1).gameObject.SetActive(false);
            spawn.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().
                text = MainController.instance.AmountCard[i].ToString();
            spawn.GetComponent<CardInventary>().NumberCard = i;
            spawn.transform.GetChild(2).GetComponent<Image>().color = new 
                Color(1f, 0.96f, 0.1745283f, 1f);
            spawn.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "ADICIONAR";
        }
    }
    public void CheckCards()
    {
        _cardsActive = LocalCards.transform.childCount;
        for (int i = 0; i < _cardsActive; i++)
        {
            if(LocalCards.transform.GetChild(i).GetChild(3).gameObject.activeSelf)
            {
                LocalCards.transform.GetChild(i).GetChild(2).gameObject.SetActive(false);
                LocalCards.transform.GetChild(i).GetChild(3).gameObject.SetActive(false);
            }
        }
    }
    public void BackMenu()
    {
        SceneManager.LoadScene(4);
    }
}
