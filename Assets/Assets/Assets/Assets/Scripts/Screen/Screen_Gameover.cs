using Cashbaazi.App.Common;
using Cashbaazi.Game.Common;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cashbaazi.App.Helper;
using Cashbaazi.App.Manager;
using UnityEngine.SceneManagement;

using PlayFab;
using PlayFab.ClientModels;


using UnityEngine.Networking;
namespace Cashbaazi.App.Screen
{
    public class Screen_Gameover : ISCREEN
    {


        public GameObject winObject, lossObject;
        [Space(20)]


        private List<string> friendNames = new List<string>();
        public GameObject[] fourButtons;


        [Space(20)]
        // [SerializeField] TextMeshProUGUI Txt_BestScore;

        [Space]
        [SerializeField] GameObject player1;
        [SerializeField] GameObject player2;
        [SerializeField] GameObject player3;
        [SerializeField] GameObject player4;

        [Space]
        [SerializeField] List<PlayerGame> allPlayers;

        [Space]
        [SerializeField] PlayerGameover playerGameOver_prefab;
        // [SerializeField] Transform playerGameOver_parent;

        [SerializeField] Button Btn_Menu;
        [SerializeField] Button ReportFraud;
        // [SerializeField] Button Btn_PlayAgain;

        [SerializeField] Screen_Report reportScreen;
        public static Screen_Gameover instance;

        private void Start()
        {

            Btn_Menu.onClick.AddListener(OnClick_Menu);
            ReportFraud.onClick.AddListener(OnClickReportBtn);

        }







   

        public override void Show()
        {

            base.Show();
            for (int i = 0; i < allPlayers.Count; i++)
            {


                //PlayerGameover player = Instantiate(playerGameOver_prefab, playerGameOver_parent);
                //player.SetPlayer(allPlayers[i].ThisPlayer, allPlayers[i].ThisPlayerScore, i + 1);

                if (!player1.activeInHierarchy)
                {

                    player1.SetActive(true);
                    player1.GetComponent<Image>().sprite = AvatarManager.instance.Get_AvatarSprite(allPlayers[i].ThisPlayer.GetPlayer_AvatarIndex());
                    player1.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = allPlayers[i].ThisPlayer.NickName;
                    player1.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = i == 0 ?
                        String.Format("<color=green>Rs.{0}</color>", AppManager.instance.Get_GameWinningAmount()) :
                        String.Format("<color=red>Rs.-{0}</color>", AppManager.instance.Get_BattleSettings().amount);
                    player1.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text = allPlayers[i].ThisPlayerScore.ToString();

                    if (player1.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text == AppManager.instance.Get_PlayerData().name)
                    {
                        fourButtons[0].SetActive(false);
                    }


                }
                else if (!player2.activeInHierarchy)
                {




                    player2.SetActive(true);
                    player2.GetComponent<Image>().sprite = AvatarManager.instance.Get_AvatarSprite(allPlayers[i].ThisPlayer.GetPlayer_AvatarIndex());
                    player2.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = allPlayers[i].ThisPlayer.NickName;
                    player2.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = i == 0 ?
                        String.Format("<color=green>Rs.{0}</color>", AppManager.instance.Get_GameWinningAmount()) :
                        String.Format("<color=red>Rs.-{0}</color>", AppManager.instance.Get_BattleSettings().amount);
                    player2.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text = allPlayers[i].ThisPlayerScore.ToString();



                    if (player2.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text == AppManager.instance.Get_PlayerData().name)
                    {
                        fourButtons[1].SetActive(false);
                    }


                }
                else if (!player3.activeInHierarchy)
                {



                    player3.SetActive(true);
                    player3.GetComponent<Image>().sprite = AvatarManager.instance.Get_AvatarSprite(allPlayers[i].ThisPlayer.GetPlayer_AvatarIndex());
                    player3.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = allPlayers[i].ThisPlayer.NickName;
                    player3.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = i == 0 ?
                        String.Format("<color=green>Rs.{0}</color>", AppManager.instance.Get_GameWinningAmount()) :
                        String.Format("<color=red>Rs.-{0}</color>", AppManager.instance.Get_BattleSettings().amount);
                    player3.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text = allPlayers[i].ThisPlayerScore.ToString();




                    if (player3.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text == AppManager.instance.Get_PlayerData().name)
                    {
                        fourButtons[2].SetActive(false);
                    }





                }
                else if (!player4.activeInHierarchy)
                {








                    player4.SetActive(true);
                    player4.GetComponent<Image>().sprite = AvatarManager.instance.Get_AvatarSprite(allPlayers[i].ThisPlayer.GetPlayer_AvatarIndex());
                    player4.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = allPlayers[i].ThisPlayer.NickName;
                    player4.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = i == 0 ?
                        String.Format("<color=green>Rs.{0}</color>", AppManager.instance.Get_GameWinningAmount()) :
                        String.Format("<color=red>Rs.-{0}</color>", AppManager.instance.Get_BattleSettings().amount);
                    player4.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text = allPlayers[i].ThisPlayerScore.ToString();



                    if (player4.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text == AppManager.instance.Get_PlayerData().name)
                    {
                        fourButtons[3].SetActive(false);
                    }



                }
            }
            //Txt_BestScore.text = string.Format("<color=white>Winner's Score</color>\n<size=200>{0}</size>", allPlayers[0].ThisPlayerScore);
            UpdateWinnerWallet();

            Timer.Schedule(this, 2f, () =>
            {
                PhotonManager.instance.DestroyBotPlayers();
            });
        }











