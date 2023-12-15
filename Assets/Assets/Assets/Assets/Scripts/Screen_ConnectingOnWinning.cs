using UnityEngine;
using TMPro;
using Photon.Realtime;
using Photon.Pun;
using Cashbaazi.App.Common;
using Cashbaazi.App.Manager;
using Cashbaazi.App.Menu;
using Cashbaazi.App.Helper;
using UnityEngine.UI;
using Cashbaazi.Game.Common;
using System.Collections.Generic;

namespace Cashbaazi.App.Screen
{
    public class Screen_ConnectingOnWinning : ISCREEN
    {
        [Space(20)]
        [SerializeField] GameObject objPlayer_me;
        [SerializeField] GameObject objPlayer_Opp1;
        [SerializeField] GameObject objPlayer_Opp2;
        [SerializeField] GameObject objPlayer_Opp3;

        public PlayerInfo[] playerInfos;

        [SerializeField] Sprite defaultSpr;

        [Space]
        [SerializeField] TextMeshProUGUI Txt_GameAmount;
        [SerializeField] TextMeshProUGUI Txt_WinAmount;
        [SerializeField] TextMeshProUGUI Txt_GameStatus;

        [Header("Refrence")]
        [SerializeField] Screen_Common commonScreen;

        [Header("Bot settings")]
      //  [SerializeField] float botAdd_TimeMax;
      //  [SerializeField] float botAdd_TimeLeft;
        [SerializeField] TextMeshProUGUI timerStart;
        bool isLocalJoinedRoom;
        bool isAllReady;
        float gameStart_TimeLeft;
        public float timerStart_Timeleft = 70;
        float currentTime;

        private void Update()
        {
            currentTime = timerStart_Timeleft;
            if (currentTime >= 0)
                timerStart_Timeleft -= Time.deltaTime;
            timerStart.text = (Mathf.RoundToInt(timerStart_Timeleft % 60)).ToString();
            if (currentTime <= 0)
            {
                currentTime = 0;
            }

            if (isAllReady)
            {
                gameStart_TimeLeft -= Time.deltaTime;
                Txt_GameStatus.text = string.Format("Game Starts in {0} Seconds", (int)gameStart_TimeLeft);

                if (gameStart_TimeLeft <= 0)
                {
                    isAllReady = false;
                    StartGameOnWinning();

                }
            }
           // else
            //{
            //    if (isLocalJoinedRoom)
            //    {
            //        botAdd_TimeLeft -= Time.deltaTime;
            //        if (botAdd_TimeLeft <= 0)
            //        {
            //            isLocalJoinedRoom = false;
            //            botAdd_TimeLeft = botAdd_TimeMax;
            //            PhotonNetwork.CurrentRoom.IsOpen = false;
            //            PhotonManager.instance.GenerateBotPlayer(AppManager.instance.Get_BattleSettings().maxPlayers - PhotonNetwork.PlayerList.Length);
            //        }
            //    }
            //}
        }


        public override void Show()
        {

            objPlayer_Opp1.SetActive(AppManager.instance.Get_BattleSettings().maxPlayers >= 2);
            objPlayer_Opp2.SetActive(AppManager.instance.Get_BattleSettings().maxPlayers >= 3);
            objPlayer_Opp3.SetActive(AppManager.instance.Get_BattleSettings().maxPlayers >= 4);

            playerInfos[0].profilePic.sprite = AvatarManager.instance.Get_AvatarSprite(ApiManager.instance.responce_userdata.avtar);
            playerInfos[0].userName.text = ApiManager.instance.responce_userdata.name;

            for (int i = 1; i < playerInfos.Length; i++)
            {
                playerInfos[i].profilePic.sprite = defaultSpr;
                playerInfos[i].userName.text = "Finding...";
            }

            isLocalJoinedRoom = false;
           // botAdd_TimeLeft = botAdd_TimeMax;

            Loading.instance.ShowLoading();

            PhotonManager.JoinedRoom += PhotonManager_JoinedRoom;
            PhotonManager.Disconnected += PhotonManager_Disconnected;
            PhotonManager.NewPlayerJoined += PhotonManager_NewPlayerJoined;
            PhotonManager.OtherPlayerLeft += PhotonManager_OtherPlayerLeft;

            Txt_GameAmount.text = string.Format("Entry Fee :- <color=white>Rs.{0}", AppManager.instance.Get_BattleSettings().amount);
            Txt_WinAmount.text = string.Format("Win Prize :- <color=white>Rs.{0}", AppManager.instance.Get_GameWinningAmount());

            base.Show();
            commonScreen.currentScreen = this;

            PhotonManager.instance.ConnectToPhoton();

            ScreenManager.instance.AddScreenToStack(this);
        }
        public override void Hide()
        {
            PhotonManager.JoinedRoom -= PhotonManager_JoinedRoom;
            PhotonManager.Disconnected -= PhotonManager_Disconnected;
            PhotonManager.NewPlayerJoined -= PhotonManager_NewPlayerJoined;
            PhotonManager.OtherPlayerLeft -= PhotonManager_OtherPlayerLeft;

            //PhotonManager.instance.DisconnectToPhoton();
            base.Hide();
        }
        private void OnDestroy()
        {
            PhotonManager.JoinedRoom -= PhotonManager_JoinedRoom;
            PhotonManager.Disconnected -= PhotonManager_Disconnected;
            PhotonManager.NewPlayerJoined -= PhotonManager_NewPlayerJoined;
            PhotonManager.OtherPlayerLeft -= PhotonManager_OtherPlayerLeft;


        }

