using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeGame : MonoBehaviour
{
    public float TempoRestante = 5;

    void Update()
    {
        if(TempoRestante > 0)
        {
            TempoRestante -= Time.deltaTime;
            gameObject.GetComponent<Image>().fillAmount = (TempoRestante / 100) * 5;
            gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = TempoRestante.ToString("0");
        }
        else
        {
            TempoRestante = 5;
            gameObject.SetActive(false);
            GameManager.instance.AcabouTempo = true;
        }
    }
    public void TimeReset(float newTime)
    {
        TempoRestante = newTime;
    }
}
