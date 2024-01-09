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



public class FriendsManager : MonoBehaviour
{
    public GameObject requestButton;
    public Text opponentName;
    public Image opponentImage;

    public static FriendsManager instance;
    [Space(20)]
    [SerializeField] Sprite[] allAvatars;
    public GameObject friendButtonPrefab;
    public Transform buttonContainer;

    private List<FriendInfo> _friends = null;

    void Start()
    {
        instance = this;
        StartCoroutine(UpdateFriendsOnlineStatusRoutine());
    }

    public void LoadFriendsList()
    {
        PlayFabClientAPI.GetFriendsList(new GetFriendsListRequest(), OnGetFriendsListSuccess, OnGetFriendsListFailure);
    }

    void OnGetFriendsListSuccess(GetFriendsListResult result)
    {
        _friends = result.Friends;
        DisplayFriends(_friends);
    }

    void OnGetFriendsListFailure(PlayFabError error)
    {
        Debug.LogError("Failed to get friends list: " + error.ErrorMessage);
    }

    void DisplayFriends(List<FriendInfo> friends)
    {
        foreach (var friend in friends)
        {
            CreateFriendButton(friend);
        }
    }

    int avatarIndexM;

    void CreateFriendButton(FriendInfo friend)
    {
        GameObject buttonGO = Instantiate(friendButtonPrefab, buttonContainer);
        Button friendButton = buttonGO.GetComponent<Button>();

        // Check if playerProfile is available
        Text buttonText = friendButton.GetComponentInChildren<Text>();
        buttonText.text = friend.TitleDisplayName; // Use the 'Username' property of PlayerProfile

        buttonGO.name = friend.TitleDisplayName;

        // Load avatar index (assuming it's stored as "AvatarIndex" in player's data)
        Debug.Log("Loading Avatar");
        PlayFabClientAPI.GetUserData(new GetUserDataRequest
        {
            PlayFabId = friend.FriendPlayFabId,
            Keys = new List<string> { "AvatarIndex" }
        }, result =>
        {
            if (result.Data.TryGetValue("AvatarIndex", out UserDataRecord userData))
            {
                int avatarIndex = int.Parse(userData.Value);
                avatarIndexM = avatarIndex;
                // Now you can use the avatar index to load and display the corresponding avatar
                // Update the UI or do whatever is necessary with the avatar information
                Debug.Log($"Loaded Avatar Index for {friend.TitleDisplayName}: {avatarIndex}");
            }
        }, error =>
        {
            Debug.LogError("Failed to get user data: " + error.ErrorMessage);
        });

        Image avatarImage = friendButton.transform.Find("avatarImage").GetComponent<Image>();
        avatarImage.sprite = allAvatars[avatarIndexM+1];

        Image onlineStatusImage = buttonGO.transform.Find("OnlineStatusImage").GetComponent<Image>();

        Text lastSeenText = buttonGO.transform.Find("lastSeenText").GetComponent<Text>();
        UpdateOnlineStatusImage(friend.FriendPlayFabId, onlineStatusImage, lastSeenText);

        friendButton.onClick.AddListener(() => OnFriendButtonClick(friend));
    }

    void OnFriendButtonClick(FriendInfo friend)
    {
        Debug.Log("Button clicked for friend: " + friend.TitleDisplayName);

        // Assign opponentName
        opponentName.text = friend.TitleDisplayName;

        // Load avatar index (assuming it's stored as "AvatarIndex" in player's data)
       





        PlayFabClientAPI.GetUserData(new GetUserDataRequest
        {
            PlayFabId = friend.FriendPlayFabId,
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
                Debug.Log($"Loaded Avatar Index for {friend.TitleDisplayName}: {avatarIndex}");
            }
        }, error =>
        {
            Debug.LogError("Failed to get user data: " + error.ErrorMessage);
        });





        specificChat.instance.chatObject.SetActive(true);
        specificChat.instance.SubscribeToRoomChatChannel(friend.TitleDisplayName);

        StartCoroutine(CheckFriendOnlineStatus(friend));
    }


    IEnumerator CheckFriendOnlineStatus(FriendInfo friend)
    {
        while (true)
        {
            yield return new WaitForSeconds(2f); // Adjust the interval as needed

            PlayFabClientAPI.GetUserData(new GetUserDataRequest
            {
                PlayFabId = friend.FriendPlayFabId,
                Keys = new List<string> { "IsOnline" }
            }, result =>
            {
                if (result.Data.TryGetValue("IsOnline", out UserDataRecord isOnlineData))
                {
                    bool isOnline = bool.Parse(isOnlineData.Value);

                    if (isOnline)
                    {
                        // Friend is online, activate requestButton
                        requestButton.SetActive(true);

                    }
                    else
                    {
                        // Friend is offline, deactivate requestButton
                        requestButton.SetActive(false);
                    }
                }
            }, error =>
            {
                Debug.LogError("Failed to get user data: " + error.ErrorMessage);
            });
        }
    }


    IEnumerator UpdateFriendsOnlineStatusRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);

            if (_friends != null)
            {
                foreach (var friend in _friends)
                {
                    Image onlineStatusImage = buttonContainer.Find(friend.TitleDisplayName).transform.Find("OnlineStatusImage").GetComponent<Image>();
                    Text lastSeentime = buttonContainer.Find(friend.TitleDisplayName).transform.Find("lastSeenText").GetComponent<Text>();
                    UpdateOnlineStatusImage(friend.FriendPlayFabId, onlineStatusImage, lastSeentime);
                }
            }
        }
    }

    void UpdateOnlineStatusImage(string playFabId, Image onlineStatusImage, Text textTime)
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest
        {
            PlayFabId = playFabId,
            Keys = new List<string> { "LastOnline", "IsOnline" }
        }, result =>
        {
            if (result.Data.TryGetValue("LastOnline", out UserDataRecord lastOnlineData) &&
                result.Data.TryGetValue("IsOnline", out UserDataRecord isOnlineData))
            {
                DateTime lastOnline = DateTime.Parse(lastOnlineData.Value);
                bool isOnline = bool.Parse(isOnlineData.Value);

                onlineStatusImage.gameObject.SetActive(isOnline);

                if (!isOnline)
                {
                    TimeSpan timeSinceLastOnline = DateTime.UtcNow - lastOnline;

                    if (timeSinceLastOnline.TotalMinutes < 1)
                    {
                        textTime.text = "last seen just now";
                    }
                    else if (timeSinceLastOnline.TotalMinutes < 60)
                    {
                        textTime.text = $"last seen {timeSinceLastOnline.TotalMinutes:F0} min ago";
                    }
                    else if (timeSinceLastOnline.TotalHours < 24)
                    {
                        textTime.text = $"last seen {timeSinceLastOnline.TotalHours:F0} hours ago";
                    }
                    else
                    {
                        textTime.text = $"last seen on {lastOnline.ToString("MM/dd/yyyy HH:mm:ss")}";
                    }

                    textTime.gameObject.SetActive(true);
                }
                else
                {
                    textTime.gameObject.SetActive(false);
                }
            }
        }, error =>
        {
            Debug.LogError("Failed to get user data: " + error.ErrorMessage);
        });
    }

}
