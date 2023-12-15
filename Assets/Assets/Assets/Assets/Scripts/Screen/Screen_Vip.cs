using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cashbaazi.App.Common;
using Cashbaazi.App.Manager;

namespace Cashbaazi.App.Screen
{
    public class Screen_Vip : ISCREEN
    {
        [Header("Refrence")]
        [SerializeField] Screen_Common commonScreen;

        public override void Show()
        {
            base.Show();
            commonScreen.currentScreen = this;

            ScreenManager.instance.AddScreenToStack(this);
        }
    }
}