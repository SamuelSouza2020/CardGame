using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RandomCustomPropertyGenerator : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _text;

    //Tabela Hash
    private ExitGames.Client.Photon.Hashtable _myCustomProperties = new ExitGames.Client.Photon.Hashtable();

    public void SetCustomNumber()
    {
        System.Random rnd = new System.Random();
        int result = rnd.Next(0,99);

        _text.text = result.ToString();

        _myCustomProperties["RandomNumber"] = result;
        PhotonNetwork.SetPlayerCustomProperties(_myCustomProperties);
        //_myCustomProperties.Remove("RandomNumber");//Para excluir
        //Para personalizar um jogador especifico ou definir apenas no jogador local
        //PhotonNetwork.LocalPlayer.CustomProperties = _myCustomProperties;
    }
    public void OnClick_Button()
    {
        SetCustomNumber();
    }
}
