using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

public class FriendRequestManager : MonoBehaviour
{
 
    public static FriendRequestManager instance;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        instance = this;
    }

    public GameObject[] playersObject; 


    public void SendFriendRequest(string name)
    {
        string friendUsername = name;

        if (string.IsNullOrEmpty(friendUsername))
        {
            Debug.LogWarning("Please enter a friend's username.");
            return;
        }

        PlayFabClientAPI.AddFriend(new AddFriendRequest
        {
            FriendTitleDisplayName = friendUsername,
        }, OnSendFriendRequestSuccess, OnSendFriendRequestFailure);
    }

    void OnSendFriendRequestSuccess(AddFriendResult result)
    {
        Debug.Log("Friend request sent successfully");
        // InitializeFriendRequests();
    }

    void OnSendFriendRequestFailure(PlayFabError error)
    {
        Debug.LogError("Failed to send friend request: " + error.ErrorMessage);
    }

    public void AcceptFriendRequest()
    {
        ChatManager.instance.AcceptFriendRequest();
    }


}
