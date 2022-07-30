using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
    }
    public void ContinueGame()
    {
        //加载游戏进度
    }

    public void GoBackToMenu()
    {
        var currentScence = SceneManager.GetActiveScene().name;
        TransitionManager.Instance.Transition(currentScence, "Menu");

        //保存游戏进度
    }

    public void StartGameweek(int gameweek)
    {
        EventHandler.CallStartNewGameEvent(gameweek);
    }
} 
