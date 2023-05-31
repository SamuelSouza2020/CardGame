using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Manager/GameSettings")]
public class GameSettings : ScriptableObject
{
    [SerializeField]//Qualquer coisa mudar o número da versao para o 0
    private string _gameVersion = "0.0.1";
    public string GameVersion { get { return _gameVersion; } }
    [SerializeField]
    private string _nickName = "UsuarioTeste";

    public string NickName 
    { 
        get 
        {
            int value = Random.Range(0, 9999);
            return _nickName + value.ToString();
        } 
    }
}
