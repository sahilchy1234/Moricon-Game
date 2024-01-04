using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonMovementChecker : MonoBehaviour
{

    public Button[] btn;
    // Start is called before the first frame update
    public void buttonClicked()
    {
        for (int i = 0; i < btn.Length; i++)
        {
            btn[i].interactable = false;
        }

        Invoke("interactableTrueEvent", 1f);
    }

    void interactableTrueEvent()
    {
        for (int i = 0; i < btn.Length; i++)
        {
            btn[i].interactable = true;
        }

    }
}
