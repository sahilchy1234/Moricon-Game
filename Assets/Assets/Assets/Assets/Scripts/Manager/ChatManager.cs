using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using PubnubApi;
using PubnubApi.Unity;
using Photon.Pun;
using System.Collections.Generic;

public class ChatManager : MonoBehaviourPun
{
    private PNConfiguration pnConfiguration;
    private Pubnub pubnub;
    private string channelPrefix = "room_chat_";

    // Add UI elements (InputField, Text, ScrollRect) as public fields
    public InputField messageInput;
    public Text chatText;
    public ScrollRect scrollRect;

    private string currentRoomChatChannel;

    void Start()
    {
        // InitializePubNub();
    }

    public void InitializePubNub()
    {

        chatText.text = "";
        PNConfiguration pnConfiguration = new PNConfiguration(new UserId("myUniqueUserId"));
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

        // PNResult<PNSubscribeResult> subscribeResult = pubNub.Subscribe()
        //     .Channels(new List<string> {  })
        //     .Execute();


        pubnub.Subscribe<string>()
.Channels(new string[] {
        // subscribe to channels
        currentRoomChatChannel
 })
.Execute();

        // if (!subscribeResult.Status.Error)
        // {
        //     Debug.Log("Subscribed to room chat channel successfully");
        // }
        // else
        // {
        //     Debug.LogError("Error subscribing to room chat channel: " + subscribeResult.Status.Error);
        // }

        // pubnub.SubscribeCallback += (sender, e) =>
        // {
        //     if (e.MessageResult != null)
        //     {
        //         string senderInfo = e.MessageResult.Publisher;
        //         string message = e.MessageResult.Payload.ToString();
        //         ReceiveMessage(senderInfo, message);
        //     }
        // };
    }

    public void SendMessageToRoom(string message)
    {
        message = messageInput.text;
        if (!string.IsNullOrEmpty(message))
        {
            string playerName = PhotonNetwork.NickName;
            string fullMessage = $"{playerName}: {message}";





            pubnub.Publish()
             .Message(fullMessage)
                .Channel(currentRoomChatChannel)
.Execute(new PNPublishResultExt(
                    (result, status) =>
                    {
                        if (!status.Error)
                        {
                            messageInput.text = ""; // Clear the input field after sending the message
                            Debug.Log("Message sent successfully");
                        }
                        else
                        {
                            Debug.LogError("Error sending message: " + status.Error);
                        }
                    }));
        }
    }

    void ReceiveMessage(Pubnub pn, PNMessageResult<object> messageResult)
    {
        string senderInfo = messageResult.Publisher;
        string message = messageResult.Message.ToString();
        DisplayMessage($"{senderInfo}: {message}");
    }



    void DisplayMessage(string message)
    {
        chatText.text += message + "\n";

        // Ensure the scroll rect always displays the latest message
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
    }
}
