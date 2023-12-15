using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Realtime;
using Cashbaazi.App.Manager;
using Cashbaazi.App.Helper;


namespace Cashbaazi.Game.Common
{
    
    public class PlayerGame : MonoBehaviour
    {
        [SerializeField] Image Img_Avatar;
        [SerializeField] TextMeshProUGUI Txt_Playername;
        [SerializeField] TextMeshProUGUI Txt_Score;

        [Space]
        public Player ThisPlayer;
        public int ThisPlayerScore;

        private void Start()
        {
            ThisPlayerScore = 0;
            PhotonGameController.OnPlayerScoreUpdated += PhotonGameController_OnPlayerScoreUpdated;
        }
        private void OnDestroy()
        {
            
            PhotonGameController.OnPlayerScoreUpdated -= PhotonGameController_OnPlayerScoreUpdated;
        }


        public void SetPlayer(Player player)
        {
            ThisPlayer = player;
            Txt_Playername.text = ThisPlayer.NickName;

            Img_Avatar.sprite = AvatarManager.instance.Get_AvatarSprite(player.GetPlayer_AvatarIndex());
        }
        public void SetPlayer(BotPlayer player)
        {
            ThisPlayer = player.player;
            Txt_Playername.text = ThisPlayer.NickName;
        }
    

        private void PhotonGameController_OnPlayerScoreUpdated(Player player, int score)
        {
            if (player != ThisPlayer)
                return;

            ThisPlayerScore = score;
            Txt_Score.text = ThisPlayerScore.ToString();
            PhotonGameController.instance.CalculateRank();
            if (ThisPlayerScore == 0)
            {
                ThisPlayerScore = Random.Range(-1, -5);
            }
        }
    }
}