using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cashbaazi.App.Helper
{
    public class Toast
    {
        public static void ShowToast(string message)
        {
#if UNITY_EDITOR
            Debug.Log(message);
#else
            //create a Toast class object
            AndroidJavaClass toastClass =
                        new AndroidJavaClass("android.widget.Toast");

            //create an array and add params to be passed
            object[] toastParams = new object[3];
            AndroidJavaClass unityActivity =
              new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            toastParams[0] =
                         unityActivity.GetStatic<AndroidJavaObject>
                                   ("currentActivity");
            toastParams[1] = message;
            toastParams[2] = toastClass.GetStatic<int>
                                   ("LENGTH_LONG");

            //call static function of Toast class, makeText
            AndroidJavaObject toastObject =
                            toastClass.CallStatic<AndroidJavaObject>
                                          ("makeText", toastParams);

            //show toast
            toastObject.Call("show");
#endif
        }
    }
}