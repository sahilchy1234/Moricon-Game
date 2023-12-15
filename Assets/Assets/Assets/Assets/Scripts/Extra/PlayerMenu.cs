using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Realtime;
using Cashbaazi.App.Manager;
using Cashbaazi.App.Common;
using Cashbaazi.Game.Common;

namespace Cashbaazi.App.Menu
{
    public class PlayerMenu : MonoBehaviour
    {
        [SerializeField] Image Img_Avatar;
        [SerializeField] TextMeshProUGUI Txt_PlayerName;

        public Player ThisPlayer;

        public void SetPlayer(Player _player)
        {
            ThisPlayer = _player;
            Txt_PlayerName.text = _player.NickName;

            if(!_player.IsBot)
                Img_Avatar.sprite = AvatarManager.instance.Get_AvatarSprite(_player.GetPlayer_AvatarIndex());
            else
                Img_Avatar.sprite = AvatarManager.instance.Get_AvatarSprite(PhotonManager.instance.GetBotPlayer(_player).player_avatar_index);
        }
    }
}