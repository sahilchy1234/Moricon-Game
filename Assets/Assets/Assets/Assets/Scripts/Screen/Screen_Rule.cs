using System;
using System.Collections;
using System.Collections.Generic;
using Cashbaazi.App.Common;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Cashbaazi.App.Screen
{
    public class Screen_Rule : ISCREEN
    {
        [Space]
        [SerializeField] Button btn_ok;

        private void Start()
        {
            btn_ok.onClick.AddListener(OnClick_Ok);
        }

        public override void Show()
        {
            if (!PlayerPrefs.HasKey(SceneManager.GetActiveScene().name))
            {
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, 1);
                base.Show();
            }
        }

        private void OnClick_Ok()
        {
            ScreenManager.instance.HideScreen(this.screenType);
        }
    }
}