        void CheckPlayerStatus()
        {
            if (PhotonNetwork.PlayerList.Length + PhotonManager.instance.BotPlayers.Count == AppManager.instance.Get_BattleSettings().maxPlayers)
            {
                gameStart_TimeLeft = 3f;
                Invoke(nameof(DelayStart), 2f);
            }
            else
            {
                isAllReady = false;
                Txt_GameStatus.text = string.Format("Waiting for {0} more players",
                    AppManager.instance.Get_BattleSettings().maxPlayers - PhotonNetwork.PlayerList.Length);
            }
        }

        void DelayStart()
        {
            isAllReady = true;
        }

        public void StartGameOnWinning()
        {
            Toast.ShowToast("Amount deducted! Please do not leave the game");
            //SceneHandler.instance.SwitchScene(AppManager.instance.Get_BattleSettings().gameType.ToString());
            // PhotonNetwork.LoadLevel(AppManager.instance.Get_BattleSettings().gameType.ToString());
            ApiManager.instance.API_MinusWallet("Game Entry Fee", AppManager.instance.Get_BattleSettings().amount, 3,
                () =>
                {
                    Toast.ShowToast("Amount deducted! Please do not leave the game");
                    //        // SceneHandler.instance.SwitchScene(SCENE_TYPE.GunsBottle);
                    SceneHandler.instance.SwitchScene(AppManager.instance.Get_BattleSettings().gameType.ToString());
                },
                () =>
                {

                });

        }

        void ResetPlayerValues(Player[] players)
        {
            for (int i = 0; i < playerInfos.Length; i++)
            {
                playerInfos[i].profilePic.sprite = defaultSpr;
                playerInfos[i].userName.text = "Finding...";
            }
            for (int i = 0; i < players.Length; i++)
            {
                playerInfos[i].profilePic.sprite = AvatarManager.instance.Get_AvatarSprite(players[i].GetPlayer_AvatarIndex());
                playerInfos[i].userName.text = players[i].NickName;
            }
        }


        private void PhotonManager_JoinedRoom()
        {
            Loading.instance.HideLoading();
            CheckPlayerStatus();

            //playerInfos[0].profilePic.sprite = AvatarManager.instance.Get_AvatarSprite(PhotonNetwork.LocalPlayer.GetPlayer_AvatarIndex());
            //playerInfos[0].userName.text = PhotonNetwork.LocalPlayer.NickName;

            isLocalJoinedRoom = true;
            //playerIds.Add(PhotonNetwork.LocalPlayer.UserId);
            ResetPlayerValues(PhotonNetwork.PlayerList);
        }



        private void PhotonManager_Disconnected()
        {
            ScreenManager.instance.SwitchScreen(SCREEN_TYPE.MENU, this.screenType);
        }


        private void PhotonManager_NewPlayerJoined(Player player)
        {
            CheckPlayerStatus();
            //playerIds.Add(player.UserId);
            //int playerIndex = playerIds.FindIndex(id => id == player.UserId);
            //if (playerIndex == -1)
            //{
            //    Debug.Log("User Does not found !!");
            //    return;
            //}
            //playerInfos[playerIndex].profilePic.sprite = AvatarManager.instance.Get_AvatarSprite(player.GetPlayer_AvatarIndex());
            //playerInfos[playerIndex].userName.text = player.NickName;

            ResetPlayerValues(PhotonNetwork.PlayerList);
        }
        private void PhotonManager_OtherPlayerLeft(Player player)
        {
            CheckPlayerStatus();

            //int playerIndex = playerIds.FindIndex(id => id == player.UserId);
            //if (playerIndex == -1)
            //{
            //    Debug.Log("User Does not found !!");
            //    return;
            //}
            //playerInfos[playerIndex].profilePic.sprite = defaultSpr;
            //playerInfos[playerIndex].userName.text = "Finding...";
            //playerIds.RemoveAt(playerIndex);

            ResetPlayerValues(PhotonNetwork.PlayerList);
        }
    }
}

[System.Serializable]
public class PlayerInfo
{
    public Image profilePic;
    public TextMeshProUGUI userName;
}