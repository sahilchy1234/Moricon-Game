using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Realtime;
using Cashbaazi.App.Manager;

namespace Cashbaazi.Game.Common
{
    public class PlayerGameover : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI Txt_PlayerName;
        [SerializeField] TextMeshProUGUI Txt_PlayerScore;
        [SerializeField] TextMeshProUGUI Txt_PlayerRank;
        [SerializeField] Image Img_Avatar;
        public void SetPlayer(Player _player, int _score, int _rank )
        {
            Txt_PlayerName.text = _player.NickName;
            Txt_PlayerScore.text = _score.ToString();
            Txt_PlayerRank.text = _rank.ToString();
            Img_Avatar.sprite = AvatarManager.instance.Get_AvatarSprite(_player.GetPlayer_AvatarIndex());
        }
    }
}