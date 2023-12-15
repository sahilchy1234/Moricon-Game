using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cashbaazi.Game.Common;

namespace Cashbaazi.App.Helper
{
    public class Timer
    {
        public static void Schedule(MonoBehaviour behaviour, float delay, Action callback)
        {
            behaviour.StartCoroutine(DoAction(delay, callback));
        }

        static IEnumerator DoAction(float delay, Action callback)
        {
            yield return new WaitForSeconds(delay);
            if (callback != null)
                callback();
        }
    }
}