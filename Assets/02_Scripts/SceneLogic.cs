using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLogic : MonoBehaviour
{




    void Start()
    {
        Screen.SetResolution(720, 1280, false);
    }

    void Update()
    {
        
    }

    //GotoGameScene
    public void BtnGoToGameScene()
    {
        SceneManager.LoadScene("02GameScene");
    }

    //GoToLobbyScene
    public void BtnGoToLobbyScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("01LobbyScene");
    }
}
