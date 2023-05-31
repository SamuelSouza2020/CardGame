using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AvataresController : MonoBehaviour
{
    public GameObject PlayerAvatar, CpuAvatar;
    public float AtkPlayer, DefPlayer, AtkCpu, DefCpu;
    void Start()
    {
        AtkPlayer = float.Parse(PlayerAvatar.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text);
        DefPlayer = float.Parse(PlayerAvatar.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text);
        AtkCpu = float.Parse(CpuAvatar.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text);
        DefCpu = float.Parse(CpuAvatar.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text);
    }
}
