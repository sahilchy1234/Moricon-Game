using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cashbaazi.App.Screen;
public class ToggleImg : MonoBehaviour
{
    private Toggle _tog;
    public GameObject secondImg;

    public Screen_BattleSetup screenScript;



    void Start()
    {
        _tog = gameObject.GetComponent<Toggle>();
        screenScript.Set_BattleMaxPlayers(2);
    }
    void Update()
    {
        if (_tog.isOn)
        {
            secondImg.SetActive(false);
        }
        else
        {
            secondImg.SetActive(true);
        }
    }
}
