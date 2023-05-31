using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnimControlOff : MonoBehaviour
{
    private AvataresController avataresControll;
    private ModoOffIA _controllIA;
    private CardsManager _cardsManager;
    private CheckTable _checkTable;
    private void Start()
    {
        avataresControll = GameObject.Find("AvatarControl").GetComponent<AvataresController>();
        _controllIA = GameObject.Find("IA").GetComponent<ModoOffIA>();
        _cardsManager = GameObject.Find("CardManager").GetComponent<CardsManager>();
        _checkTable = GameObject.Find("CheckTable").GetComponent<CheckTable>();
    }
    public void CallAnimationAkDf(float lifePlayer, float lifeInimigo)
    {
        StartCoroutine(AnimAtaqueDefesa(lifePlayer, lifeInimigo));
    }
    public void CallAnimationRound()
    {
        StartCoroutine(AnimRound());
    }
    IEnumerator AnimAtaqueDefesa(float lifePlayer, float lifeInimigo)
    {
        ////Aqui os ataques vao ao mesmo tempo
        yield return new WaitForSeconds(0.1f);
        avataresControll.PlayerAvatar.transform.GetChild(0).GetComponent<Animator>().Play("PlayerFight");
        avataresControll.CpuAvatar.transform.GetChild(0).GetComponent<Animator>().Play("InimFight");
        float vidaAtualIni = float.Parse(avataresControll.CpuAvatar.transform.GetChild(3).
            GetChild(2).GetComponent<TextMeshProUGUI>().text);
        float vidaAtualP = float.Parse(avataresControll.PlayerAvatar.transform.GetChild(3).
            GetChild(2).GetComponent<TextMeshProUGUI>().text);
        yield return new WaitForSeconds(0.6f);
        avataresControll.PlayerAvatar.transform.GetChild(7).gameObject.SetActive(true);
        avataresControll.CpuAvatar.transform.GetChild(7).gameObject.SetActive(true);
        //Comparar as vidas atuais com o calculo
        if (vidaAtualP > lifePlayer)
            avataresControll.PlayerAvatar.transform.GetChild(0).GetComponent<Animator>().Play("PlayerFail");
        else
        {
            avataresControll.PlayerAvatar.transform.GetChild(6).gameObject.SetActive(true);
            avataresControll.PlayerAvatar.transform.GetChild(0).GetComponent<Animator>().Play("PlayerDefense");
        }

        if (vidaAtualIni > lifeInimigo)
            avataresControll.CpuAvatar.transform.GetChild(0).GetComponent<Animator>().Play("IniFail");
        else
        {
            avataresControll.CpuAvatar.transform.GetChild(6).gameObject.SetActive(true);
            avataresControll.CpuAvatar.transform.GetChild(0).GetComponent<Animator>().Play("IniDefense");
        }

        yield return new WaitForSeconds(1.5f);
        avataresControll.PlayerAvatar.transform.GetChild(0).GetComponent<Animator>().Play("New State");
        avataresControll.CpuAvatar.transform.GetChild(0).GetComponent<Animator>().Play("New State");

        avataresControll.PlayerAvatar.transform.GetChild(7).gameObject.SetActive(false);//Desativar hadouken
        avataresControll.CpuAvatar.transform.GetChild(7).gameObject.SetActive(false);//Desativar hadouken
        avataresControll.PlayerAvatar.transform.GetChild(6).gameObject.SetActive(false);//Desativar hadouken
        avataresControll.CpuAvatar.transform.GetChild(6).gameObject.SetActive(false);//Desativar hadouken
        avataresControll.CpuAvatar.transform.GetChild(3).GetChild(2).
            GetComponent<TextMeshProUGUI>().text = lifeInimigo.ToString("0.0");
        avataresControll.PlayerAvatar.transform.GetChild(3).GetChild(2).
            GetComponent<TextMeshProUGUI>().text = lifePlayer.ToString("0.0");
        //Vida Player mostrada no text e image
        if (lifePlayer > 100)
        {
            avataresControll.PlayerAvatar.transform.GetChild(3).GetChild(1).gameObject.SetActive(true);
            avataresControll.PlayerAvatar.transform.GetChild(3).GetChild(1).
                GetComponent<Image>().fillAmount = (lifePlayer - 100) / 100;
            avataresControll.PlayerAvatar.transform.GetChild(3).GetChild(0).
                GetComponent<Image>().fillAmount = 1;

        }
        else if (lifePlayer < 100)
        {
            avataresControll.PlayerAvatar.transform.GetChild(3).GetChild(1).gameObject.SetActive(false);
            avataresControll.PlayerAvatar.transform.GetChild(3).GetChild(4).
                GetComponent<TextMeshProUGUI>().text = "100";
            avataresControll.PlayerAvatar.transform.GetChild(3).GetChild(0).
                GetComponent<Image>().fillAmount = lifePlayer / 100;
        }
        else
        {
            avataresControll.PlayerAvatar.transform.GetChild(3).GetChild(1).gameObject.SetActive(false);
            avataresControll.PlayerAvatar.transform.GetChild(3).GetChild(4).GetComponent<TextMeshProUGUI>().text = "100";
            avataresControll.PlayerAvatar.transform.GetChild(3).GetChild(0).GetComponent<Image>().fillAmount = 1;
        }
        //Vida Inimigo mostrada no text e image
        if (lifeInimigo > 100)
        {
            avataresControll.CpuAvatar.transform.GetChild(3).GetChild(1).gameObject.SetActive(true);
            avataresControll.CpuAvatar.transform.GetChild(3).GetChild(1).
                GetComponent<Image>().fillAmount = (lifeInimigo - 100) / 100;
            avataresControll.CpuAvatar.transform.GetChild(3).GetChild(0).GetComponent<Image>().fillAmount = 1;
        }
        else if (lifeInimigo < 100)
        {
            avataresControll.CpuAvatar.transform.GetChild(3).GetChild(1).gameObject.SetActive(false);
            avataresControll.CpuAvatar.transform.GetChild(3).GetChild(4).GetComponent<TextMeshProUGUI>().text = "100";
            avataresControll.CpuAvatar.transform.GetChild(3).GetChild(0).GetComponent<Image>().fillAmount = lifeInimigo / 100;
        }
        else
        {
            avataresControll.CpuAvatar.transform.GetChild(3).GetChild(1).gameObject.SetActive(false);
            avataresControll.CpuAvatar.transform.GetChild(3).GetChild(4).GetComponent<TextMeshProUGUI>().text = "100";
            avataresControll.CpuAvatar.transform.GetChild(3).GetChild(0).GetComponent<Image>().fillAmount = 1;
        }
    }
    IEnumerator AnimRound()
    {
        //Aqui vamos limpar as mesas para o proximo round
        yield return new WaitForSeconds(4f);
        avataresControll.PlayerAvatar.transform.GetChild(4).gameObject.SetActive(false);
        avataresControll.CpuAvatar.transform.GetChild(4).gameObject.SetActive(false);
        avataresControll.PlayerAvatar.transform.GetChild(7).gameObject.SetActive(false);
        avataresControll.CpuAvatar.transform.GetChild(7).gameObject.SetActive(false);
        int qtdMesaPlayer = MainController.instance.LocalMesaP.transform.childCount;
        for (int i = 0; i < qtdMesaPlayer; i++)
        {
            MainController.instance.LocalMesaP.transform.GetChild(i).gameObject.SetActive(false);
            _controllIA.LocalMesaIA.transform.GetChild(i).gameObject.SetActive(false);
        }

        //GameManager.instance.GeradorDeCartas.InstCardForGame = 1;
        //GameManager.instance.GeradorDeCartas.GerarCarta();
        //GameManager.instance.ResultadoGame = true;
        //GameManager.instance.AcabouRound = false;

        float vidaAtualIni = float.Parse(avataresControll.CpuAvatar.
            transform.GetChild(3).GetChild(2).GetComponent<TextMeshProUGUI>().text);
        float vidaAtualP = float.Parse(avataresControll.PlayerAvatar.
            transform.GetChild(3).GetChild(2).GetComponent<TextMeshProUGUI>().text);
        if (vidaAtualIni <= 0 || vidaAtualP <= 0)
        {
            for (int i = 1; i <= 5; i++)
            {
                if (i != 3)
                {
                    avataresControll.PlayerAvatar.transform.GetChild(i).gameObject.SetActive(false);
                    avataresControll.CpuAvatar.transform.GetChild(i).gameObject.SetActive(false);
                }
            }
            //GameManager.instance.AcabouJogo = true;
            if (vidaAtualP <= 0)
                avataresControll.PlayerAvatar.transform.GetChild(0).GetComponent<Animator>().Play("PlayerDead");
            else
                avataresControll.CpuAvatar.transform.GetChild(0).GetComponent<Animator>().Play("InimDead");

            //Bloquear Cartas
            //for (int i = 0; i < MainController.instance.LocalMaoP.transform.childCount; i++)
            //{
            //    MainController.instance.LocalMaoP.transform.GetChild(i).
            //        GetComponent<Button>().interactable = false;
            //}
            //for (int i = 0; i < GameManager.instance.LocalCard.transform.childCount; i++)
            //{
            //    GameManager.instance.LocalCardIni.transform.GetChild(i).
            //        GetComponent<Button>().interactable = false;
            //}
            MainController.instance.GameOver.gameObject.SetActive(true);
            if (vidaAtualP > 0)
                MainController.instance.GameOver.text = "VENCEU";
            else
                MainController.instance.GameOver.text = "PERDEU";
        }
        //GameManager.instance.TwoPlayers = 0;
        //GameManager.instance.PlayerJogou = false;
        //Destruir os dois objetos
        Destroy(MainController.instance.LocalMesaP.transform.GetChild(0).gameObject);
        Destroy(_controllIA.LocalMesaIA.transform.GetChild(0).gameObject);
        MainController.instance.WaitTurn = false;
        int totalMao = MainController.instance.LocalMaoP.transform.childCount;
        int subTotal = 5 - totalMao;
        _cardsManager.InstCardForGame = subTotal;
        if (vidaAtualIni > 0 && vidaAtualP > 0)
        {
            _cardsManager.GerarCarta();
            _controllIA.IAControll();
            _checkTable.CheckingGame = false;
        }
    }
}
