using Cashbaazi.App.Common;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace Cashbaazi.Game.Common
{
    public static class P_Extention
    {
        const string KEY_Avatar = "P_AVR";

        public static int GetPlayer_AvatarIndex(this Player player)
        {
            if (player.IsBot)
               return PhotonManager.instance.BotPlayers.Find(x => x.player == player).player_avatar_index;

            if (player.CustomProperties.ContainsKey(KEY_Avatar))
                return (int)player.CustomProperties[KEY_Avatar];

            Debug.Log(player.NickName + " custom propery not setted");
            return 0;
        }
        public static void SetPlayer_AvatarIndex(this Player player, int avatarIndex)
        {
            Hashtable hs = new Hashtable();
            hs.Add(KEY_Avatar, avatarIndex);

            player.SetCustomProperties(hs);
        }
    }
}