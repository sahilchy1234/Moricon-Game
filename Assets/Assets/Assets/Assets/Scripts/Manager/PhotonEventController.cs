using Cashbaazi.App.Helper;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace Cashbaazi.Game.Common
{
    public class PhotonEventController : MonoBehaviourPunCallbacks, IOnEventCallback
    {
        public enum PHOTON_EVENTS
        {
            GAME_START,
            SYNC_SCORE,
            PLAYER_READY
        }


        public float GameDuration = 60f;
        public IPunEventCallback eventListner;
        bool isGameStarted;


        public float ElapsedTimeInGameOver
        {
            get { return ((float)(PhotonNetwork.ServerTimestamp - PhotonNetwork.CurrentRoom.Get_StartTime())) / 1000.0f; }
        }
        public float RemainingSecondsInGameOver
        {
            get { return Mathf.Max(0f, this.GameDuration - this.ElapsedTimeInGameOver); }
        }



        private void Update()
        {
            if (isGameStarted && RemainingSecondsInGameOver <= 0)
            {
                isGameStarted = false;
                eventListner.OnTimeEnds();
            }
        }



        public void Send_PlayerReady()
        {
            byte evCode = (byte)PHOTON_EVENTS.PLAYER_READY;
            PhotonNetwork.RaiseEvent(evCode, null, new RaiseEventOptions() { CachingOption = EventCaching.AddToRoomCache }, SendOptions.SendReliable);

            this.ProcessOnEvent(evCode, null, PhotonNetwork.LocalPlayer.ActorNumber);
        }
        public void Send_StartGame()
        {
            PhotonNetwork.CurrentRoom.Set_StartTime();

            byte evCode = (byte)PHOTON_EVENTS.GAME_START;
            PhotonNetwork.RaiseEvent(evCode, null, new RaiseEventOptions() { CachingOption = EventCaching.AddToRoomCache }, SendOptions.SendReliable);

            this.ProcessOnEvent(evCode, null, PhotonNetwork.LocalPlayer.ActorNumber);
        }
        public void Send_UpdatedScore(int _score)
        {
            Hashtable hs = new Hashtable();
            hs.Add("score", _score);

            byte evCode = (byte)PHOTON_EVENTS.SYNC_SCORE;
            PhotonNetwork.RaiseEvent(evCode, hs, new RaiseEventOptions() { CachingOption = EventCaching.AddToRoomCache }, SendOptions.SendReliable);

            this.ProcessOnEvent(evCode, hs, PhotonNetwork.LocalPlayer.ActorNumber);
        }




        void ProcessOnEvent(byte eventCode, object content, int senderId)
        {
            if (senderId == -1)
                return;

            Player sender = PhotonNetwork.CurrentRoom.GetPlayer(senderId);
            PHOTON_EVENTS evType = (PHOTON_EVENTS)eventCode;

            switch (evType)
            {
                case PHOTON_EVENTS.PLAYER_READY:
                    eventListner.OnPlayerReady(sender);

                    break;
                case PHOTON_EVENTS.GAME_START:
                    eventListner.OnGameStarts();
                    Timer.Schedule(this, .5f, () => isGameStarted = true);
                    break;
                case PHOTON_EVENTS.SYNC_SCORE:
                    Hashtable hs = content as Hashtable;
                    eventListner.OnSyncScore(sender, (int)hs["score"]);
                    break;
                default:
                    break;
            }
        }
        public void OnEvent(EventData photonEvent)
        {
            this.ProcessOnEvent(photonEvent.Code, photonEvent.CustomData, photonEvent.Sender);
        }
    }

    public interface IPunEventCallback
    {
        void OnPlayerReady(Player player);
        void OnGameStarts();
        void OnSyncScore(Player player, int score);
        void OnTimeEnds();
    }

    public static class PhotonEventsExtention
    {
        public static readonly string KEY_StartTime = "KST";


        public static void Set_StartTime(this Room room)
        {
            if (room == null)
                return;

            Hashtable hs = new Hashtable();
            hs[KEY_StartTime] = PhotonNetwork.ServerTimestamp;

            room.SetCustomProperties(hs);
        }
        public static int Get_StartTime(this RoomInfo room)
        {
            if (room == null || room.CustomProperties == null || !room.CustomProperties.ContainsKey(KEY_StartTime))
                return 0;

            return (int)room.CustomProperties[KEY_StartTime];
        }
    }
}