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
            txt_deposit.text = String.Format("Deposit wallet - Rs.{0}", ApiManager.instance.responce_userdata.MainWallet);
            txt_winning.text = String.Format("Winning wallet - Rs.{0}", ApiManager.instance.responce_userdata.WinningWallet);

            base.Show();
        }

        private void Update()
        {
         
        }
        private void OnClick_Deposit()
        {
            if (AppManager.instance.Get_BattleSettings().amount > ApiManager.instance.responce_userdata.MainWallet)
            {
              Toast.ShowToast("You don't have enough balance in deposit!");
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
                Toast.ShowToast("You don't have enough balance in winnings!");
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