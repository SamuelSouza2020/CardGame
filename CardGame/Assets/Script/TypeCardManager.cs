using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TypeCardManager : MonoBehaviour
{
    private AvataresController avataresControll;
    private ModoOffIA _controllIA;

    private void Start()
    {
        avataresControll = GameObject.Find("AvatarControl").GetComponent<AvataresController>();
        _controllIA = GameObject.Find("IA").GetComponent<ModoOffIA>();
    }
    public void TypeController(int value, GameObject quemJogou, int numberRandom)
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
            case 7:
                //int contMaoP = MainController.instance.LocalMaoP.transform.childCount;
                //int contMaoIA = _controllIA.LocalMaoIA.transform.childCount;
                //for (int i = 0; i < 4; i++)
                //{
                //    Debug.Log(MainController.instance.LocalMaoP.transform.childCount);
                //    Destroy(MainController.instance.LocalMaoP.transform.GetChild(0).gameObject);
                //    Destroy(_controllIA.LocalMaoIA.transform.GetChild(0).gameObject);
                //}
                MainController.instance.DeckPlayerGame.Add(MainController.instance.LocalMaoP.
                    transform.GetChild(0).GetComponent<CardFunctionInterno>().NumberCard);
                MainController.instance.DeckPlayerGame.Add(MainController.instance.LocalMaoP.
                    transform.GetChild(1).GetComponent<CardFunctionInterno>().NumberCard);
                MainController.instance.DeckPlayerGame.Add(MainController.instance.LocalMaoP.
                    transform.GetChild(2).GetComponent<CardFunctionInterno>().NumberCard);
                MainController.instance.DeckPlayerGame.Add(MainController.instance.LocalMaoP.
                    transform.GetChild(3).GetComponent<CardFunctionInterno>().NumberCard);
                Destroy(MainController.instance.LocalMaoP.transform.GetChild(0).gameObject);
                Destroy(MainController.instance.LocalMaoP.transform.GetChild(1).gameObject);
                Destroy(MainController.instance.LocalMaoP.transform.GetChild(2).gameObject);
                Destroy(MainController.instance.LocalMaoP.transform.GetChild(3).gameObject);

                //_controllIA.CardsIA.Add(_controllIA.LocalMaoIA.transform.GetChild(0).
                //    GetComponent<CardFunctionInterno>().NumberCard);
                Destroy(_controllIA.LocalMaoIA.transform.GetChild(0).gameObject);
                Destroy(_controllIA.LocalMaoIA.transform.GetChild(1).gameObject);
                Destroy(_controllIA.LocalMaoIA.transform.GetChild(2).gameObject);
                Destroy(_controllIA.LocalMaoIA.transform.GetChild(3).gameObject);
                break;
            //Reseta Os ataques e defesas
            case 8:
                avataresControll.PlayerAvatar.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = avataresControll.AtkPlayer.ToString();
                avataresControll.PlayerAvatar.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = avataresControll.DefPlayer.ToString();
                avataresControll.CpuAvatar.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = avataresControll.AtkCpu.ToString();
                avataresControll.CpuAvatar.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = avataresControll.DefCpu.ToString();
                break;
            //Trocar o ataque e defesa de ambos os jogadores
            case 9:
                float atkPlayer = float.Parse(avataresControll.PlayerAvatar.transform.GetChild(1).GetComponent
                    <TextMeshProUGUI>().text);
                avataresControll.PlayerAvatar.transform.GetChild(1).GetComponent<TextMeshProUGUI>().
                    text = avataresControll.PlayerAvatar.transform.GetChild(2).GetComponent <TextMeshProUGUI>().text;
                avataresControll.PlayerAvatar.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = atkPlayer.ToString();
                float atkCpu = float.Parse(avataresControll.CpuAvatar.transform.GetChild(1).GetComponent
                    <TextMeshProUGUI>().text);
                avataresControll.CpuAvatar.transform.GetChild(1).GetComponent<TextMeshProUGUI>().
                    text = avataresControll.CpuAvatar.transform.GetChild(2).GetComponent <TextMeshProUGUI>().text;
                avataresControll.CpuAvatar.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = atkCpu.ToString();
                break;
        }
    }
}
