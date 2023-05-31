using ControlGame;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviourPunCallbacks
{
    public GameObject CardPrefab, LocalCard, MesaJ1, LocalCardIni, MesaIni,
        CardPlayerJ, CardInimigoJ;
    public int VitoriasP, CountRoundGame;
    public float AtkRoundPlayer, AtkRoundInimigo, DefRoundPlayer, DefRoundInimigo;
    public List<GameObject> CardsGeration, CardsGerationIni;
    public List<int> DeckPlayer, DeckInimigo, RepresentaDeck, DeckSub1, DeckSub2, DeckIA;
    //public List<int> JogadaPlayer, JogadaInimigo;
    public int JogadaPlayer, JogadaInimigo;
    //Verificar qual jogador é o aparelho
    public int TwoPlayers;
    public Sprite CardAtk, CardDef, CardVerso;
    public List<Sprite> TypeCard;
    public bool VezInimigo, AcabouRound, ResultadoGame, 
        AcabouJogo, InimigoJogou, AcabouTempo, Aguardando;
    public TextMeshProUGUI Resultado, GameOver;
    public Button BtGerador;

    //Scripts
    public Card GeradorDeCartas;
    public TimeGame TempoDeJogar;
    public TypeCardManager TypeManager;

    //Teste dos Valores dos Jogadores
    public float AtkP1, AtkP2, DefP1, DefP2, LifeP1, LifeP2;

    [SerializeField]
    AnimationControl _animControl;

    //teste online
    public bool PlayerJogou = false, Online;

    //Teste de verificar Erro
    public GameObject ErrorGame, VerificarValores;


    public static GameManager instance;

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
        Online = true;
    }
    void Start()
    {
        TempoDeJogar.enabled = false;
        TempoDeJogar.gameObject.SetActive(false);
        AcabouTempo = false;
        ResultadoGame = true;
        AcabouRound = false;
        VitoriasP = 0;
        AcabouJogo = false;
        CountRoundGame = 0;
        VezInimigo = false;
        InimigoJogou = false;
        Aguardando = false;

        int contCard = 0, numberLoop = TypeCard.Count * 5;
        
        for (int i = 0; i < numberLoop; i++)
        {
            if(contCard > TypeCard.Count-1)
            {
                contCard=0;
            }
            RepresentaDeck.Add(contCard);
            contCard++;
        }
        for (int i = 0; i < numberLoop; i++)
        {
            int cartaSorteada = UnityEngine.Random.Range(0, RepresentaDeck.Count);
            DeckPlayer.Add(RepresentaDeck[cartaSorteada]);
            RepresentaDeck.RemoveAt(cartaSorteada);
        }
        for (int i = 0; i < numberLoop; i++)
        {
            if (contCard > TypeCard.Count - 1)
            {
                contCard = 0;
            }
            RepresentaDeck.Add(contCard);
            contCard++;
        }
        for (int i = 0; i < numberLoop; i++)
        {
            int cartaSorteada = UnityEngine.Random.Range(0, RepresentaDeck.Count);
            DeckInimigo.Add(RepresentaDeck[cartaSorteada]);
            RepresentaDeck.RemoveAt(cartaSorteada);
        }
        StartCoroutine(StartGame());
    }
    void Update()
    {
        if (AcabouTempo)
        {
            Online = false;
            IAInimigo.instance.SimularPlayer2();
            AcabouTempo = false;
        }
        if ((TwoPlayers == 1 && !PlayerJogou) && (!base.photonView.IsMine || !Online))
        {
            if(!Online && CardPlayerJ.gameObject.name == "Inimigo")
            {
                TestViewCard.instance.CartaJogadaPlayer1 = TestViewCard.instance.CartaJogada;
                for (int i = 0; i < LocalCard.transform.childCount; i++)
                {
                    LocalCard.transform.GetChild(i).GetComponent
                        <Button>().interactable = true;
                }
                PlayerJogou = true;
                StartCoroutine(TimeTheInvokeCard());
            }
            else if(Online)
            {
                TestViewCard.instance.CartaJogadaPlayer1 = TestViewCard.instance.CartaJogada;
                for (int i = 0; i < LocalCard.transform.childCount; i++)
                {
                    LocalCard.transform.GetChild(i).GetComponent
                        <Button>().interactable = true;
                }
                PlayerJogou = true;
                StartCoroutine(TimeTheInvokeCard());
            }
        }
        if ((TwoPlayers == 1 && Aguardando) && (base.photonView.IsMine || !Online))
        {
            TestViewCard.instance.CartaJogadaPlayer1 = TestViewCard.instance.CartaJogada;
            for (int i = 0; i < LocalCard.transform.childCount; i++)
            {
                LocalCard.transform.GetChild(i).GetComponent
                    <Button>().interactable = false;
            }
            Aguardando = false;
        }
        if ((TwoPlayers == 2 && !PlayerJogou) && (base.photonView.IsMine || !Online))
        {
            if (!Online && CardPlayerJ.gameObject.name == "Player")
            {
                TestViewCard.instance.CartaJogadaPlayer2 = TestViewCard.instance.CartaJogada;
                for (int i = 0; i < LocalCard.transform.childCount; i++)
                {
                    LocalCard.transform.GetChild(i).GetComponent
                        <Button>().interactable = true;
                }
                PlayerJogou = true;
                StartCoroutine(TimeTheInvokeCard());
            }
            else if(Online)
            {
                TestViewCard.instance.CartaJogadaPlayer2 = TestViewCard.instance.CartaJogada;
                for (int i = 0; i < LocalCard.transform.childCount; i++)
                {
                    LocalCard.transform.GetChild(i).GetComponent
                        <Button>().interactable = true;
                }
                PlayerJogou = true;
                StartCoroutine(TimeTheInvokeCard());
                StartCoroutine(TimeEndGame());
            }
        }
        if ((TwoPlayers == 2 && Aguardando) && (!base.photonView.IsMine || !Online))
        {
            TestViewCard.instance.CartaJogadaPlayer2 = TestViewCard.instance.CartaJogada;
            for (int i = 0; i < LocalCard.transform.childCount; i++)
            {
                LocalCard.transform.GetChild(i).GetComponent
                    <Button>().interactable = false;
            }
            Aguardando = false;
            Debug.Log("EntrouAG");
            StartCoroutine(TimeEndGame());
        }
        if (!AcabouJogo)
        {
            if (AcabouRound && ResultadoGame && TwoPlayers == 2)
            {
                //Contar tempo da jogada
                if(Online)
                {
                    TempoDeJogar.TimeReset(20);
                    TempoDeJogar.enabled = false;
                    TempoDeJogar.gameObject.SetActive(false);
                }

                //Valor inimigo para comparar
                float atkInimigo = float.Parse(CardInimigoJ.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text);
                float defInimigo = float.Parse(CardInimigoJ.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text);
                float lifeInimigo = float.Parse(CardInimigoJ.transform.GetChild(3).
                    GetChild(2).GetComponent<TextMeshProUGUI>().text);
                //Valor player para comparar
                float atkPlayer = float.Parse(CardPlayerJ.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text);
                float defPlayer = float.Parse(CardPlayerJ.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text);
                float lifePlayer = float.Parse(CardPlayerJ.transform.GetChild(3).
                    GetChild(2).GetComponent<TextMeshProUGUI>().text);

                //Calculo do dano Player
                if (atkPlayer > defInimigo)
                    lifeInimigo -= atkPlayer - defInimigo;
                //Calculo do dano Inimigo
                if (atkInimigo > defPlayer)
                    lifePlayer -= atkInimigo - defPlayer;

                //Desativar as cartas no periodo da animação
                foreach (var cards in CardsGeration)
                    cards.GetComponent<Button>().interactable = false;
                foreach (var cards in CardsGerationIni)
                    cards.GetComponent<Button>().interactable = false;

                //Animações e calculos visuais
                _animControl.CallAnimationAkDf(lifePlayer, lifeInimigo);
                _animControl.CallAnimationRound();
                StartCoroutine(NewRound());
                ResultadoGame = false;
                CountRoundGame++;
                TwoPlayers = 0;
                PlayerJogou = false;
            }
        }
    }
    IEnumerator NewRound()
    {
        //Aqui vamos limpar as mesas para o proximo round
        yield return new WaitForSeconds(4.2f);
        if (base.photonView.IsMine || !Online)
        {
            foreach (var cards in CardsGeration)
            {
                cards.GetComponent<Button>().interactable = true;
            }
        }
    }
    IEnumerator StartGame()
    {
        //Iniciar a instanciar as cartas
        yield return new WaitForSeconds(1f);
        GeradorDeCartas.enabled = true;

        yield return new WaitForSeconds(1.5f);
        //IAInimigo.instance.VerificarPlayer();
        if (base.photonView.IsMine || !Online)
        {
            for (int i = 0; i < LocalCard.transform.childCount; i++)
            {
                LocalCard.transform.GetChild(i).GetComponent
                    <Button>().interactable = true;
            }
        }
    }
    IEnumerator TimeTheInvokeCard()
    {
        //Iniciar a instanciar as cartas
        yield return new WaitForSeconds(0.2f);
        GeradorDeCartas.InvokeCard();
    }
    IEnumerator TimeEndGame()
    {
        yield return new WaitForSeconds(0.5f);
        if (TwoPlayers == 2)
        {
            MesaIni.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
            int card1 = 0;
            int card2 = 0;
            if(Online)
            {
                card1 = TestViewCard.instance.CartaJogadaPlayer1;
                card2 = TestViewCard.instance.CartaJogadaPlayer2;
            }
            else
            {
                if (CardPlayerJ.gameObject.name == "Player")
                {
                    card1 = MesaJ1.transform.GetChild(0).GetComponent<TesteInterno>().numberRandom;
                    card2 = MesaIni.transform.GetChild(0).GetComponent<TesteInterno>().numberRandom;
                }
                if (CardPlayerJ.gameObject.name == "Inimigo")
                {
                    card1 = MesaJ1.transform.GetChild(0).GetComponent<TesteInterno>().numberRandom;
                    card2 = MesaIni.transform.GetChild(0).GetComponent<TesteInterno>().numberRandom;
                }
            }

            //Verificar se uma carta anula a outra
            if (!(card1 == 2 && card2 == 6 || card1 == 6 && card2 == 2) &&
                !(card1 == 3 && card2 == 5 || card1 == 5 && card2 == 3))
            {
                if(Online)
                {
                    if (base.photonView.IsMine)
                    {
                        //Chamar a função das cartas
                        if (TestViewCard.instance.CartaJogadaPlayer1 != 5 && TestViewCard.instance.CartaJogadaPlayer1 != 6)
                            MesaJ1.transform.GetChild(0).GetComponent<TesteInterno>().
                                CallTypeCard(TestViewCard.instance.CartaJogadaPlayer1, GameManager.instance.CardPlayerJ);
                        else
                            MesaJ1.transform.GetChild(0).GetComponent<TesteInterno>().
                                CallTypeCard(TestViewCard.instance.CartaJogadaPlayer1, GameManager.instance.CardInimigoJ);

                        if (TestViewCard.instance.CartaJogadaPlayer2 != 5 && TestViewCard.instance.CartaJogadaPlayer2 != 6)
                            MesaIni.transform.GetChild(0).GetComponent<TesteInterno>().
                                CallTypeCard(TestViewCard.instance.CartaJogadaPlayer2, GameManager.instance.CardInimigoJ);
                        else
                            MesaIni.transform.GetChild(0).GetComponent<TesteInterno>().
                                CallTypeCard(TestViewCard.instance.CartaJogadaPlayer2, GameManager.instance.CardPlayerJ);

                        //Verificar se elas tem text
                        if (card1 == 0 || card1 == 1)
                        {
                            MesaJ1.transform.GetChild(0).GetChild(0).
                                GetComponent<TextMeshProUGUI>().enabled = true;
                            MesaJ1.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                        }
                        if (card2 == 0 || card2 == 1)
                        {
                            MesaIni.transform.GetChild(0).GetChild(0).
                                GetComponent<TextMeshProUGUI>().enabled = true;
                            MesaIni.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                        }

                        //Teste de passar valores p segundo jogador
                        AtkP1 = Single.Parse(CardPlayerJ.transform.
                            GetChild(1).GetComponent<TextMeshProUGUI>().text);
                        AtkP2 = Single.Parse(CardInimigoJ.transform.
                            GetChild(1).GetComponent<TextMeshProUGUI>().text);
                        DefP1 = Single.Parse(CardPlayerJ.transform.
                            GetChild(2).GetComponent<TextMeshProUGUI>().text);
                        DefP2 = Single.Parse(CardInimigoJ.transform.
                            GetChild(2).GetComponent<TextMeshProUGUI>().text);
                        LifeP1 = Single.Parse(CardPlayerJ.transform.
                            GetChild(3).GetChild(2).GetComponent<TextMeshProUGUI>().text);
                        LifeP2 = Single.Parse(CardInimigoJ.transform.
                            GetChild(3).GetChild(2).GetComponent<TextMeshProUGUI>().text);
                    }
                    if (!base.photonView.IsMine)
                    {
                        //Verificar se elas tem text
                        if (card1 == 0 || card1 == 1)
                        {
                            MesaIni.transform.GetChild(0).GetChild(0).GetComponent
                                <TextMeshProUGUI>().enabled = true;
                            MesaIni.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                        }
                        if (card2 == 0 || card2 == 1)
                        {
                            MesaJ1.transform.GetChild(0).GetChild(0).GetComponent
                                <TextMeshProUGUI>().enabled = true;
                            MesaJ1.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                        }
                    }
                }
                else
                {
                    Debug.Log(MesaJ1.transform.GetChild(0).GetChild(0).GetComponent
                                <TextMeshProUGUI>().text);
                    int cardP = Convert.ToInt32(MesaJ1.transform.GetChild(0).GetChild(0).
                        GetComponent<TextMeshProUGUI>().text);
                    int cardI = Convert.ToInt32(MesaIni.transform.GetChild(0).GetChild(0).GetComponent
                                <TextMeshProUGUI>().text);
                    IAInimigo.instance.ResultGameIA(cardP, cardI);
                }
            }
            if(Online)
                TestViewCard.instance.UpdateValueP2();
        }
        yield return new WaitForSeconds(1f);
        AcabouJogo = false;
        AcabouRound = true;
        ResultadoGame = true;
    }
}
