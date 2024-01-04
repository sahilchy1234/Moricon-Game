using Cashbaazi.App.Helper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Cashbaazi.App.Common
{
    public class ScreenManager : Singleton<ScreenManager>
    {
        [SerializeField] ISCREEN[] allScreens;
        
        [Space]
        [SerializeField] SCREEN_TYPE firstScreen;
        [SerializeField] Stack<SCREEN_TYPE> screensStack;

        public ISCREEN currentScreen;
        int click = 0;
       // [SerializeField] GameObject exitScreen;
        private void Start()
        {
            
            screensStack = new Stack<SCREEN_TYPE>();
            SwitchScreen(firstScreen);   
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
               click++;
                ShowBackScreen();               
            }
            //else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MENU"))
            //{
            //    if (Input.GetKeyDown(KeyCode.Escape))
            //    {
            //        Application.Quit();
            //        Debug.Log("Done 2");
            //    }
            //}
        }


        ISCREEN GetScreen(SCREEN_TYPE stype)
        {
            return System.Array.Find(allScreens, x => x.screenType == stype);
        }

        public void SwitchScreen(SCREEN_TYPE _toShow, SCREEN_TYPE _tohide = SCREEN_TYPE.none)
        {
            ISCREEN toshow = GetScreen(_toShow);
            ISCREEN tohide = GetScreen(_tohide);

            if (tohide != null)
            {
                tohide.Hide();
                Timer.Schedule(this, Core.Screen_FadeTime, () => 
                {
                    toshow.Show();
                });
            }
            else
            {
                toshow.Show();
            }
        }

        public void HideScreen(SCREEN_TYPE _tohide)
        {
            ISCREEN tohide = GetScreen(_tohide);
            if (tohide != null)
                tohide.Hide();
        }

        public void AddScreenToStack(ISCREEN _screenType)
        {
            screensStack.Push(_screenType.screenType);
        }
        public void ShowBackScreen()
        {
        //   if (screensStack.Count <= 1)
        //    return;
              ISCREEN currentShowing = GetScreen(screensStack.ToArray()[0]);
        //  ISCREEN previousShowing = GetScreen(screensStack.ToArray()[1]);
         // screensStack.Pop();
            currentShowing.Hide();
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MENU");
            // SwitchScreen(SCREEN_TYPE.COMMON);
           // Timer.Schedule(this, Core.Screen_FadeTime, () => previousShowing.Show());
        }

    }

    public enum SCREEN_TYPE
    {
        none,
        LOGIN,
        REGISTER,
        OTP,
        COMMON,
        MENU,
        VIP,
        OFFER,
        SHARE,
        WALLET,
        BATTLE_SETTING,
        CONNECTING,
        WAITING,
        GAME,
        GAMEOVER,
        USER_PROFILE_UPDATE,
        USER_PROFILE,
        ADD_WALLET,
        WITHDRAW_TYPE,
        TRANSACTION_HISTORY,
        WITHDRAW_PAYTM,
        WITHDRAW_UPI,
        WITHDRAW_BANK,
        DEDUCT_BALANCE_FROM,
        GAME_RULE,
        EXIT,
        WITHDRAWL_REQUEST,
        UPDATE_GAME,
        RULE_GUNSBOTTLE,
        RULE_KNIFEHIT,
        REPORT,
        CONNECTING_WINNING,
        WITHDRAW_AMAZONPAY,
        UPDATE_AMAZONPAYNUM,
        UPDATE_UPI_DETAILS,
        UPDATE_PAYTMNUM,
        UPDATE_BANK_DETAILS,
        RULE_DUNKBALL,
        RULE_FRUITNNINJA
    }
}