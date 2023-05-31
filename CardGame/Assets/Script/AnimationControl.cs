using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ControlGame
{
    public class AnimationControl : MonoBehaviour
    {
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
            GameManager.instance.CardPlayerJ.transform.GetChild(0).GetComponent<Animator>().Play("PlayerFight");
            GameManager.instance.CardInimigoJ.transform.GetChild(0).GetComponent<Animator>().Play("InimFight");
            float vidaAtualIni = float.Parse(GameManager.instance.CardInimigoJ.transform.GetChild(3).
                GetChild(2).GetComponent<TextMeshProUGUI>().text);
            float vidaAtualP = float.Parse(GameManager.instance.CardPlayerJ.transform.GetChild(3).
                GetChild(2).GetComponent<TextMeshProUGUI>().text);
            yield return new WaitForSeconds(0.6f);
            GameManager.instance.CardInimigoJ.transform.GetChild(7).gameObject.SetActive(true);
            GameManager.instance.CardPlayerJ.transform.GetChild(7).gameObject.SetActive(true);
            //Comparar as vidas atuais com o calculo
            if (vidaAtualP > lifePlayer)
                GameManager.instance.CardPlayerJ.transform.GetChild(0).GetComponent<Animator>().Play("PlayerFail");
            else
            {
                GameManager.instance.CardPlayerJ.transform.GetChild(6).gameObject.SetActive(true);
                GameManager.instance.CardPlayerJ.transform.GetChild(0).GetComponent<Animator>().Play("PlayerDefense");
            }

            if (vidaAtualIni > lifeInimigo)
                GameManager.instance.CardInimigoJ.transform.GetChild(0).GetComponent<Animator>().Play("IniFail");
            else
            {
                GameManager.instance.CardInimigoJ.transform.GetChild(6).gameObject.SetActive(true);
                GameManager.instance.CardInimigoJ.transform.GetChild(0).GetComponent<Animator>().Play("IniDefense");
            }

            yield return new WaitForSeconds(1.5f);
            GameManager.instance.CardPlayerJ.transform.GetChild(0).GetComponent<Animator>().Play("New State");
            GameManager.instance.CardInimigoJ.transform.GetChild(0).GetComponent<Animator>().Play("New State");

            GameManager.instance.CardPlayerJ.transform.GetChild(7).gameObject.SetActive(false);//Desativar hadouken
            GameManager.instance.CardInimigoJ.transform.GetChild(7).gameObject.SetActive(false);//Desativar hadouken
            GameManager.instance.CardPlayerJ.transform.GetChild(6).gameObject.SetActive(false);//Desativar hadouken
            GameManager.instance.CardInimigoJ.transform.GetChild(6).gameObject.SetActive(false);//Desativar hadouken
            GameManager.instance.CardInimigoJ.transform.GetChild(3).GetChild(2).
                GetComponent<TextMeshProUGUI>().text = lifeInimigo.ToString("0.0");
            GameManager.instance.CardPlayerJ.transform.GetChild(3).GetChild(2).
                GetComponent<TextMeshProUGUI>().text = lifePlayer.ToString("0.0");
            //Vida Player mostrada no text e image
            if (lifePlayer > 100)
            {
                GameManager.instance.CardPlayerJ.transform.GetChild(3).GetChild(1).gameObject.SetActive(true);
                GameManager.instance.CardPlayerJ.transform.GetChild(3).GetChild(1).
                    GetComponent<Image>().fillAmount = (lifePlayer - 100) / 100;
                GameManager.instance.CardPlayerJ.transform.GetChild(3).GetChild(0).
                    GetComponent<Image>().fillAmount = 1;

            }
            else if (lifePlayer < 100)
            {
                GameManager.instance.CardPlayerJ.transform.GetChild(3).GetChild(1).gameObject.SetActive(false);
                GameManager.instance.CardPlayerJ.transform.GetChild(3).GetChild(4).GetComponent<TextMeshProUGUI>().text = "100";
                GameManager.instance.CardPlayerJ.transform.GetChild(3).GetChild(0).
                    GetComponent<Image>().fillAmount = lifePlayer / 100;
            }
            else
            {
                GameManager.instance.CardPlayerJ.transform.GetChild(3).GetChild(1).gameObject.SetActive(false);
                GameManager.instance.CardPlayerJ.transform.GetChild(3).GetChild(4).GetComponent<TextMeshProUGUI>().text = "100";
                GameManager.instance.CardPlayerJ.transform.GetChild(3).GetChild(0).GetComponent<Image>().fillAmount = 1;
            }
            //Vida Inimigo mostrada no text e image
            if (lifeInimigo > 100)
            {
                GameManager.instance.CardInimigoJ.transform.GetChild(3).GetChild(1).gameObject.SetActive(true);
                GameManager.instance.CardInimigoJ.transform.GetChild(3).GetChild(1).
                    GetComponent<Image>().fillAmount = (lifeInimigo - 100) / 100;
                GameManager.instance.CardInimigoJ.transform.GetChild(3).GetChild(0).GetComponent<Image>().fillAmount = 1;
            }
            else if (lifeInimigo < 100)
            {
                GameManager.instance.CardInimigoJ.transform.GetChild(3).GetChild(1).gameObject.SetActive(false);
                GameManager.instance.CardInimigoJ.transform.GetChild(3).GetChild(4).GetComponent<TextMeshProUGUI>().text = "100";
                GameManager.instance.CardInimigoJ.transform.GetChild(3).GetChild(0).GetComponent<Image>().fillAmount = lifeInimigo / 100;
            }
            else
            {
                GameManager.instance.CardInimigoJ.transform.GetChild(3).GetChild(1).gameObject.SetActive(false);
                GameManager.instance.CardInimigoJ.transform.GetChild(3).GetChild(4).GetComponent<TextMeshProUGUI>().text = "100";
                GameManager.instance.CardInimigoJ.transform.GetChild(3).GetChild(0).GetComponent<Image>().fillAmount = 1;
            }
        }
        IEnumerator AnimRound()
        {
            //Aqui vamos limpar as mesas para o proximo round
            yield return new WaitForSeconds(4f);
            GameManager.instance.CardPlayerJ.transform.GetChild(4).gameObject.SetActive(false);
            GameManager.instance.CardInimigoJ.transform.GetChild(4).gameObject.SetActive(false);
            GameManager.instance.CardPlayerJ.transform.GetChild(7).gameObject.SetActive(false);
            GameManager.instance.CardInimigoJ.transform.GetChild(7).gameObject.SetActive(false);
            int qtdMesaPlayer = GameManager.instance.MesaJ1.transform.childCount;
            for (int i = 0; i < qtdMesaPlayer; i++)
            {
                GameManager.instance.MesaJ1.transform.GetChild(i).gameObject.SetActive(false);
                GameManager.instance.MesaIni.transform.GetChild(i).gameObject.SetActive(false);
            }
            GameManager.instance.GeradorDeCartas.InstCardForGame = 1;

            GameManager.instance.GeradorDeCartas.GerarCarta();
            GameManager.instance.ResultadoGame = true;
            GameManager.instance.AcabouRound = false;
            float vidaAtualIni = float.Parse(GameManager.instance.CardInimigoJ.
                transform.GetChild(3).GetChild(2).GetComponent<TextMeshProUGUI>().text);
            float vidaAtualP = float.Parse(GameManager.instance.CardPlayerJ.
                transform.GetChild(3).GetChild(2).GetComponent<TextMeshProUGUI>().text);
            if (vidaAtualIni <= 0 || vidaAtualP <= 0)
            {
                for (int i = 1; i <= 5; i++)
                {
                    if (i != 3)
                    {
                        GameManager.instance.CardPlayerJ.transform.GetChild(i).gameObject.SetActive(false);
                        GameManager.instance.CardInimigoJ.transform.GetChild(i).gameObject.SetActive(false);
                    }
                }
                GameManager.instance.AcabouJogo = true;
                if (vidaAtualP <= 0)
                    GameManager.instance.CardPlayerJ.transform.GetChild(0).GetComponent<Animator>().Play("PlayerDead");
                else
                    GameManager.instance.CardInimigoJ.transform.GetChild(0).GetComponent<Animator>().Play("InimDead");

                //Bloquear Cartas
                for (int i = 0; i < GameManager.instance.LocalCard.transform.childCount; i++)
                {
                    GameManager.instance.LocalCard.transform.GetChild(i).
                        GetComponent<Button>().interactable = false;
                }
                for (int i = 0; i < GameManager.instance.LocalCard.transform.childCount; i++)
                {
                    GameManager.instance.LocalCardIni.transform.GetChild(i).
                        GetComponent<Button>().interactable = false;
                }
                GameManager.instance.GameOver.gameObject.SetActive(true);
                if (vidaAtualP > 0)
                    GameManager.instance.GameOver.text = "VENCEU";
                else
                    GameManager.instance.GameOver.text = "PERDEU";
            }
            GameManager.instance.TwoPlayers = 0;
            //GameManager.instance.PlayerJogou = false;
            //Destruir os dois objetos
            Destroy(GameManager.instance.MesaJ1.transform.GetChild(0).gameObject);
            Destroy(GameManager.instance.MesaIni.transform.GetChild(0).gameObject);
        }
    }
}
