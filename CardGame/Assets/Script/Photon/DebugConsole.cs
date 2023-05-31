using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugConsole : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject _textFrame;
    [SerializeField]
    private int _maxLength = 5000;
    [SerializeField]
    private TextMeshProUGUI _text;

    public static DebugConsole instance;

    void Awake()
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
        AddText("Debug console is running.");
    }

    public void AddText(string text, UnityEngine.Object obj = null)
    {
        Debug.Log(text, obj);
        _text.text = text + Environment.NewLine + _text.text;
        if(_text.text.Length > _maxLength)
            _text.text = _text.text.Substring(0, _maxLength / 2);
    }
    public void OnClick_Button()
    {
        _textFrame.SetActive(!_textFrame.activeSelf);
    }
}
