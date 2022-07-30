using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class GameController : Singleton<GameController>
{
    //事件
    public UnityEvent OnFinish;

    public GameH2AData_SO gameData;
    public GameH2AData_SO[] gameDataArray;
    public GameObject lineParent;
    public LineRenderer linePerfab;
    public Ball ballPerfab;
    public Transform[] holdersTransform;

    private void OnEnable()
    {
        EventHandler.CheckGameStateEvent += OnCheckGameStateEvent;
    }
    private void OnDisable()
    {
        EventHandler.CheckGameStateEvent -= OnCheckGameStateEvent;

    }



    private void Start()
    {
        DrawLine();
        CreateBall();
    }

    private void OnCheckGameStateEvent()
    {
        foreach (var ball in FindObjectsOfType<Ball>())
        {
            if (!ball.isMatch)
                return;
        }

        Debug.Log("游戏结束");
        //打开门
        EventHandler.CallGamePassEvent(gameData.gameName);
        //启动事件
        OnFinish?.Invoke();
    }

    public void ResetGame()
    {
        //只需要清除已经归为的球
        foreach (var holder in holdersTransform)
        {
            if (holder.childCount > 0)
                Destroy(holder.GetChild(0).gameObject);
        }
        //生成球
        CreateBall();
    }

    public void DrawLine()
    {
        foreach (var connections in gameData.lineConnections)
        {
            var line = Instantiate(linePerfab, lineParent.transform);
            line.SetPosition(0, holdersTransform[connections.from].position);
            line.SetPosition(1, holdersTransform[connections.to].position);

            holdersTransform[connections.from].GetComponent<Holder>().linkHolders.Add(holdersTransform[connections.to].GetComponent<Holder>());
            holdersTransform[connections.to].GetComponent<Holder>().linkHolders.Add(holdersTransform[connections.from].GetComponent<Holder>());
        }
    }
    public void CreateBall()
    {
        for (int i = 0; i < gameData.startBallOrder.Count; i++)
        {
            if (gameData.startBallOrder[i] == BallName.None)
            {
                holdersTransform[i].GetComponent<Holder>().isEmpty = true;
                continue;
            }

            var ball = Instantiate(ballPerfab, holdersTransform[i]);

            //第一句和第三句的功能有些重复，设置正确或不正确sprite；
            //但是因为两功能使用场景不同所以在初始化时设置两遍sprite；
            holdersTransform[i].GetComponent<Holder>().CheckBall(ball);
            holdersTransform[i].GetComponent<Holder>().isEmpty = false;
            ball.SetupBall(gameData.GetBallDetails(gameData.startBallOrder[i]));
        }
    }

    public void SetGameWeekData(int week)
    {
        gameData = gameDataArray[week];
        DrawLine();
        CreateBall();
    }
}
