using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using Cashbaazi.App.Screen;
using Cashbaazi.App.Common;
using Cashbaazi.App.Helper;
using Cashbaazi.App.Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayfabManager : MonoBehaviour
{
    public string bottleGamedata;
    public string KnifeHitdata;
    public string DunkBalldata;
    public string FruitNinjadata;

    public static PlayfabManager instance;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
        instance = this;
        Login();
        // GetData();

    }

    void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = AppManager.instance.Get_PlayerData().mobile,
            CreateAccount = true
        };

        PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);




    }


    void UpdateUserNameFunction()
    {
        var request1 = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = AppManager.instance.Get_PlayerData().name,
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request1, OnNameUpdateSuccess, OnError);
    }

    void OnNameUpdateSuccess(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log("Name Update");
    }

    void OnSuccess(LoginResult result)
    {
        UpdateUserNameFunction();
        Debug.Log("Login Successfull");

        Invoke("GetData", 2f);

        SaveData("AvatarIndex", (AppManager.instance.Get_PlayerAvatarIndex()).ToString());

        FriendsManager.instance.LoadFriendsList();

        Playfab_Leaderboard.instance.SendLeaderboard(PlayerPrefs.GetInt("Main_Score",0));

        SaveOnlineStatus(true);
    }


       private void OnApplicationFocus(bool focusStatus)
    {
        SaveOnlineStatus(!focusStatus);
    }

    private void OnApplicationQuit()
    {
        SaveOnlineStatus(false);
    }
    
    void OnError(PlayFabError error)
    {
        Debug.Log("Error Login");
        Debug.Log(error.GenerateErrorReport());
    }



    public void SaveData(string game_name, string game_data)
    {
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>{
              {game_name, game_data}
            },

            Permission = UserDataPermission.Public
        };
        PlayFabClientAPI.UpdateUserData(request, OnDataSend, OnError);
    }

    public void GetData()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnDataReceived, OnError);
    }

    void OnDataReceived(GetUserDataResult result)
    {
        Debug.Log("Got Data!");

        if (result.Data != null)
        {

            if (result.Data.ContainsKey("BottleGameData"))
            {
                bottleGamedata = result.Data["BottleGameData"].Value;
            }

            if (result.Data.ContainsKey("KnifeHitGameData"))
            {
                KnifeHitdata = result.Data["KnifeHitGameData"].Value;
            }

            if (result.Data.ContainsKey("DunkGameData"))
            {
                DunkBalldata = result.Data["DunkGameData"].Value;
            }

            if (result.Data.ContainsKey("FruitNijaGameData"))
            {
                FruitNinjadata = result.Data["FruitNijaGameData"].Value;
            }
        }
        if (result.Data.ContainsKey("Main_Score"))
        {
            // FruitNinjadata = result.Data["FruitNijaGameData"].Value;
            string stringValue = result.Data["Main_Score"].Value;
            int intValue;

            if (int.TryParse(stringValue, out intValue))
            {
                PlayerPrefs.SetInt("Main_Score", intValue);
            }
            else
            {
                Debug.LogError("Failed to parse Main_Score value as an integer.");
            }

        }

    }

    public void BottleGameSaveFunction(string data)
    {
        string maindata = bottleGamedata + data;
        // bottleGamedata += data;
        SaveData("BottleGameData", maindata);
    }

    public void KnifeHitFunction(string data)
    {
        string maindata = KnifeHitdata + data;
        // KnifeHitdata += data;
        SaveData("KnifeHitGameData", maindata);
    }


    public void DunkSaveFunction(string data)
    {
        string maindata = DunkBalldata + data;
        // DunkBalldata += data;
        SaveData("DunkGameData", data);
    }


    public void FruitNijaSaveFunction(string data)
    {
        string maindata = FruitNinjadata + data;
        SaveData("FruitNijaGameData", maindata);
    }

    public void SaveOnlineStatus(bool isOnline)
    {
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                { "LastOnline", DateTime.UtcNow.ToString() },
                { "IsOnline", isOnline.ToString() } // Save the online status
            },
            Permission = UserDataPermission.Public
        };
        PlayFabClientAPI.UpdateUserData(request, OnDataSend, OnError);
    }


    void OnDataSend(UpdateUserDataResult result)
    {
        Debug.Log("Data Sent!");
    }
}
