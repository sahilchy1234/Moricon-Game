using Cashbaazi.App.Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cashbaazi.App.Manager
{
    public class AppManager : Singleton<AppManager>
    {
        [Space(20)]
        [SerializeField] BATTLE_SETTINGS battleSetting;


        #region Public Values Methods
        public BATTLE_SETTINGS Get_BattleSettings()
        {
            return battleSetting;
        }
        public Responce_Userdata Get_PlayerData()
        {
            return ApiManager.instance.responce_userdata;
        }
        public float Get_PlayerWallet()
        {
            return ApiManager.instance.responce_userdata.wallet;
        }
        public int Get_PlayerAvatarIndex()
        {
            return int.Parse(ApiManager.instance.responce_userdata.avtar);
        }
        public float Get_GameWinningAmount()
        {
            float winningAmount = 0;
            if (battleSetting.maxPlayers == 2)
            {
                switch (battleSetting.amount)
                {
                    case 2:
                        winningAmount = 3f;
                        break;                   
                    case 5:
                        winningAmount = 8f;
                        break;
                    case 8:
                        winningAmount = 13f;
                        break;
                    case 10:
                        winningAmount = 16f;
                        break;
                    case 15:
                        winningAmount = 25f;
                        break;
                    case 20:
                        winningAmount = 33f;
                        break;
                    case 25:
                        winningAmount = 40f;
                        break;
                    case 50:
                        winningAmount = 80f;
                        break;
                    case 100:
                        winningAmount = 160f;
                        break;
                    default:
                        break;
                }
            }

            if (battleSetting.maxPlayers == 4)
            {
                switch (battleSetting.amount)
                {
                    case 2:
                        winningAmount = 6f;
                        break;
                    case 5:
                        winningAmount = 16f;
                        break;
                    case 8:
                        winningAmount = 26f;
                        break;
                    case 10:
                        winningAmount = 32f;
                        break;
                    case 15:
                        winningAmount = 50f;
                        break;
                    case 20:
                        winningAmount = 64f;
                        break;
                    case 25:
                        winningAmount = 80f;
                        break;
                    case 50:
                        winningAmount = 160f;
                        break;
                    case 100:
                        winningAmount = 320f;
                        break;
                    default:
                        break;
                }
            }

            return winningAmount;
        }
        #endregion


        #region Battle Settings
        public void Set_BattleType(GAME_TYPE _gtype)
        {
            battleSetting.gameType = _gtype;
        }
        public void Get_BattleAmount(int _amount)
        {
            battleSetting.amount = _amount;
        }
        public void Set_BattleMaxPlayer(int _maxPlayer)
        {
            battleSetting.maxPlayers = _maxPlayer;
        }
        public void Set_DeductFrom(int _from)
        {
            battleSetting.deductionWallet = _from;
        }
        #endregion
    }


    [System.Serializable]
    public class BATTLE_SETTINGS
    {
        public GAME_TYPE gameType;
        public int amount;
        public int maxPlayers;
        public int deductionWallet;
    }
    public enum GAME_TYPE
    {
        GunsBottleGame,
        KnifeHit,
        DunkBall,
        FruitNinja
    }
}