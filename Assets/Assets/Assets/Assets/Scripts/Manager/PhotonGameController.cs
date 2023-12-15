using Cashbaazi.App.Common;
using Cashbaazi.App.Helper;
using Cashbaazi.App.Manager;
using Cashbaazi.App.Screen;
using Cashbaazi.Game.GunsBottleGame;
using Cashbaazi.Game.KnifeHit;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cashbaazi.Game.Common
{
    public class PhotonGameController : MonoBehaviour, IPunEventCallback
    {
        public static PhotonGameController instance;

        public delegate void ScoreUpdate(Player player, int score);
        public static event ScoreUpdate OnPlayerScoreUpdated;

        public bool IsGameFinished;
        [SerializeField] int localPlayerScore;
        [SerializeField] PhotonEventController eventController;

        [Header("References")]
        //  [SerializeField] Screen_Waiting screen_waiting;
        [SerializeField] Screen_Game screen_game;
        [SerializeField] Screen_Gameover screen_gameover;
        
        [SerializeField] Base_GameStarter gameStarterScript;

        int readyPlayerCount;
        HashSet<int> disconnectedPlayerIds; // Keep track of disconnected player IDs
        private bool isGameStarted;

        private void Awake()
        {
            instance = this;
            disconnectedPlayerIds = new HashSet<int>();
            isGameStarted = false;
        }
        private void Start()
        {
            PhotonManager.Disconnected += PhotonManager_Disconnected;

            GameObject GO = new GameObject("---EventController");
            eventController = GO.AddComponent<PhotonEventController>();
            eventController.GameDuration = Core.GameDuration_FruitNinja;

            eventController.eventListner = this;

            PhotonManager.instance.CreateBotMinds();
        }

       
        private void OnDestroy()
        {
            //if (isGameStarted)
            //{
             
               
            //    // Remove the local player from the game and notify the remaining players.
            //  //  PhotonNetwork.CloseConnection(PhotonNetwork.LocalPlayer);

            //   // string message = "Player " + PhotonNetwork.LocalPlayer.NickName + " has left the game early.";
            //   // PhotonNetwork.CurrentRoom.BroadcastMessage(message, null, 0);
            //}

            PhotonManager.Disconnected -= PhotonManager_Disconnected;
        }


        public void UpdateScore(int _scoreToAdd)
        {
            localPlayerScore += _scoreToAdd;
            Send_UpdatedScore();
        }

        public void CalculateRank()
        {
            screen_gameover.CalculateRank();
        }
        public int GameRemainingTime()
        {
            if (eventController == null)
                return (int)Core.GameDuration_FruitNinja;

            return (int)eventController.RemainingSecondsInGameOver;
        }


        private void PhotonManager_Disconnected()
        {
            //disconnectedPlayerIds.Add(PhotonNetwork.LocalPlayer.ActorNumber);

           // // Leave the room
           // PhotonNetwork.LeaveRoom();

          //  PhotonNetwork.JoinLobby();

            // Leave the room
           // PhotonNetwork.LeaveRoom();
            SceneHandler.instance.SwitchScene(SCENE_TYPE.MENU.ToString());
        }


        #region Public Methods for Sending Events
        public void Send_PlayerReady()
        {
           // if (disconnectedPlayerIds.Contains(PhotonNetwork.LocalPlayer.ActorNumber))
            //    return;

            eventController.Send_PlayerReady();
        }

        //public void OnPlayerEnteredRoom(Player newPlayer)
        //{
        //    //if (disconnectedPlayerIds.Contains(newPlayer.ActorNumber))
        //    //{
        //    //    Debug.Log($"Player {newPlayer.NickName} tried to rejoin the game, but was rejected because they disconnected earlier.");
        //    //    PhotonNetwork.CloseConnection(newPlayer);
        //    //    return;
        //    //}

        //    // Handle the case when a new player enters the game
        //}

        //void OnJoinedLobby()
        //{
        //    int playerID = PhotonNetwork.LocalPlayer.ActorNumber;

        //    if (disconnectedPlayerIds.Contains(playerID))
        //    {
        //        // Display message indicating player has been disconnected
        //        // and cannot rejoin the game
        //        return;
        //    }

        //    // Allow player to join a game
        //}

        public void Send_StartGame()
        {
            if (!PhotonNetwork.IsMasterClient)
                return;
            //foreach (int actorNumber in disconnectedPlayerIds)
            //{
            //    Player disconnectedPlayer = PhotonNetwork.CurrentRoom.GetPlayer(actorNumber);
            //    if (disconnectedPlayer != null)
            //    {
            //        PhotonNetwork.CloseConnection(disconnectedPlayer);
            //        PhotonNetwork.DestroyPlayerObjects(disconnectedPlayer);
            //    }
            //}
            //disconnectedPlayerIds.Clear();

            // Start the game
            eventController.Send_StartGame();
        }
        public void Send_UpdatedScore()
        {
            eventController.Send_UpdatedScore(localPlayerScore);
        }
        #endregion


        public void OnPlayerLeftGame(Player otherPlayer)
        {
          

            //if (isGameStarted)
            //{
               
            //    // Remove the player from the game and notify the remaining players.
            //    PhotonNetwork.CloseConnection(otherPlayer);

            //    string message = "Player " + otherPlayer.NickName + " has left the game early.";
            //   // PhotonNetwork.CurrentRoom.BroadcastMessage(message, null, 0);
            //}
        }

        #region Event callbacks
        public void OnPlayerReady(Player player)
        {
            readyPlayerCount++;

            if (readyPlayerCount + PhotonManager.instance.BotPlayers.Count == AppManager.instance.Get_BattleSettings().maxPlayers)
                if (readyPlayerCount == AppManager.instance.Get_BattleSettings().maxPlayers)
                    //screen_waiting.StartTimer();
                    ScreenManager.instance.SwitchScreen(SCREEN_TYPE.GAME);
        }
        public void OnGameStarts()
        {
              isGameStarted = true;
                gameStarterScript.InitGame();
        }
        public void OnSyncScore(Player player, int score)
        {
            if (OnPlayerScoreUpdated != null)
                OnPlayerScoreUpdated(player, score);


        }


        public void OnTimeEnds()
        {
            IsGameFinished = true;
            gameStarterScript.StopGame();

            foreach (var item in PhotonManager.instance.BotPlayers)
                item.playerMind.DeactivateMind();

            CalculateRank();

            Destroy(eventController.gameObject);
            ScreenManager.instance.SwitchScreen(SCREEN_TYPE.GAMEOVER, SCREEN_TYPE.GAME);
            
        }

        

        

        #endregion
    }
}