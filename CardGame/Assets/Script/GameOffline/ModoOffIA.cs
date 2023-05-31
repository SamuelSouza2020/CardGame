using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ModoOffIA : MonoBehaviour
{
    public List<int> CardsIA;
    public GameObject LocalMaoIA, LocalMesaIA;
    [SerializeField]
    bool _dangerDeath = false, encontrouCarta = false, acabouPerigo = false;
    [SerializeField]
    float atkPlayer, myDef, myLife, sumTimeLife;
    int numeroAchado;

    AvataresController _avatarControl;

    private void Start()
    {
        _avatarControl = GameObject.Find("AvatarControl").GetComponent<AvataresController>();
    }
    public void IAControll()
    {
        atkPlayer = float.Parse(_avatarControl.PlayerAvatar.transform.GetChild(1).
            GetComponent<TextMeshProUGUI>().text);
        myDef = float.Parse(_avatarControl.CpuAvatar.transform.GetChild(2).
            GetComponent<TextMeshProUGUI>().text);
        myLife = float.Parse(_avatarControl.CpuAvatar.transform.GetChild(3).GetChild(2).
            GetComponent<TextMeshProUGUI>().text);

        sumTimeLife = (atkPlayer * 2) - (myDef * 2);
        if(sumTimeLife > myLife)
        {
            _dangerDeath = true;
            //passos = 0;
        }

        if(!_dangerDeath || acabouPerigo)
        {
            int randomCardFirst = Random.Range(0, CardsIA.Count);
            var cardspawnFirst = LocalMaoIA.transform.GetChild(2).gameObject;
            cardspawnFirst.transform.SetParent(LocalMesaIA.transform, false);
            cardspawnFirst.transform.GetChild(1).gameObject.SetActive(true);
            cardspawnFirst.GetComponent<Image>().sprite = MainController.instance.
                    CardsAllGame[CardsIA[randomCardFirst]];
            if (CardsIA[randomCardFirst] != 0 && CardsIA[randomCardFirst] != 1)
                cardspawnFirst.transform.GetChild(0).gameObject.SetActive(false);
            cardspawnFirst.GetComponent<CardFunctionInterno>().NumberCard = CardsIA[randomCardFirst];
            Debug.Log(randomCardFirst);
            //CardsIA.Remove(randomCardFirst);
            CardsIA.RemoveAt(randomCardFirst);
        }
        else
        {
            for (int i = 0; i < CardsIA.Count; i++)
            {
                if (CardsIA[i] == 9)
                {
                    encontrouCarta = true;
                    numeroAchado = i;
                    continue;
                }
            }
            if (encontrouCarta)
            {
                var cardspawnFirst = LocalMaoIA.transform.GetChild(2).gameObject;
                cardspawnFirst.transform.SetParent(LocalMesaIA.transform, false);
                cardspawnFirst.transform.GetChild(1).gameObject.SetActive(true);
                cardspawnFirst.GetComponent<Image>().sprite = MainController.instance.CardsAllGame[8];
                cardspawnFirst.transform.GetChild(0).gameObject.SetActive(false);
                cardspawnFirst.GetComponent<CardFunctionInterno>().NumberCard = 8;
                Debug.Log(numeroAchado);
                CardsIA.RemoveAt(numeroAchado);
            }
            else
            {
                for (int i = 0; i < CardsIA.Count; i++)
                {
                    if (CardsIA[i] == 8)
                    {
                        encontrouCarta = true;
                        numeroAchado = i;
                        continue;
                    }
                }
                if (encontrouCarta)
                {
                    var cardspawnFirst = LocalMaoIA.transform.GetChild(2).gameObject;
                    cardspawnFirst.transform.SetParent(LocalMesaIA.transform, false);
                    cardspawnFirst.transform.GetChild(1).gameObject.SetActive(true);
                    cardspawnFirst.GetComponent<Image>().sprite = MainController.instance.CardsAllGame[9];
                    cardspawnFirst.transform.GetChild(0).gameObject.SetActive(false);
                    cardspawnFirst.GetComponent<CardFunctionInterno>().NumberCard = 9;
                    Debug.Log(numeroAchado);
                    CardsIA.RemoveAt(numeroAchado);
                }
                else
                {
                    for (int i = 0; i < CardsIA.Count; i++)
                    {
                        if (CardsIA[i] == 1)
                        {
                            encontrouCarta = true;
                            numeroAchado = i;
                            continue;
                        }
                    }
                    if (encontrouCarta)
                    {
                        var cardspawnFirst = LocalMaoIA.transform.GetChild(2).gameObject;
                        cardspawnFirst.transform.SetParent(LocalMesaIA.transform, false);
                        cardspawnFirst.transform.GetChild(1).gameObject.SetActive(true);
                        cardspawnFirst.GetComponent<Image>().sprite = MainController.instance.CardsAllGame[1];
                        cardspawnFirst.transform.GetChild(0).gameObject.SetActive(false);
                        cardspawnFirst.GetComponent<CardFunctionInterno>().NumberCard = 1;
                        Debug.Log(numeroAchado);
                        CardsIA.RemoveAt(numeroAchado);
                    }
                }
            }
            //var cardspawnFirst = LocalMaoIA.transform.GetChild(2).gameObject;
            //switch (passos)
            //{
            //    case 0:
            //        cardspawnFirst = LocalMaoIA.transform.GetChild(2).gameObject;
            //        cardspawnFirst.transform.SetParent(LocalMesaIA.transform, false);
            //        cardspawnFirst.transform.GetChild(1).gameObject.SetActive(true);
            //        cardspawnFirst.GetComponent<Image>().sprite = MainController.instance.CardsAllGame[8];
            //        cardspawnFirst.transform.GetChild(0).gameObject.SetActive(false);
            //        cardspawnFirst.GetComponent<CardFunctionInterno>().NumberCard = 8;
            //        break;
            //    case 1:
            //        cardspawnFirst = LocalMaoIA.transform.GetChild(2).gameObject;
            //        cardspawnFirst.transform.SetParent(LocalMesaIA.transform, false);
            //        cardspawnFirst.transform.GetChild(1).gameObject.SetActive(true);
            //        cardspawnFirst.GetComponent<Image>().sprite = MainController.instance.CardsAllGame[1];
            //        cardspawnFirst.transform.GetChild(0).gameObject.SetActive(false);
            //        cardspawnFirst.GetComponent<CardFunctionInterno>().NumberCard = 1;
            //        break;
            //    case 2:
            //        cardspawnFirst = LocalMaoIA.transform.GetChild(2).gameObject;
            //        cardspawnFirst.transform.SetParent(LocalMesaIA.transform, false);
            //        cardspawnFirst.transform.GetChild(1).gameObject.SetActive(true);
            //        cardspawnFirst.GetComponent<Image>().sprite = MainController.instance.CardsAllGame[1];
            //        cardspawnFirst.transform.GetChild(0).gameObject.SetActive(false);
            //        cardspawnFirst.GetComponent<CardFunctionInterno>().NumberCard = 1;
            //        break;
            //    case 3:
            //        cardspawnFirst = LocalMaoIA.transform.GetChild(2).gameObject;
            //        cardspawnFirst.transform.SetParent(LocalMesaIA.transform, false);
            //        cardspawnFirst.transform.GetChild(1).gameObject.SetActive(true);
            //        cardspawnFirst.GetComponent<Image>().sprite = MainController.instance.CardsAllGame[0];
            //        cardspawnFirst.transform.GetChild(0).gameObject.SetActive(false);
            //        cardspawnFirst.GetComponent<CardFunctionInterno>().NumberCard = 0;
            //        break;
            //    case 4:
            //        cardspawnFirst = LocalMaoIA.transform.GetChild(2).gameObject;
            //        cardspawnFirst.transform.SetParent(LocalMesaIA.transform, false);
            //        cardspawnFirst.transform.GetChild(1).gameObject.SetActive(true);
            //        cardspawnFirst.GetComponent<Image>().sprite = MainController.instance.CardsAllGame[0];
            //        cardspawnFirst.transform.GetChild(0).gameObject.SetActive(false);
            //        cardspawnFirst.GetComponent<CardFunctionInterno>().NumberCard = 0;
            //        break;
            //    case 5:
            //        cardspawnFirst = LocalMaoIA.transform.GetChild(2).gameObject;
            //        cardspawnFirst.transform.SetParent(LocalMesaIA.transform, false);
            //        cardspawnFirst.transform.GetChild(1).gameObject.SetActive(true);
            //        cardspawnFirst.GetComponent<Image>().sprite = MainController.instance.CardsAllGame[0];
            //        cardspawnFirst.transform.GetChild(0).gameObject.SetActive(false);
            //        cardspawnFirst.GetComponent<CardFunctionInterno>().NumberCard = 0;
            //        break;
            //    case 6:
            //        cardspawnFirst = LocalMaoIA.transform.GetChild(2).gameObject;
            //        cardspawnFirst.transform.SetParent(LocalMesaIA.transform, false);
            //        cardspawnFirst.transform.GetChild(1).gameObject.SetActive(true);
            //        cardspawnFirst.GetComponent<Image>().sprite = MainController.instance.CardsAllGame[2];
            //        cardspawnFirst.transform.GetChild(0).gameObject.SetActive(false);
            //        cardspawnFirst.GetComponent<CardFunctionInterno>().NumberCard = 2;
            //        break;
            //    case 7:
            //        cardspawnFirst = LocalMaoIA.transform.GetChild(2).gameObject;
            //        cardspawnFirst.transform.SetParent(LocalMesaIA.transform, false);
            //        cardspawnFirst.transform.GetChild(1).gameObject.SetActive(true);
            //        cardspawnFirst.GetComponent<Image>().sprite = MainController.instance.CardsAllGame[2];
            //        cardspawnFirst.transform.GetChild(0).gameObject.SetActive(false);
            //        cardspawnFirst.GetComponent<CardFunctionInterno>().NumberCard = 2;
            //        break;
            //    case 10:
            //        cardspawnFirst = LocalMaoIA.transform.GetChild(2).gameObject;
            //        cardspawnFirst.transform.SetParent(LocalMesaIA.transform, false);
            //        cardspawnFirst.transform.GetChild(1).gameObject.SetActive(true);
            //        cardspawnFirst.GetComponent<Image>().sprite = MainController.instance.CardsAllGame[2];
            //        cardspawnFirst.transform.GetChild(0).gameObject.SetActive(false);
            //        cardspawnFirst.GetComponent<CardFunctionInterno>().NumberCard = 2;
            //        break;
            //    default:
            //        cardspawnFirst = LocalMaoIA.transform.GetChild(2).gameObject;
            //        cardspawnFirst.transform.SetParent(LocalMesaIA.transform, false);
            //        cardspawnFirst.transform.GetChild(1).gameObject.SetActive(true);
            //        cardspawnFirst.GetComponent<Image>().sprite = MainController.instance.CardsAllGame[3];
            //        cardspawnFirst.transform.GetChild(0).gameObject.SetActive(false);
            //        cardspawnFirst.GetComponent<CardFunctionInterno>().NumberCard = 3;
            //        break;
            //}
            //passos++;
            acabouPerigo = true;
        }
    }
}
