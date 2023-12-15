using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitScript : MonoBehaviour
{
    public GameObject exitScreen;
    
    private void Update()
    {

        if (SceneManager.GetActiveScene().name == "MENU")
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                exitScreen.SetActive(true);
            }
        }
    }
}