        public string currentIndianTime;
        private const string worldTimeApiUrl = "http://worldtimeapi.org/api/timezone/Asia/Kolkata"; // India time zone

        IEnumerator StartDataUploadingPlayfab()
        {
            // Make a web request to the WorldTimeAPI
            UnityWebRequest www = UnityWebRequest.Get(worldTimeApiUrl);
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                // Parse the JSON response
                string jsonResponse = www.downloadHandler.text;
                WorldTimeResponse worldTimeResponse = JsonUtility.FromJson<WorldTimeResponse>(jsonResponse);

                // Get the current time from the response
                System.DateTime currentTime = System.DateTime.Parse(worldTimeResponse.datetime);

                // Print the time
                Debug.Log("Current Time in India: " + currentTime.ToString());
                currentIndianTime = currentTime.ToString();

                DataStorePlayfab();

            }
            else
            {
                Debug.LogError("Error fetching Indian time: " + www.error);
            }
        }

        [System.Serializable]
        private class WorldTimeResponse
        {
            public string datetime;
        }







        string alldata;
        public void DataStorePlayfab()
        {
            //For two players
            if (AppManager.instance.Get_maxPlayers() == 2)
            {

                if (allPlayers[0].ThisPlayer.IsLocal)
                {

                    alldata = "\n" + " Game status : win, " + "Score:- " + allPlayers[0].ThisPlayerScore + " , " + "Entry Amount:- " + AppManager.instance.Get_EntryAmount().ToString() + ", " + "Opponent:- " + allPlayers[1].ThisPlayer.NickName + " Time:-" + currentIndianTime + " ; \n";
                }
                else
                {



                    if (allPlayers[1].ThisPlayer.IsLocal)
                    {
                        alldata = "\n" + " Game status : loss, " + "Score:- " + allPlayers[1].ThisPlayerScore + " , " + "Entry Amount:- " + AppManager.instance.Get_EntryAmount().ToString() + ", " + "Opponent:- " + allPlayers[0].ThisPlayer.NickName + " Time:-" + currentIndianTime + " ; \n";
                    }

                }
            }
            else
            {


                if (allPlayers[0].ThisPlayer.IsLocal)
                {

                    alldata = "\n" + " Game status : win, " + "Score:- " + allPlayers[0].ThisPlayerScore + " , " + "Entry Amount:- " + AppManager.instance.Get_EntryAmount().ToString() + ", " + "Opponents:- " + allPlayers[1].ThisPlayer.NickName + " " + allPlayers[2].ThisPlayer.NickName + " " + allPlayers[3].ThisPlayer.NickName + " Time:-" + currentIndianTime + " ; \n";
                }
                else
                {



                    if (allPlayers[1].ThisPlayer.IsLocal)
                    {
                        alldata = "\n" + " Game status : loss, " + "Score:- " + allPlayers[1].ThisPlayerScore + " , " + "Entry Amount:- " + AppManager.instance.Get_EntryAmount().ToString() + ", " + "Opponents:- " + allPlayers[0].ThisPlayer.NickName + " " + allPlayers[2].ThisPlayer.NickName + " " + allPlayers[3].ThisPlayer.NickName + " Time:-" + currentIndianTime + " ; \n";

                    }
                    else
                    {
                        if (allPlayers[2].ThisPlayer.IsLocal)
                        {
                            alldata = "\n" + " Game status : loss, " + "Score:- " + allPlayers[2].ThisPlayerScore + " , " + "Entry Amount:- " + AppManager.instance.Get_EntryAmount().ToString() + ", " + "Opponents:- " + allPlayers[0].ThisPlayer.NickName + " " + allPlayers[1].ThisPlayer.NickName + " " + allPlayers[3].ThisPlayer.NickName + " Time:-" + currentIndianTime + " ; \n";

                        }
                        else
                        {
                            if (allPlayers[3].ThisPlayer.IsLocal)
                            {
                                alldata = "\n" + " Game status : loss, " + "Score:- " + allPlayers[3].ThisPlayerScore + " , " + "Entry Amount:- " + AppManager.instance.Get_EntryAmount().ToString() + ", " + "Opponents:- " + allPlayers[0].ThisPlayer.NickName + " " + allPlayers[2].ThisPlayer.NickName + " " + allPlayers[1].ThisPlayer.NickName + " Time:-" + currentIndianTime + " ; \n";

                            }
                        }

                    }

                }

            }





            // if (allPlayers[2] != null)
            // {
            //     if (allPlayers[2].ThisPlayer.IsLocal)
            //     {
            //         alldata = "; loss, " + allPlayers[2].ThisPlayerScore + " , " + AppManager.instance.Get_EntryAmount().ToString();
            //     }
            // }
            // else
            // {



            //     if (allPlayers[3] != null)
            //     {
            //         if (allPlayers[3].ThisPlayer.IsLocal)
            //         {
            //             alldata = "; loss, " + allPlayers[3].ThisPlayerScore + " , " + AppManager.instance.Get_EntryAmount().ToString();
            //         }
            //     }
            // }

            Scene scene = SceneManager.GetActiveScene();

            if (scene.name == "GunsBottleGame")
            {
                PlayfabManager.instance.BottleGameSaveFunction(alldata);
            }
            if (scene.name == "KnifeHit")
            {
                PlayfabManager.instance.KnifeHitFunction(alldata);
            }
            if (scene.name == "DunkBall")
            {
                PlayfabManager.instance.DunkSaveFunction(alldata);
            }
            if (scene.name == "FruitNinja")
            {
                PlayfabManager.instance.FruitNijaSaveFunction(alldata);
            }

            Debug.Log(alldata);
        }




