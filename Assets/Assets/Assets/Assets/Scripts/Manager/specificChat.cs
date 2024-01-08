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
using System;

using UnityEngine.SceneManagement;

public class specificChat : MonoBehaviour
{

    [SerializeField] Screen_Common commonScreen;

    public GameObject friendRequestbox;
    public Text requestedPlayerText;

    public bool isPrivateRoomNeed;

    public static specificChat instance;
    public GameObject chatObject;
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


    private void Awake()
    {
        instance = this;
    }
    void OnEnable()
    {
        InitializePubNub();
    }

    public void InitializePubNub()
    {
        Debug.Log("Trying to initialize Chat........");

        pnConfiguration = new PNConfiguration(new UserId(AppManager.instance.Get_PlayerData().name.ToString()));
        pnConfiguration.PublishKey = "pub-c-f8802aef-ed9f-4ff4-9e9f-ce9791a52a35";
        pnConfiguration.SubscribeKey = "sub-c-e46578df-0367-45d5-bcbd-04ef60c02065";
        pnConfiguration.Secure = true;
        pubnub = new Pubnub(pnConfiguration);

        var listener = new SubscribeCallbackListener();
        pubnub.AddListener(listener);
        listener.onMessage += ReceiveMessage;
    }

    public void SubscribeToRoomChatChannel(string initializerName)
    {
        if (PlayerPrefs.HasKey(initializerName))
        {
            Debug.Log("subscribing to channel with  id" + channelPrefix + PlayerPrefs.GetString(initializerName));
            currentRoomChatChannel = channelPrefix + PlayerPrefs.GetString(initializerName);
            pubnub.Subscribe<string>()
                .Channels(new string[] { currentRoomChatChannel })
                .Execute();

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
    }
    string roomNameToJoin;
    void ReceiveMessage(Pubnub pn, PNMessageResult<object> messageResult)
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "GunsBottlGame" || scene.name == "KnifeHit" || scene.name == "DunkBall" || scene.name == "GunsBottlGame")
        {
            // frienRequestCame = FriendRequestManager.instance.friendRequestPanel;
        }

        string senderColor = "<color=green>";
        string messageColor = "<color=white>";
        string receiverColor = "<color=blue>";

        string senderInfo = messageResult.Publisher;
        string message = messageResult.Message.ToString();
        string localPlayerIdentifier = AppManager.instance.Get_PlayerData().name.ToString();
        bool isLocalPlayerMessage = senderInfo.Equals(localPlayerIdentifier);

        if (isLocalPlayerMessage)
        {
            DisplayMessage($"{senderColor}{senderInfo}:</color> {messageColor}{message}</color>\n");
        }
        else
        {
            DisplayMessage($"{receiverColor}{senderInfo}:</color> {messageColor}{message}</color>\n");
        }





        if (message.Contains("battle_request_from"))
        {
            string extractedString = ExtractStringBetweenBrackets(message, "[", "]");

            if (!message.Contains(AppManager.instance.Get_PlayerData().name))
            {
                friendRequestbox.SetActive(true);
                requestedPlayerText.text = "Game Request from " + extractedString + " would you like to accept?";
            }
            roomNameToJoin = extractedString;

        }
        string extractedString1 = ExtractStringBetweenBrackets(message, "[", "]");

        // Map extracted string to corresponding GAME_TYPE enum
        if (Enum.TryParse(extractedString1, out GAME_TYPE gameType))
        {
            AppManager.instance.Set_BattleType(gameType);
        }

        string extractedAmountString2 = ExtractStringBetweenBrackets(message, "{", "}");

        if (int.TryParse(extractedAmountString2, out int parsedAmount))
        {
            AppManager.instance.battleSetting.amount = parsedAmount;
        }

        string extractedAmountString3 = ExtractStringBetweenBrackets(message, "^", "*");

        if (int.TryParse(extractedAmountString3, out int parsedAmount1))
        {
            AppManager.instance.Set_BattleMaxPlayer(parsedAmount1);
        }

        string extractedAmountString4 = ExtractStringBetweenBrackets(message, "!", "@");




        // if (int.TryParse(extractedAmountString4, out int parsedAmount2))
        // {
        //     AppManager.instance.Set_DeductFrom(0);

        //     if (AppManager.instance.Get_BattleSettings().amount > ApiManager.instance.responce_userdata.MainWallet)
        //     {

        //     }
        //     else
        //     {

        //     }
        // }

    }




    public void SendMessageToRoom(string message)
    {


        message = messageInput.text;

        Debug.Log("SendMessageToRoom");
        if (!string.IsNullOrEmpty(message))
        {
            string playerName = "";
            string fullMessage = $"{playerName}: {message}";

            pubnub.Publish()
                .Message(fullMessage)
                .Channel(currentRoomChatChannel)
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


    public void RequestToPlayerForGameStart()
    {
        isPrivateRoomNeed = true;
    }

    public bool isRequestedFromPrivateRoom;
    public void joinRoomRequestedPlayer()
    {
        ScreenManager.instance.SwitchScreen(SCREEN_TYPE.DEDUCT_BALANCE_FROM, commonScreen.currentScreen.screenType);

        isRequestedFromPrivateRoom = true;
        PhotonManager.instance.privateRoomNameToJoin = roomNameToJoin;
    }



    //_______________________________________________________
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

}
