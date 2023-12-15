using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cashbaazi.App.Common;
using Cashbaazi.App.Manager;
using Cashbaazi.App.Helper;

namespace Cashbaazi.App.Screen
{
    public class Screen_Menu : ISCREEN
    {
        [Header("Refrence")]
        [SerializeField] Screen_Common commonScreen;

        public override void Show()

        {
           
          //  ApiManager.instance.Api_AppUpdate(() =>
       // {
            //var response = ApiManager.instance.response_App_Update;
          //  if (response.IsUpdateAvailable)
          //  {
           //     ScreenManager.instance.SwitchScreen(SCREEN_TYPE.UPDATE_GAME);
         //   }
          //  else
         //   {
                base.Show();
                  commonScreen.currentScreen = this;
          //   }   //  Show();
                    //  {
                    //     if (string.IsNullOrEmpty(ApiManager.instance.SessionToken))
                    //   {
                    //      base.Show();
                    //     ScreenManager.instance.AddScreenToStack(this);
                    // }

         //   });
            
       // }

        //public void Start()
        //{
        //    gunBottleGame.onClick.AddListener(OnClickGunBottleGame);
        //    KnifeHitGame.onClick.AddListener(OnClickKnifeHitGame);
        //}
        //public void SetSelectedGame(int gameTypeValue)
        //{
        //    
            
        //    //
        }

        public void OnClickGunBottleGame(int gameTypeValue)
        {
            AppManager.instance.Set_BattleType((GAME_TYPE)gameTypeValue);
            ScreenManager.instance.SwitchScreen(SCREEN_TYPE.RULE_GUNSBOTTLE, commonScreen.currentScreen.screenType);
        }

        public void OnClickKnifeHitGame(int gameTypeValue)
        {
            AppManager.instance.Set_BattleType((GAME_TYPE)gameTypeValue);
            ScreenManager.instance.SwitchScreen(SCREEN_TYPE.RULE_KNIFEHIT, commonScreen.currentScreen.screenType);
        }

        public void OnClickDunkBallGame(int gameTypeValue)
        {
            AppManager.instance.Set_BattleType((GAME_TYPE)gameTypeValue);
            ScreenManager.instance.SwitchScreen(SCREEN_TYPE.RULE_DUNKBALL, commonScreen.currentScreen.screenType);
        }

        public void OnClickFruitNinjaBallGame(int gameTypeValue)
        {
            AppManager.instance.Set_BattleType((GAME_TYPE)gameTypeValue);
            ScreenManager.instance.SwitchScreen(SCREEN_TYPE.RULE_FRUITNNINJA, commonScreen.currentScreen.screenType);
        }
    }
}