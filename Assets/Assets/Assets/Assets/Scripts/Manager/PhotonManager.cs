using Cashbaazi.App.Helper;
using Cashbaazi.App.Manager;
using Cashbaazi.Game.Common;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace Cashbaazi.App.Common
{
    public class PhotonManager : MonoBehaviourPunCallbacks
    {
        public static PhotonManager instance;

        public delegate void ConnectionEvents();
        public static event ConnectionEvents JoinedRoom;
        public static System.Action Disconnected;

        public delegate void PlayerEvents(Player player);
        public static event PlayerEvents NewPlayerJoined;
        public static event PlayerEvents OtherPlayerLeft;

        public delegate void BotPlayerEvents(int botActorNumber, int scoreToAdd);
        public static event BotPlayerEvents OnBotPlayerScoreUpdated;

        [SerializeField] PhotonView pv;

        [Header("BotPlayer")]
        [SerializeField] List<BotPlayer> botPlayers;

        public List<BotPlayer> BotPlayers
        {
            get { return botPlayers == null ? new List<BotPlayer>() : botPlayers; }
        }
        public BotPlayer GetBotPlayer(Player _player)
        {
            return botPlayers.Find(x => x.player == _player);
        }


        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(this.gameObject);
        }
        private void Start()
        {
            botPlayers = new List<BotPlayer>();
            // InitializeChatManager();


        }


        void InitializeChatManager()
        {
            ChatManager chatManager = FindObjectOfType<ChatManager>();
            chatManager.InitializePubNub();
            if (chatManager != null)
            {
                chatManager.SubscribeToRoomChatChannel(PhotonNetwork.CurrentRoom.Name);
            }
        }


        public void ConnectToPhoton()
        {
            PhotonNetwork.NickName = AppManager.instance.Get_PlayerData().name;
            PhotonNetwork.ConnectUsingSettings();
        }
        public void DisconnectToPhoton()
        {
            PhotonNetwork.Disconnect();
        }

        public string privateRoomNameToJoin;
        public void JoinRandomRoom()
        {

            if (specificChat.instance.isRequestedFromPrivateRoom)
            {
                PhotonNetwork.JoinRoom(privateRoomNameToJoin);
            }


            return;


            if (!specificChat.instance.isPrivateRoomNeed)
            {
                botPlayers.Clear();
                Hashtable hs = new Hashtable();
                hs.Add("gametype", AppManager.instance.Get_BattleSettings().gameType.ToString());
                hs.Add("amount", AppManager.instance.Get_BattleSettings().amount);

                string[] propertyForLobby = new string[] { "gametype", "amount" };

                RoomOptions rops = new RoomOptions();
                rops.MaxPlayers = (byte)AppManager.instance.Get_BattleSettings().maxPlayers;
                rops.CustomRoomProperties = hs;
                rops.CustomRoomPropertiesForLobby = propertyForLobby;

                PhotonNetwork.JoinRandomOrCreateRoom(hs, (byte)AppManager.instance.Get_BattleSettings().maxPlayers, MatchmakingMode.FillRoom,
                    null, null, null, rops);
            }
            else
            {
                CreatePrivateRoom();
            }
        }



        public void CreatePrivateRoom()
        {
            botPlayers.Clear();

            // Set custom room properties
            Hashtable roomProperties = new Hashtable();
            roomProperties.Add("gametype", AppManager.instance.Get_BattleSettings().gameType.ToString());
            roomProperties.Add("amount", AppManager.instance.Get_BattleSettings().amount);

            // Set custom room properties to be visible in the lobby
            string[] propertyForLobby = new string[] { "gametype", "amount" };

            // Create RoomOptions and set MaxPlayers and custom properties
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = (byte)AppManager.instance.Get_BattleSettings().maxPlayers;
            roomOptions.IsVisible = false;  // Set to false to make the room private
            roomOptions.CustomRoomProperties = roomProperties;
            roomOptions.CustomRoomPropertiesForLobby = propertyForLobby;


            // Create a private room
            PhotonNetwork.CreateRoom(AppManager.instance.Get_PlayerData().name, roomOptions, null);
            specificChat.instance.messageInput.text = "battle_request_from " + "[" + AppManager.instance.Get_PlayerData().name + "]";
            specificChat.instance.SendMessageToRoom("battle_request_from " + "["+ AppManager.instance.Get_PlayerData().name + "]");
            specificChat.instance.SendMessageToRoom("battle_datas " + "[" + AppManager.instance.Get_BattleSettings().gameType + "]" + "{" + AppManager.instance.Get_BattleSettings().amount + "}" + "^" + AppManager.instance.Get_BattleSettings().maxPlayers + "*" + "!" + AppManager.instance.Get_BattleSettings().deductionWallet + "@");
             

             Debug.Log("battle_datas " + "[" + AppManager.instance.Get_BattleSettings().gameType + "]" + "{" + AppManager.instance.Get_BattleSettings().amount + "}" + "^" + AppManager.instance.Get_BattleSettings().maxPlayers + "*" + "!" + AppManager.instance.Get_BattleSettings().deductionWallet + "@");
      
        }


        #region Monocallbacks
        public override void OnConnectedToMaster()
        {
            Debug.Log("Connected To Master");
            PhotonNetwork.JoinLobby();

            PhotonNetwork.LocalPlayer.SetPlayer_AvatarIndex(AppManager.instance.Get_PlayerAvatarIndex());
        }
        public override void OnJoinedLobby()
        {
            Debug.Log("Joined Lobby");



            JoinRandomRoom();
        }
        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("Random join failed : " + message);
            JoinRandomRoom();
        }
        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            Debug.Log("Join failed : " + message);
            JoinRandomRoom();
        }
        public override void OnJoinedRoom()
        {
            Debug.Log("Joined Room : " + PhotonNetwork.CurrentRoom.Name);

            ChatManager chatManager = FindObjectOfType<ChatManager>();
            if (chatManager != null)
            {
                Debug.Log("room name" + PhotonNetwork.CurrentRoom.Name);
                chatManager.SubscribeToRoomChatChannel(PhotonNetwork.CurrentRoom.Name);
            }
            if (JoinedRoom != null)
                JoinedRoom();
        }
        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            if (NewPlayerJoined != null)
                NewPlayerJoined(newPlayer);

        }
        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            if (OtherPlayerLeft != null)
                OtherPlayerLeft(otherPlayer);
        }
        public override void OnLeftRoom()
        {
            Debug.Log("Room left");
            botPlayers.Clear();
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            Debug.Log("Disconnected : " + cause.ToString());
            if (Disconnected != null)
                Disconnected();
            botPlayers.Clear();
        }
        #endregion



        #region Generating bots
        public void GenerateBotPlayer(int botCount)
        {
            if (!PhotonNetwork.IsMasterClient)
                return;

            for (int i = 0; i < botCount; i++)
            {
                Timer.Schedule(this, i * UnityEngine.Random.Range(1f, 4f), () =>
                {
                    pv.RPC("RPC_InstantiateBot", RpcTarget.All,
                         Core.Bot_PlayerNames[Random.Range(0, Core.Bot_PlayerNames.Length)],
                         Random.Range(0, AvatarManager.instance.Get_All().Length));
                });
            }
        }
        public void DestroyBotPlayers()
        {
            foreach (var item in botPlayers)
                Destroy(item.gameObject);

            botPlayers.Clear();
        }
        [PunRPC]
        public void RPC_InstantiateBot(string botName, int botAvatarIndex)
        {
            GameObject botObj = new GameObject("Bot_" + botName);
            botObj.transform.SetParent(this.transform);

            BotPlayer bot = botObj.AddComponent<BotPlayer>();
            botPlayers.Add(bot);

            Player botPhoton = bot.InitBot(botName, botAvatarIndex, botPlayers.Count == 0 ? -1 : botPlayers.Count * -20);

            if (NewPlayerJoined != null)
                NewPlayerJoined(botPhoton);

        }
        public void CreateBotMinds()
        {
            if (AppManager.instance.Get_BattleSettings().gameType == GAME_TYPE.GunsBottleGame)
            {
                foreach (var item in botPlayers)
                {
                    GameObject mindObj = new GameObject("mind");
                    mindObj.transform.SetParent(item.transform);

                    BotMind mind = mindObj.AddComponent<Game.GunsBottleGame.BotMind_GunsBottleGame>();
                    mind.InitBot(item);
                }
            }
            else if (AppManager.instance.Get_BattleSettings().gameType == GAME_TYPE.KnifeHit)
            {
                foreach (var item in botPlayers)
                {
                    GameObject mindObj = new GameObject("mind");
                    mindObj.transform.SetParent(item.transform);

                    BotMind mind = mindObj.AddComponent<Game.KnifeHit.BotMind_KnifeHit>();
                    mind.InitBot(item);
                }
            }
        }
        public void BotPlayer_UpdateScore(Player player, int toAdd)
        {
            pv.RPC("RPC_BotPlayerUpdateScore", RpcTarget.All, player.ActorNumber, toAdd);
        }
        [PunRPC]
        public void RPC_BotPlayerUpdateScore(int playerActor, int toAdd)
        {
            if (OnBotPlayerScoreUpdated != null)
                OnBotPlayerScoreUpdated(playerActor, toAdd);
        }
        #endregion
    }
}
