using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cashbaazi.App.Common;
using Cashbaazi.App.Manager;
using Cashbaazi.Game.Common;
using Photon.Pun;
using Photon.Realtime;
using Cashbaazi.App.Helper;
using TMPro;
using UnityEngine.SceneManagement;

namespace Cashbaazi.App.Screen
{
    public class Screen_Game : ISCREEN
    {
        [Space(20)]
        [SerializeField] PlayerGame playerGame_Prefab;
        [SerializeField] Transform playerGame_Parent;

        [Space]
        [SerializeField] PlayerGame playerGame_Local;
        [SerializeField] List<PlayerGame> playerGame_Others;

        [Space]
        [SerializeField] TextMeshProUGUI Txt_remainingTime;

        [Header("Refrence")]
        [SerializeField] Screen_Gameover screen_Gameover;
        private bool isPlaying;
        private void Start()
        {
            playerGame_Others = new List<PlayerGame>();
            isPlaying = true;
        }

        private void Update()
        {
            Txt_remainingTime.text = string.Format("Time Left\n<color=white><size=50>{0:00}</size></color>",
                PhotonGameController.instance.GameRemainingTime());
        }

        public override void Show()
        {
            List<PlayerGame> allPlayers = new List<PlayerGame>();

            playerGame_Local.SetPlayer(PhotonNetwork.LocalPlayer);
            allPlayers.Add(playerGame_Local);
            foreach (var item in PhotonNetwork.PlayerListOthers)
            {
                PlayerGame otherPlayer = Instantiate(playerGame_Prefab, playerGame_Parent);
                otherPlayer.SetPlayer(item);
                playerGame_Others.Add(otherPlayer);
                allPlayers.Add(otherPlayer);
            }
            foreach (var item in PhotonManager.instance.BotPlayers)
            {
                PlayerGame otherPlayer = Instantiate(playerGame_Prefab, playerGame_Parent);
                otherPlayer.SetPlayer(item);
                playerGame_Others.Add(otherPlayer);
                allPlayers.Add(otherPlayer);

                item.playerMind.ActivateMind();
            }

            screen_Gameover.AssignAllPlayers(allPlayers);

            base.Show();

            Timer.Schedule(this, Core.Screen_FadeTime, () =>
            {
                PhotonGameController.instance.Send_StartGame();
            });
            PhotonNetwork.AddCallbackTarget(this);

        }

        public override void Hide()
        {
            // Unregister from OnPlayerLeftRoom callback
            PhotonNetwork.RemoveCallbackTarget(this);

            base.Hide();
        }

    }
}