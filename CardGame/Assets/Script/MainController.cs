using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainController : MonoBehaviour
{
    public List<int> DeckPlayerOfficial, CardsPlayerOfficial, AmountCard, DeckPlayerGame;
    public List<Sprite> CardsAllGame;
    public GameObject CardGameOFF, LocalMaoP, LocalMesaP;
    public bool WaitTurn = false;
    public TextMeshProUGUI GameOver; 
    InventaryManager _inventaryManager;

    public static MainController instance;

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
        SceneManager.sceneLoaded += Carrega;
    }
    void Start()
    {
        
    }
    void Carrega(Scene cena, LoadSceneMode modo)
    {
        if(OndeEstou.instance.fase == 2)
        {
            LocalMaoP = GameObject.Find("Mao");
            LocalMesaP = GameObject.Find("Mesa");
            GameOver = GameObject.Find("GameOver").GetComponent<TextMeshProUGUI>();
            GameOver.gameObject.SetActive(false);

            int sum = 0, countCard = 0;
            for (int i = 0; i < DeckPlayerOfficial.Count; i++)
            {
                sum += AmountCard[DeckPlayerOfficial[i]];
            }
            for (int i = 0; i < sum; i++)
            {
                DeckPlayerGame.Add(0);
            }

            for (int i = 0; i < DeckPlayerGame.Count; i++)
            {
                if (AmountCard[DeckPlayerOfficial[countCard]] > 0)
                {
                    DeckPlayerGame[i] = DeckPlayerOfficial[countCard];
                }
                else
                {
                    countCard++;
                    DeckPlayerGame[i] = DeckPlayerOfficial[countCard];
                }
                AmountCard[DeckPlayerOfficial[countCard]]--;
            }
        }
        if(OndeEstou.instance.fase == 3)
        {
            _inventaryManager = GameObject.Find("InventaryManager").GetComponent<InventaryManager>();
            _inventaryManager.InstantiateCardsPlayer();
        }
        if(OndeEstou.instance.fase == 4)
        {
            GameObject.Find("OffBt").GetComponent<Button>().onClick.AddListener(IrFaseOff);
            GameObject.Find("InventaryBt").GetComponent<Button>().onClick.AddListener(IrFaseInvenary);
        }
    }
    public void IrFaseInvenary()
    {
        SceneManager.LoadScene(3);
    }
    public void IrFaseOff()
    {
        SceneManager.LoadScene(2);
    }
}
