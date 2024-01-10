using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cashbaazi.App.Common;
using TMPro;
public class buttonFriendRequest : MonoBehaviour
{
    public ScreenManager _script;
    public specificChat chat_script;
    public int i;


    public void SendRequest()
    {
        if (!PlayerPrefs.HasKey(FriendRequestManager.instance.playersObject[i].transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text))
        {
            _script.SendRequestFriendship(i);
        }

        ChatInfoGame.instance.OnFriendButtonClick((FriendRequestManager.instance.playersObject[i].transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text));
        Invoke("StartChat", 2f);
    }

    void StartChat()
    {
        chat_script.SubscribeToRoomChatChannel(PlayerPrefs.GetString(FriendRequestManager.instance.playersObject[i].transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text));
        chat_script.chatObject.SetActive(true);

    }
}
