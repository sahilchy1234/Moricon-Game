using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Cashbaazi.App.Common;
using System;
using Cashbaazi.App.Helper;
using Cashbaazi.App.Manager;

namespace Cashbaazi.App.Screen
{
    public class Screen_DeductFrom : ISCREEN
    {
        public Toggle[] tog;
        public Button mainBtn;
        [Space]
        [SerializeField] TextMeshProUGUI txt_deposit;
        [SerializeField] TextMeshProUGUI txt_winning;


        [Space]
        [SerializeField] Button btn_deposit;
        [SerializeField] Button btn_winning;
        [SerializeField] Button btn_back;

        [Header("Refrence")]
        [SerializeField] Screen_Common commonScreen;

        private void Start()
        {
            btn_deposit.onClick.AddListener(OnClick_Deposit);
            btn_winning.onClick.AddListener(OnClick_Winning);
            btn_back.onClick.AddListener(OnClick_Back);
        }

        public override void Show()
        {
            txt_deposit.text = String.Format(ApiManager.instance.responce_userdata.MainWallet.ToString());
            txt_winning.text = String.Format(ApiManager.instance.responce_userdata.WinningWallet.ToString());

            base.Show();
        }

        private void Update()
        {
            if (tog[0].isOn || tog[1].isOn)
            {
                mainBtn.interactable = true;

            }
            else
            {
                mainBtn.interactable = false;
            }

        }

        public void mainButtonFunction()
        {
            if (tog[0].isOn)
            {
                OnClick_Deposit();
            }
            else
            {
                if (tog[1].isOn)
                {
                    OnClick_Winning();
                }
            }
        }


        private void OnClick_Deposit()
        {
            if (AppManager.instance.Get_BattleSettings().amount > ApiManager.instance.responce_userdata.MainWallet)
            {
                Toast.ShowToast("You don't have enough balance in playable!");
                return;
            }

            //  AppManager.instance.Set_DeductFrom();
            //  commonScreen.WalletUpdate();
            ScreenManager.instance.SwitchScreen(SCREEN_TYPE.CONNECTING, commonScreen.currentScreen.screenType);


        }

        private void OnClick_Winning()
        {
            if (AppManager.instance.Get_BattleSettings().amount > ApiManager.instance.responce_userdata.WinningWallet)
            {
                Toast.ShowToast("You don't have enough balance in redeemable!");
                return;
            }
            //  AppManager.instance.Set_DeductFrom();
            ScreenManager.instance.SwitchScreen(SCREEN_TYPE.CONNECTING_WINNING, commonScreen.currentScreen.screenType);


        }
        private void OnClick_Back()
        {
            ScreenManager.instance.HideScreen(this.screenType);
        }

    }
}