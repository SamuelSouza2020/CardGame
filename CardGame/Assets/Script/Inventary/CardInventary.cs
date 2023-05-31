using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CardInventary : MonoBehaviour
{
    InventaryManager _inventaryManager;
    public int NumberCard;
    bool checkRepeat = false;
    void Start()
    {
        _inventaryManager = GameObject.Find("InventaryManager").GetComponent<InventaryManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OptionCards()
    {
        _inventaryManager.CheckCards();
        transform.GetChild(3).gameObject.SetActive(true);

        
        if (_inventaryManager.CardsDeck.interactable)
        {
            for (int i = 0; i < MainController.instance.DeckPlayerOfficial.Count; i++)
            {
                if (MainController.instance.DeckPlayerOfficial[i] == NumberCard) 
                    checkRepeat = true;
            }
        }
        if(!checkRepeat)
            transform.GetChild(2).gameObject.SetActive(true);
    }
    public void RemoveAndAddCards()
    {
        if(!_inventaryManager.CardsDeck.interactable)
        {
            MainController.instance.DeckPlayerOfficial.Remove(NumberCard);
            MainController.instance.AmountCard.Remove(NumberCard);
            Destroy(gameObject);
        }
        else
        {
            MainController.instance.DeckPlayerOfficial.Add(NumberCard);
            MainController.instance.AmountCard.Add(NumberCard); 
            transform.GetChild(2).gameObject.SetActive(false);

        }
    }
}
