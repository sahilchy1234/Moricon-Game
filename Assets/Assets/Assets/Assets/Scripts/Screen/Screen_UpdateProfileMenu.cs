using Cashbaazi.App.Common;
using Cashbaazi.App.Helper;
using Cashbaazi.App.Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cashbaazi.App.Screen
{
    public class Screen_UpdateProfileMenu : ISCREEN
    {
        [Space(20)]
        [SerializeField] Toggle[] Toggle_Avatars;

        [Space]
        [SerializeField] TMP_InputField Txt_Username;
        [SerializeField] TMP_InputField Txt_Email;
       

        [Space]
        [SerializeField] Button Btn_UpdateProfile;
        [SerializeField] Button Btn_Back;

        private void Start()
        {
            Btn_UpdateProfile.onClick.AddListener(OnClick_UpdateProfile);
            Btn_Back.onClick.AddListener(OnClick_Back);
        }

        public override void Show()
        {
            Txt_Username.text = AppManager.instance.Get_PlayerData().name;
            Txt_Email.text = AppManager.instance.Get_PlayerData().email;
            
            int selectedAvatarIndex = AppManager.instance.Get_PlayerAvatarIndex();

            for (int i = 0; i < Toggle_Avatars.Length; i++)
            {
                Toggle_Avatars[i].transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = AvatarManager.instance.Get_AvatarSprite(i);
                Toggle_Avatars[i].isOn = i == selectedAvatarIndex;
            }

            base.Show();

            ScreenManager.instance.AddScreenToStack(this);
        }

        private void OnClick_UpdateProfile()
        {
            if (string.IsNullOrEmpty(Txt_Username.text))
            {
                Toast.ShowToast("Enter valid username");
                return;
            }
            if (string.IsNullOrEmpty(Txt_Email.text))
            {
                Toast.ShowToast("Enter valid email id");
                return;
            }



            int avatarIndex = System.Array.FindIndex(Toggle_Avatars, x => x.isOn);
            ApiManager.instance.API_UpdateProfile(Txt_Username.text, Txt_Email.text, avatarIndex, string.Empty,
                () =>
                {
                    Toast.ShowToast("Profile Updated Successfully");
                    ScreenManager.instance.SwitchScreen(SCREEN_TYPE.USER_PROFILE, this.screenType);
                },
                () =>
                {
                    Toast.ShowToast("Username Already Found,Try Another!");
                });
        }
        private void OnClick_Back()
        {
            ScreenManager.instance.SwitchScreen(SCREEN_TYPE.USER_PROFILE, this.screenType);
        }
    }
}