using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour,ISaveable
{
    private Dictionary<string, bool> miniGameDict = new Dictionary<string, bool>();
    private GameController currentGame;
    private int gameWeek;
    private void OnEnable()
    {
        EventHandler.AfterScenceloadEvent += OnAfterScenceloadEvent;
        EventHandler.GamePassEvent += OnGamePassEvent;
        EventHandler.StartNewGameEvent += OnStartNewGameEvent;

    }

    private void OnDisable()
    {
        EventHandler.AfterScenceloadEvent -= OnAfterScenceloadEvent;
        EventHandler.GamePassEvent -= OnGamePassEvent;
        EventHandler.StartNewGameEvent += OnStartNewGameEvent;

    }

    private void OnStartNewGameEvent(int gameweek)
    {
        this.gameWeek = gameweek;
        miniGameDict.Clear();
    }

    void Start()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
        EventHandler.CallGameStateChangeEvent(GameState.Play);
        ISaveable saveable = this;
        saveable.SaveableRegister();
    }
    private void OnAfterScenceloadEvent()
    {
        foreach(var miniGame in FindObjectsOfType<MiniGame>())
        {
            if(miniGameDict.TryGetValue(miniGame.gameName,out bool isPass))
            {
                miniGame.isPass = isPass;
                miniGame.UpdateMiniGameState();
            }
        }

        currentGame = FindObjectOfType<GameController>();
        currentGame?.SetGameWeekData(gameWeek);
    }

    private void OnGamePassEvent(string gameName)
    {
        miniGameDict[gameName] = true;
    }

    public GameSaveData GenerateSaveData()
    {
        GameSaveData saveData= new GameSaveData();
        saveData.gameWeek = this.gameWeek;
        saveData.miniGameDict = this.miniGameDict;
        return saveData;
    }

    public void RestoreGameData(GameSaveData saveData)
    {
        this.gameWeek = saveData.gameWeek;
        this.miniGameDict = saveData.miniGameDict;
    }
}
