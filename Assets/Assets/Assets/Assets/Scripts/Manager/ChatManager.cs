using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Newtonsoft.Json;
using PubnubApi;
using PubnubApi.Unity;
using Photon.Pun;
using Cashbaazi.App.Screen;
using Cashbaazi.App.Common;
using Cashbaazi.App.Helper;
using Cashbaazi.App.Manager;
using TMPro;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;

public class ChatManager : MonoBehaviourPun
{
    bool flag;
    public static ChatManager instance;

    public string requestePlayerName;
    public GameObject messagePrefab;
    public Transform contentPanel;
    public InputField messageInput;
    public ScrollRect scrollRect;
    public int maxMessages = 20;

    private List<Text> messageTexts = new List<Text>();
    private PNConfiguration pnConfiguration;
    private Pubnub pubnub;
    private string channelPrefix = "room_chat_";
    private string currentRoomChatChannel;

    void Start()
    {
        instance = this;
        // Invoke("InitializePubNub", 2f);
        // InitializePubNub();
    }

    public void InitializePubNub()
    {
        Debug.Log("Trying to initialize Chat........");
        PNConfiguration pnConfiguration = new PNConfiguration(new UserId(AppManager.instance.Get_PlayerData().name.ToString()));
        pnConfiguration.PublishKey = "pub-c-f8802aef-ed9f-4ff4-9e9f-ce9791a52a35";
        pnConfiguration.SubscribeKey = "sub-c-e46578df-0367-45d5-bcbd-04ef60c02065";
        pnConfiguration.Secure = true;
        pubnub = new Pubnub(pnConfiguration);

        var listener = new SubscribeCallbackListener();
        pubnub.AddListener(listener);
        listener.onMessage += ReceiveMessage;
    }

    public void SubscribeToRoomChatChannel(string roomName)
    {
        currentRoomChatChannel = channelPrefix + roomName;
        pubnub.Subscribe<string>().Channels(new string[] { currentRoomChatChannel }).Execute();
    }

    public void SendMessageToRoom(string message)
    {
        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == "GunsBottlGame" || scene.name == "KnifeHit" || scene.name == "DunkBall" || scene.name == "GunsBottlGame")
        {
            // frienRequestCame = FriendRequestManager.instance.friendRequestPanel;
            //  nameText = FriendRequestManager.instance.nameTEXT;
        }

        message = messageInput.text;
        Debug.Log("SendMessageToRoom");

        if (!string.IsNullOrEmpty(message))
        {
            string playerName = "";
            string fullMessage = $"{playerName}: {message}";

            pubnub.Publish().Message(fullMessage).Channel(currentRoomChatChannel)
                .Execute(new PNPublishResultExt((result, status) =>
                {
                    if (!status.Error)
                    {
                        Debug.Log("Message sent successfully");
                    }
                    else
                    {
                        Debug.LogError("Error sending message: " + status.Error);
                    }
                }));
        }
        else
        {
            Debug.LogError("Field Empty");
        }
    }

    void ReceiveMessage(Pubnub pn, PNMessageResult<object> messageResult)
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "GunsBottlGame" || scene.name == "KnifeHit" || scene.name == "DunkBall" || scene.name == "GunsBottlGame")
        {
            // frienRequestCame = FriendRequestManager.instance.friendRequestPanel;
        }

        string senderInfo = messageResult.Publisher;
        string message = messageResult.Message.ToString();
        string localPlayerIdentifier = AppManager.instance.Get_PlayerData().name.ToString();
        bool isLocalPlayerMessage = senderInfo.Equals(localPlayerIdentifier);

        if (isLocalPlayerMessage)
        {
            DisplayMessage($"<color=green>{senderInfo}:</color> <color=white>{message}</color>\n");
        }
        else
        {
            DisplayMessage($"<color=blue>{senderInfo}:</color> <color=white>{message}</color>\n");
        }
    }

    void DisplayMessage(string message)
    {
        if (!message.Contains("friend_request_to"))
        {
            GameObject newMessageObject = Instantiate(messagePrefab, contentPanel);
            Text newMessageText = newMessageObject.GetComponent<Text>();
            newMessageText.text = message;
            messageTexts.Add(newMessageText);

            if (messageTexts.Count > maxMessages)
            {
                Text removedMessage = messageTexts[0];
                messageTexts.RemoveAt(0);
                Destroy(removedMessage.gameObject);
            }

            Canvas.ForceUpdateCanvases();
            scrollRect.verticalNormalizedPosition = 0f;
        }

        Debug.Log(message);

        if (!flag)
        {
            if (message.Contains(AppManager.instance.Get_PlayerData().name.ToString()) && message.Contains("friend_request_to"))
            {
                // Extracting information from the message
                string extractedString = ExtractStringBetweenBrackets(message, "[", "]");
                string extractedString1 = ExtractStringBetweenBrackets(message, "{", "}");
                string extractedString2 = ExtractStringBetweenBrackets(message, "xzc", "xcx").Replace("zc", "");


                // FriendRequestManager.instance.nameTEXT.text = $"Friend request from {extractedString} would you like to accept?";
                requestePlayerName = extractedString;

                PlayerPrefs.SetString(requestePlayerName, extractedString2);
                Debug.Log(requestePlayerName + " " + extractedString2);
                PlayfabManager.instance.SaveData(requestePlayerName, extractedString2);
                AcceptFriendRequest();

                Debug.Log("subscribing to channel with intiazier name" + requestePlayerName);
                specificChat.instance.SubscribeToRoomChatChannel(requestePlayerName);
                specificChat.instance.chatObject.SetActive(true);

            }
        }
        // flag = true;

    }

    string ExtractStringBetweenBrackets(string longString, string startBracket, string endBracket)
    {
        int startIndex = longString.IndexOf(startBracket);
        int endIndex = longString.IndexOf(endBracket);

        if (startIndex != -1 && endIndex != -1 && endIndex > startIndex)
        {
            return longString.Substring(startIndex + 1, endIndex - startIndex - 1);
        }
        return "";
    }

    public void SendFriendRequest(int i)
    {
        flag = false;

        float randomNumber = Random.Range(0, 99999);

        messageInput.text = $"friend_request_to [{AppManager.instance.Get_PlayerData().name}]{{{FriendRequestManager.instance.playersObject[i].transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text}}}xzc" + randomNumber + "xcx";
        SendMessageToRoom($"friend_request_to [{AppManager.instance.Get_PlayerData().name}]{{{FriendRequestManager.instance.playersObject[i].transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text}}}xzc" + randomNumber + "xcx");
        PlayerPrefs.SetString(FriendRequestManager.instance.playersObject[i].transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text, randomNumber.ToString());
        PlayfabManager.instance.SaveData(FriendRequestManager.instance.playersObject[i].transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text, randomNumber.ToString());

        SendFriendRequest(FriendRequestManager.instance.playersObject[i].transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text);
    }

    public void AcceptFriendRequest()
    {
        SendFriendRequest(requestePlayerName);
    }




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
}
