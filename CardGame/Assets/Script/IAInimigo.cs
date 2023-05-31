using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAInimigo : MonoBehaviourPunCallbacks
{
    public int SomaDeck = 0;

    public static IAInimigo instance;

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
    public void SimularPlayer2()
    {
        if(!GameManager.instance.Online)
        {
            if(GameManager.instance.CardPlayerJ.gameObject.name == "Player")
            {
                int soma = 0;
                for (int i = 0; i < GameManager.instance.DeckSub1.Count; i++)
                {
                    soma += GameManager.instance.DeckSub1[i];
                }
                for (int i = 0; i < soma; i++)
                {
                    GameManager.instance.DeckIA.Add(0);
                }
                int contDeck = 0;
                for (int i = 0; i < GameManager.instance.DeckIA.Count; i++)
                {
                    if (GameManager.instance.DeckSub1[contDeck] <= 0)
                        contDeck++;
                    GameManager.instance.DeckIA[i] = contDeck;
                    GameManager.instance.DeckSub1[contDeck]--;
                }
            }
            if(GameManager.instance.CardPlayerJ.gameObject.name == "Inimigo")
            {
                int soma = 0;
                for (int i = 0; i < GameManager.instance.DeckSub2.Count; i++)
                {
                    soma += GameManager.instance.DeckSub2[i];
                }
                for (int i = 0; i < soma; i++)
                {
                    GameManager.instance.DeckIA.Add(0);
                }
                int contDeck = 0;
                for (int i = 0; i < GameManager.instance.DeckIA.Count; i++)
                {
                    if (GameManager.instance.DeckSub2[contDeck] <= 0)
                        contDeck++;
                    GameManager.instance.DeckIA[i] = contDeck;
                    GameManager.instance.DeckSub2[contDeck]--;
                }
            }
            //GameManager.instance.TwoPlayers++;
        }
    }
    public void ResultGameIA(int value1, int value2)
    {
        int card1 = 0;
        int card2 = 0;
        if (GameManager.instance.CardPlayerJ.gameObject.name == "Player")
        {
            card1 = GameManager.instance.MesaJ1.transform.GetChild(0).GetComponent
                <TesteInterno>().NumberTypeCard;
            card2 = GameManager.instance.MesaIni.transform.GetChild(0).GetComponent
                <TesteInterno>().NumberTypeCard;
        }
        if (GameManager.instance.CardPlayerJ.gameObject.name == "Inimigo")
        {
            card1 = GameManager.instance.MesaIni.transform.GetChild(0).GetComponent
                <TesteInterno>().NumberTypeCard;
            card2 = GameManager.instance.MesaJ1.transform.GetChild(0).GetComponent
                <TesteInterno>().NumberTypeCard;
        }

        if (card1 != 5 && card1 != 6)
            GameManager.instance.TypeManager.TypeController
                (card1, GameManager.instance.CardPlayerJ, value1);
        else
            GameManager.instance.TypeManager.TypeController
                (card1, GameManager.instance.CardInimigoJ, value1);

        if (card2 != 5 && card2 != 6)
            GameManager.instance.TypeManager.TypeController
                (card2, GameManager.instance.CardInimigoJ, value2);
        else
            GameManager.instance.TypeManager.TypeController
                (card2, GameManager.instance.CardPlayerJ, value2);

    }
}