        public void UpdateWinnerWallet()
        {

            StartCoroutine("StartDataUploadingPlayfab");


            if (allPlayers[0].ThisPlayer.IsLocal)
            {
                ApiManager.instance.API_AddWallet("Game Winning Amount", AppManager.instance.Get_GameWinningAmount(),
                    () =>
                    {
                        int score = PlayerPrefs.GetInt("Main_Score");
                        score += (int)AppManager.instance.Get_GameWinningAmount(); // Explicitly cast to int
                        PlayerPrefs.SetInt("Main_Score", score);
                        PlayfabManager.instance.SaveData("Main_Score", score.ToString());

                        winObject.SetActive(true);
                        lossObject.SetActive(false);
                        Toast.ShowToast("Winning amount added! ENJOY");
                    },
                    () =>
                    {
                        UpdateWinnerWallet();
                    });
            }
            else
            {
                winObject.SetActive(false);
                lossObject.SetActive(true);
            }
        }


        public void AssignAllPlayers(List<PlayerGame> _players)
        {
            allPlayers = _players;

        }
        public void CalculateRank()
        {
            allPlayers = allPlayers.OrderByDescending(x => x.ThisPlayerScore).ToList();
        }


        public void OnClick_Menu()
        {
            PhotonNetwork.Disconnect();
            ScreenManager.instance.SwitchScreen(SCREEN_TYPE.MENU, this.screenType);
            //  Loading.instance.ShowLoading();

            Debug.Log("");
        }

        public void OnClickReportBtn()
        {
            ScreenManager.instance.SwitchScreen(SCREEN_TYPE.REPORT, this.screenType);
            Debug.Log("Report Page");
        }

    }
}