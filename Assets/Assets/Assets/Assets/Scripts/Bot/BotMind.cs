using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cashbaazi.Game.Common
{
    public class BotMind : MonoBehaviour
    {
        public BotPlayer myPlayer;
        public bool isMindActivated;

        public virtual void InitBot(BotPlayer _player)
        {
            myPlayer = _player;
            myPlayer.playerMind = this;
        }
        public virtual void ActivateMind()
        {
            isMindActivated = true;
        }
        public virtual void DeactivateMind()
        {
            isMindActivated = false;
        }
        public virtual void SendScoreToPlayer(int toadd)
        {
            myPlayer.OnScoreReceivedFrom_Mind(toadd);
        }
    }
}