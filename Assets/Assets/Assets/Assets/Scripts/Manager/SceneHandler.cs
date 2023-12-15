using Cashbaazi.App.Helper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Cashbaazi.App.Common
{
    public class SceneHandler : Singleton<SceneHandler>
    {
        public void SwitchScene(string _stype)
        {
            StartCoroutine(LoadScene(_stype));
        }

        IEnumerator LoadScene(string _stype)
        {
            Loading.instance.ShowLoading();

            AsyncOperation opr = SceneManager.LoadSceneAsync(_stype.ToString());
            if (!opr.isDone)
                yield return null;

            yield return new WaitForSeconds(.5f);
            Loading.instance.HideLoading();
        }
    }

    public enum SCENE_TYPE
    {
        LOGIN,
        MENU,
        GunsBottleGame,
        KnifeHit,
        DunkBall,
        FruitNinja

    }
}