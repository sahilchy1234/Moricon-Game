using Cashbaazi.App.Common;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Cashbaazi.Game.Common
{
    public class BotPlayer : MonoBehaviour
    {
        public Player player;
        public BotMind playerMind;

        public string player_name;
        public int player_avatar_index;
        public int player_currentScore;

        private void Start()
        {
           PhotonManager.OnBotPlayerScoreUpdated += PhotonManager_OnBotPlayerScoreUpdated;
        }
        private void OnDestroy()
        {
           PhotonManager.OnBotPlayerScoreUpdated -= PhotonManager_OnBotPlayerScoreUpdated;
        }



        public Player InitBot(string _playername, int _avatarIndex, int actorNumber)
        {
            player_name = _playername;
            player_avatar_index = _avatarIndex;

            player = new Player(player_name, actorNumber);
            return player;
        }

        public void OnScoreReceivedFrom_Mind(int toAdd)
        {
           PhotonManager.instance.BotPlayer_UpdateScore(player, toAdd);
        }



        private void PhotonManager_OnBotPlayerScoreUpdated(int botActorNumber, int scoreToAdd)
        {
            if (player.ActorNumber != botActorNumber)
                return;

            player_currentScore += scoreToAdd;
            PhotonGameController.instance.OnSyncScore(player, player_currentScore);
        }
    }
}