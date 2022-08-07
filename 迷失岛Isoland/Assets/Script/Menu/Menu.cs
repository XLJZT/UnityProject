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
        //������Ϸ����
        SaveLoadManager.Instance.Load();
    }

    public void GoBackToMenu()
    {
        var currentScence = SceneManager.GetActiveScene().name;
 
        TransitionManager.Instance.Transition(currentScence, "Menu");
        //������Ϸ����
        SaveLoadManager.Instance.Save();

    }

    public void StartGameweek(int gameweek)
    {
        EventHandler.CallStartNewGameEvent(gameweek);
    }
} 
