using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : Singleton<TransitionManager>
{
    //初始加载的场景
    [SceneName] public string startScence;

    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration;
    bool isfade;
    //用于控制对话时不可以跳转页面  跳转页面后对话框可能会存在
    bool canTransition;

    private void OnEnable()
    {
        EventHandler.GameStateChangeEvent += OnGameStateChangeEvent;
        EventHandler.StartNewGameEvent += OnStartNewGameEvent;
    }
    private void OnDisable()
    {
        EventHandler.GameStateChangeEvent -= OnGameStateChangeEvent;
        EventHandler.StartNewGameEvent += OnStartNewGameEvent;
    }

    private void OnStartNewGameEvent(int obj)
    {
        StartCoroutine(TransitionToScene("Menu", startScence));
    }

    private void OnGameStateChangeEvent(GameState gameState)
    {
        canTransition = gameState == GameState.Play;
    }

    //private void Start()
    //{
    //    //加载初始场景startScence
    //    StartCoroutine(TransitionToScene(string.Empty, startScence));
    //}
    public void Transition(string from,string to)
    {
        if(!isfade&&canTransition)
            StartCoroutine(TransitionToScene(from,to));
    }

    IEnumerator TransitionToScene(string from, string to)
    {
        yield return Fade(1);
        //如果from为空，则跳过下面两句
        if (from != string.Empty)
        {
            //触发保存场景物品
            EventHandler.CallBeforScenceUnloadEvent();

            yield return SceneManager.UnloadSceneAsync(from);
        }
        yield return SceneManager.LoadSceneAsync(to,LoadSceneMode.Additive);

        Scene newScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(newScene);

        //触发加载场景的物品
        EventHandler.CallAfterScenceloadEvent();
        yield return Fade(0);
    }

    IEnumerator Fade(float targetAlpha)
    {
        isfade = true;
        fadeCanvasGroup.blocksRaycasts = true;

        float speed = Mathf.Abs(fadeCanvasGroup.alpha - targetAlpha) / fadeDuration;
        while (!Mathf.Approximately(fadeCanvasGroup.alpha, targetAlpha))
        {
            fadeCanvasGroup.alpha = Mathf.MoveTowards(fadeCanvasGroup.alpha, targetAlpha, speed * Time.deltaTime);
            yield return null;
        }
       
        fadeCanvasGroup.blocksRaycasts = false;
        isfade = false;
    }
}
