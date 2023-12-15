using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cashbaazi.App.Common
{
    public class ISCREEN : MonoBehaviour
    {
        public SCREEN_TYPE screenType;
        [SerializeField] CanvasGroup mcanvas;

        public virtual void Show()
        {
            
            if (gameObject.activeInHierarchy)
                return;

            if (iTween.Count(this.gameObject) > 0)
                iTween.Stop(this.gameObject);

            gameObject.SetActive(true);

            iTween.ValueTo(this.gameObject, iTween.Hash
                (
                    "from", mcanvas.alpha,
                    "to", 1,
                    "time", Core.Screen_FadeTime,
                    "easetype", Core.Screen_FadeAnimation,
                    "onupdate", "UpdateCanvasAlpha"
                ));
        }
        public virtual void Hide()
        {
            if (iTween.Count(this.gameObject) > 0)
                iTween.Stop(this.gameObject);

            iTween.ValueTo(this.gameObject, iTween.Hash
                (
                    "from", mcanvas.alpha,
                    "to", 0,
                    "time", Core.Screen_FadeTime,
                    "easetype", Core.Screen_FadeAnimation,
                    "onupdate", "UpdateCanvasAlpha",
                    "oncomplete", "HideComplete"
                ));
        }
        void UpdateCanvasAlpha(float value)
        {
            mcanvas.alpha = value;
        }
        void HideComplete()
        {
            gameObject.SetActive(false);
        }
    }

}