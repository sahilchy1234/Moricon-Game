using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cashbaazi.App.Common;
using Cashbaazi.App.Manager;
using Cashbaazi.App.Helper;
using UnityEngine.UI;

namespace Cashbaazi.App.Screen
{
    public class Screen_BattleSetup : ISCREEN
    {
        [Space(20)]
        [SerializeField] GameObject obj_SelectAmount;
        [SerializeField] GameObject obj_SelectPlayerOptions;

        [Header("Refrence")]
        [SerializeField] Screen_Common commonScreen;
        [SerializeField] Button onClickPractice;
        public override void Show()
        {
            obj_SelectAmount.SetActive(true);
            obj_SelectPlayerOptions.SetActive(false);
           

            base.Show();
            commonScreen.currentScreen = this;

            ScreenManager.instance.AddScreenToStack(this);
        }

        public void Set_BattleAmount(int _amount)
        {
            //if (_amount > AppManager.instance.Get_PlayerWallet())
            //{
            //    Toast.ShowToast(string.Format("You don't have enough balance to play {0}Rs. game", _amount));
            //    return;
            //}
            AppManager.instance.Get_BattleAmount(_amount);
            obj_SelectAmount.SetActive(false);
            obj_SelectPlayerOptions.SetActive(true);
        }
        public void Set_BattleMaxPlayers(int _maxPlayers)
        {
            AppManager.instance.Set_BattleMaxPlayer(_maxPlayers);
             ScreenManager.instance.SwitchScreen(SCREEN_TYPE.DEDUCT_BALANCE_FROM);
            
        }

        public void OnClickPraticeGame(int _maxPlayers)
        {
            AppManager.instance.Set_BattleMaxPlayer(_maxPlayers);
            ScreenManager.instance.SwitchScreen(SCREEN_TYPE.CONNECTING, commonScreen.currentScreen.screenType);
        }

    }
}