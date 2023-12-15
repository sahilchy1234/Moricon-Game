using Cashbaazi.App.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using Cashbaazi.App.Helper;
using Cashbaazi.App.Manager;

namespace Cashbaazi.App.Screen
{
    public class Screen_UpdateGame : ISCREEN
    {
        [SerializeField] Button updateBtn;
        void Start()
        {
            updateBtn.onClick.AddListener(OnClick_UpdateNow);
        }

        public override void Show()
        {
            base.Show();
            ScreenManager.instance.AddScreenToStack(this);
        }
        public void OnClick_UpdateNow()
        {
            Application.OpenURL("https://morcoin.in/site/DownloadApp");
        }
    }
}