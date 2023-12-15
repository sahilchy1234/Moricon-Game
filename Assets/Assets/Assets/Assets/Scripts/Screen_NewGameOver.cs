using Cashbaazi.App.Common;
using Cashbaazi.Game.Common;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cashbaazi.App.Helper;
using Cashbaazi.App.Manager;
using UnityEngine.SceneManagement;


namespace Cashbaazi.App.Screen
{
    public class Screen_NewGameOver : ISCREEN
    {
        [Space(20)]
        // [SerializeField] TextMeshProUGUI Txt_BestScore;

        [Space]
        [SerializeField] GameObject player1;
        [SerializeField] GameObject player2;
        [SerializeField] GameObject player3;
        [SerializeField] GameObject player4;

        [Space]
        [SerializeField] List<PlayerGame> allPlayers;

        [Space]
        [SerializeField] PlayerGameover playerGameOver_prefab;
        // [SerializeField] Transform playerGameOver_parent;

        [SerializeField] Button Btn_Menu;
        [SerializeField] Button ReportFraud;
        // [SerializeField] Button Btn_PlayAgain;

        [SerializeField] Screen_Report reportScreen;

        private void Start()
        {
            Btn_Menu.onClick.AddListener(OnClick_Menu);
            ReportFraud.onClick.AddListener(OnClickReportBtn);
            //  Btn_PlayAgain.onClick.AddListener(OnClick_PlayAgain);
        }


        public override void Show()
        {
            base.Show();
            for (int i = 0; i < allPlayers.Count; i++)
            {
                //PlayerGameover player = Instantiate(playerGameOver_prefab, playerGameOver_parent);
                //player.SetPlayer(allPlayers[i].ThisPlayer, allPlayers[i].ThisPlayerScore, i + 1);

                if (!player1.activeInHierarchy)
                {
                    player1.SetActive(true);
                    player1.GetComponent<Image>().sprite = AvatarManager.instance.Get_AvatarSprite(allPlayers[i].ThisPlayer.GetPlayer_AvatarIndex());
                    player1.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = allPlayers[i].ThisPlayer.NickName;
                    player1.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = i == 0 ?
                        String.Format("<color=green>Rs.{0}</color>", AppManager.instance.Get_GameWinningAmount()) :
                        String.Format("<color=red>Rs.-{0}</color>", AppManager.instance.Get_BattleSettings().amount);
                    player1.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text = allPlayers[i].ThisPlayerScore.ToString();

                }
                else if (!player2.activeInHierarchy)
                {
                    player2.SetActive(true);
                    player2.GetComponent<Image>().sprite = AvatarManager.instance.Get_AvatarSprite(allPlayers[i].ThisPlayer.GetPlayer_AvatarIndex());
                    player2.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = allPlayers[i].ThisPlayer.NickName;
                    player2.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = i == 0 ?
                        String.Format("<color=green>Rs.{0}</color>", AppManager.instance.Get_GameWinningAmount()) :
                        String.Format("<color=red>Rs.-{0}</color>", AppManager.instance.Get_BattleSettings().amount);
                    player2.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text = allPlayers[i].ThisPlayerScore.ToString();
                }
                else if (!player3.activeInHierarchy)
                {
                    player3.SetActive(true);
                    player3.GetComponent<Image>().sprite = AvatarManager.instance.Get_AvatarSprite(allPlayers[i].ThisPlayer.GetPlayer_AvatarIndex());
                    player3.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = allPlayers[i].ThisPlayer.NickName;
                    player3.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = i == 0 ?
                        String.Format("<color=green>Rs.{0}</color>", AppManager.instance.Get_GameWinningAmount()) :
                        String.Format("<color=red>Rs.-{0}</color>", AppManager.instance.Get_BattleSettings().amount);
                    player3.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text = allPlayers[i].ThisPlayerScore.ToString();
                }
                else if (!player4.activeInHierarchy)
                {
                    player4.SetActive(true);
                    player4.GetComponent<Image>().sprite = AvatarManager.instance.Get_AvatarSprite(allPlayers[i].ThisPlayer.GetPlayer_AvatarIndex());
                    player4.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = allPlayers[i].ThisPlayer.NickName;
                    player4.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = i == 0 ?
                        String.Format("<color=green>Rs.{0}</color>", AppManager.instance.Get_GameWinningAmount()) :
                        String.Format("<color=red>Rs.-{0}</color>", AppManager.instance.Get_BattleSettings().amount);
                    player4.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text = allPlayers[i].ThisPlayerScore.ToString();
                }
            }
            //Txt_BestScore.text = string.Format("<color=white>Winner's Score</color>\n<size=200>{0}</size>", allPlayers[0].ThisPlayerScore);

            UpdateWinnerWallet();
            Timer.Schedule(this, 2f, () =>
            {
                PhotonManager.instance.DestroyBotPlayers();
            });
        }

        public void UpdateWinnerWallet()
        {
            if (allPlayers[0].ThisPlayer.IsLocal)
            {
               // ApiManager.instance.API_AddWallet("Game Winning Amount", AppManager.instance.Get_GameWinningAmount(),
                //    () =>
                //    {
                        Toast.ShowToast("Winning amount added! ENJOY");
                 //   },
                //    () =>
                  //  {
                        UpdateWinnerWallet();
                  //  });
            }
        }
        public void AssignAllPlayers(List<PlayerGame> _players)
        {
            allPlayers = _players;

        }
        public void CalculateRank()
        {
            allPlayers = allPlayers.OrderByDescending(x => x.ThisPlayerScore).ToList();
        }


        private void OnClick_Menu()
        {
            Loading.instance.ShowLoading();
            PhotonNetwork.Disconnect();
        }

        private void OnClickReportBtn()
        {
            ScreenManager.instance.SwitchScreen(SCREEN_TYPE.REPORT, this.screenType);
            Debug.Log("Report Page");
        }

        /*   private void OnClick_PlayAgain()
           {

               ScreenManager.instance.SwitchScreen(SCREEN_TYPE.CONNECTING);
           }*/
    }
}