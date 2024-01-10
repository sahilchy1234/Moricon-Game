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


public class ChatInfoGame : MonoBehaviour
{


    public Image onlineStatusIMG;
    public Sprite[] imageOnlineOffline;
    public Image opponentImage;
    public Sprite[] allAvatars;
    public Text opponentName;


    public static ChatInfoGame instance;


    void Start()
    {
        instance = this;
    }


    public void OnFriendButtonClick(string TitleDisplayName)
    {
        // Debug.Log("Button clicked for friend: " + friend.TitleDisplayName);

        // Assign opponentName
        opponentName.text = TitleDisplayName;

        // Load avatar index (assuming it's stored as "AvatarIndex" in player's data)






        PlayFabClientAPI.GetUserData(new GetUserDataRequest
        {
            PlayFabId = TitleDisplayName,
            Keys = new List<string> { "AvatarIndex" }
        }, result =>
        {
            if (result.Data.TryGetValue("AvatarIndex", out UserDataRecord userData))
            {
                int avatarIndex = int.Parse(userData.Value);
                // Now you can use the avatar index to load and display the corresponding avatar
                // Update the UI or do whatever is necessary with the avatar information
                opponentImage.sprite = allAvatars[avatarIndex];
                // Now you can use the avatar index to load and display the corresponding avatar
                // Update the UI or do whatever is necessary with the avatar information
                // Debug.Log($"Loaded Avatar Index for {friend.TitleDisplayName}: {avatarIndex}");
            }
        }, error =>
        {
            Debug.LogError("Failed to get user data: " + error.ErrorMessage);
        });




        StartCoroutine(CheckFriendOnlineStatus(TitleDisplayName));
    }


    IEnumerator CheckFriendOnlineStatus(string friend)
    {
        while (true)
        {
            yield return new WaitForSeconds(2f); // Adjust the interval as needed

            PlayFabClientAPI.GetUserData(new GetUserDataRequest
            {
                PlayFabId = friend,
                Keys = new List<string> { "IsOnline" }
            }, result =>
            {
                if (result.Data.TryGetValue("IsOnline", out UserDataRecord isOnlineData))
                {
                    bool isOnline = bool.Parse(isOnlineData.Value);

                    if (isOnline)
                    {
                        // Friend is online, activate requestButton
                        // requestButton.SetActive(true);
                        onlineStatusIMG.sprite = imageOnlineOffline[1];
                        onlineStatusIMG.color = Color.white;


                    }
                    else
                    {
                        // Friend is offline, deactivate requestButton
                        // requestButton.SetActive(false);
                        onlineStatusIMG.sprite = imageOnlineOffline[0];
                        onlineStatusIMG.color = new Color(0.5f, 0.5f, 0.5f, 1f); // RGB values (0.5, 0.5, 0.5) represent gray
                    }
                }
            }, error =>
            {
                Debug.LogError("Failed to get user data: " + error.ErrorMessage);
            });
        }
    }
}
