using UnityEngine;
using UnityEngine.UI;
using PlayFab.ClientModels;
using PlayFab;
using System.Collections.Generic;

public class FriendsManager : MonoBehaviour
{
    public GameObject friendButtonPrefab; // Reference to your button prefab
    public Transform buttonContainer; // Parent transform for the buttons (UI panel or another GameObject)

    private List<FriendInfo> _friends = null;

    void Start()
    {

        Invoke("LoadFriendsList", 5f);
        // LoadFriendsList();
    }

    void LoadFriendsList()
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

    void CreateFriendButton(FriendInfo friend)
    {
        GameObject buttonGO = Instantiate(friendButtonPrefab, buttonContainer);
        Button friendButton = buttonGO.GetComponent<Button>();

        // Check if playerProfile is available

        Text buttonText = friendButton.GetComponentInChildren<Text>();
        buttonText.text = friend.TitleDisplayName; // Use the 'Username' property of PlayerProfile


        friendButton.onClick.AddListener(() => OnFriendButtonClick(friend));
    }

    void OnFriendButtonClick(FriendInfo friend)
    {
        Debug.Log("Button clicked for friend: " + friend.TitleDisplayName);

        specificChat.instance.chatObject.SetActive(true);
        specificChat.instance.SubscribeToRoomChatChannel(friend.TitleDisplayName);
        // Handle button click for the specific friend (e.g., send an invitation, view profile, etc.)
    }
}
