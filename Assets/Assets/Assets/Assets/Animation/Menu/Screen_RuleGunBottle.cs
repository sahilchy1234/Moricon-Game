using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cashbaazi.App.Common;
using Cashbaazi.App.Manager;
using Cashbaazi.App.Helper;
using TMPro;
using UnityEngine.UI;

namespace Cashbaazi.App.Screen
{
    public class Screen_RuleGunBottle : ISCREEN
    {
        [SerializeField] Button nextBtn;

        [Header("Refrence")]
        [SerializeField] Screen_Common commonScreen;

        private void Start()
        {
            nextBtn.onClick.AddListener(OnClickNextBtn);
        }
        public override void Show()
        {
            base.Show();
            commonScreen.currentScreen = this;
            ScreenManager.instance.AddScreenToStack(this);
        }


        public void OnClickNextBtn()
        {
            ScreenManager.instance.SwitchScreen(SCREEN_TYPE.BATTLE_SETTING);
            base.Hide();
        }
    }
}