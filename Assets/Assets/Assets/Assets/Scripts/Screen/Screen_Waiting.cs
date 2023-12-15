using Cashbaazi.App.Common;
using Cashbaazi.App.Helper;
using Cashbaazi.Game.Common;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Cashbaazi.App.Screen
{
    public class Screen_Waiting : ISCREEN
    {
        [SerializeField] TextMeshProUGUI Txt_Status;
        float gameStart_TimeLeft;
        bool isGameStarting;
        [SerializeField] Base_GameStarter gameStarterScript;

        private void Update()
        {
            if (isGameStarting)
            {               
                gameStart_TimeLeft -= Time.deltaTime;
                Txt_Status.text = string.Format("<color=white>GAME STARTS IN</color>\n<size=40>{0}</size>", (int)gameStart_TimeLeft);

                if (gameStart_TimeLeft <= 0)
                {         
                    isGameStarting = false;
                  //  ScreenManager.instance.SwitchScreen(SCREEN_TYPE.GAME, this.screenType);
                }
            }
        }

        public override void Show()
        {
            Txt_Status.text = string.Format("<color=white>Please Wait...</color>\n<size=40>We are waiting for other players to join</size>");

            base.Show();
            Timer.Schedule(this, Core.Screen_FadeTime * 0.1f, () => 
            {
                PhotonGameController.instance.Send_PlayerReady();
            });
            
        }

        public void StartTimer()
        {          
            gameStart_TimeLeft = 0f;
            isGameStarting = true;
        }
    }
}