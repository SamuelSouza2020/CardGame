using ControlGame;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CheckTable : MonoBehaviour
{
    private ModoOffIA _objCpu;
    private TypeCardManager _cardManager;
    private AvataresController _avataresController;
    private AnimControlOff _animControl;
    public bool CheckingGame = false;
    void Start()
    {
        _objCpu = GameObject.Find("IA").GetComponent<ModoOffIA>();
        _cardManager = GameObject.Find("TypeCardManager").GetComponent<TypeCardManager>();
        _avataresController = GameObject.Find("AvatarControl").GetComponent<AvataresController>();
        _animControl = GameObject.Find("AnimationControl").GetComponent<AnimControlOff>();
        CheckingGame = false;
        StartCoroutine(IniciarGame());
    }

    // Update is called once per frame
    void Update()
    {
        if (_objCpu.LocalMesaIA.transform.childCount > 0 &&
            MainController.instance.LocalMesaP.transform.childCount > 0 && !CheckingGame)
        {
            _objCpu.LocalMesaIA.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);


            int numberCardPlayer = MainController.instance.LocalMesaP.transform.
                GetChild(0).GetComponent<CardFunctionInterno>().NumberCard;
            int numberCardCpu = _objCpu.LocalMesaIA.transform.GetChild(0).
                GetComponent<CardFunctionInterno>().NumberCard;

            if(!(numberCardPlayer == 2 && numberCardCpu == 6) && !(numberCardPlayer == 6 && numberCardCpu == 2)
                && !(numberCardPlayer == 3 && numberCardCpu == 5) && !(numberCardPlayer == 5 && numberCardCpu == 3))
            {
                //Verifica se a carta Player nao modifica os atributos da IA
                if (!(numberCardPlayer == 5) && !(numberCardPlayer == 6)
                    && !(numberCardPlayer == 8) && !(numberCardPlayer == 9))
                {
                    _cardManager.TypeController(numberCardPlayer,
                        _avataresController.PlayerAvatar, 10);
                }
                //Verifica se a carta IA nao modifica os atributos do Player
                if (!(numberCardCpu == 5) && !(numberCardCpu == 6)
                    && !(numberCardCpu == 8) && !(numberCardCpu == 9))
                {
                    _cardManager.TypeController(numberCardCpu,
                        _avataresController.CpuAvatar, 10);
                }
            }

            if (numberCardPlayer == 8 || numberCardPlayer == 9)
            {
                _cardManager.TypeController(numberCardPlayer,
                    _avataresController.PlayerAvatar, 0);
            }
            if (numberCardCpu == 8 || numberCardCpu == 9)
            {
                _cardManager.TypeController(numberCardCpu,
                    _avataresController.CpuAvatar, 0);
            }

            if(!(numberCardPlayer == 2 && numberCardCpu == 6) && !(numberCardPlayer == 6 && numberCardCpu == 2)
                && !(numberCardPlayer == 3 && numberCardCpu == 5) && !(numberCardPlayer == 5 && numberCardCpu == 3))
            {
                if (numberCardPlayer == 5 || numberCardPlayer == 6)
                {
                    _cardManager.TypeController(numberCardPlayer,
                        _avataresController.CpuAvatar, 0);
                }
                if (numberCardCpu == 5 || numberCardCpu == 6)
                {
                    _cardManager.TypeController(numberCardCpu,
                        _avataresController.PlayerAvatar, 0);
                }
            }

            //Valor inimigo para comparar
            float atkInimigo = float.Parse(_avataresController.CpuAvatar.transform.GetChild(1).
                GetComponent<TextMeshProUGUI>().text);
            float defInimigo = float.Parse(_avataresController.CpuAvatar.transform.GetChild(2).
                GetComponent<TextMeshProUGUI>().text);
            float lifeInimigo = float.Parse(_avataresController.CpuAvatar.transform.GetChild(3).
                GetChild(2).GetComponent<TextMeshProUGUI>().text);
            //Valor player para comparar
            float atkPlayer = float.Parse(_avataresController.PlayerAvatar.transform.GetChild(1).
                GetComponent<TextMeshProUGUI>().text);
            float defPlayer = float.Parse(_avataresController.PlayerAvatar.transform.GetChild(2).
                GetComponent<TextMeshProUGUI>().text);
            float lifePlayer = float.Parse(_avataresController.PlayerAvatar.transform.GetChild(3).
                GetChild(2).GetComponent<TextMeshProUGUI>().text);

            //Calculo do dano Player
            if (atkPlayer > defInimigo)
                lifeInimigo -= atkPlayer - defInimigo;
            //Calculo do dano Inimigo
            if (atkInimigo > defPlayer)
                lifePlayer -= atkInimigo - defPlayer;

            //Animações e calculos visuais
            _animControl.CallAnimationAkDf(lifePlayer, lifeInimigo);
            _animControl.CallAnimationRound();
            CheckingGame = true;
        }
    }
    IEnumerator IniciarGame()
    {
        yield return new WaitForSeconds(1);
        _objCpu.IAControll();
    }
}
