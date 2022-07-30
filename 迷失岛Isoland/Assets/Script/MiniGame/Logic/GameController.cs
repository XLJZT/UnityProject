using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class GameController : Singleton<GameController>
{
    //�¼�
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

        Debug.Log("��Ϸ����");
        //����
        EventHandler.CallGamePassEvent(gameData.gameName);
        //�����¼�
        OnFinish?.Invoke();
    }

    public void ResetGame()
    {
        //ֻ��Ҫ����Ѿ���Ϊ����
        foreach (var holder in holdersTransform)
        {
            if (holder.childCount > 0)
                Destroy(holder.GetChild(0).gameObject);
        }
        //������
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

            //��һ��͵�����Ĺ�����Щ�ظ���������ȷ����ȷsprite��
            //������Ϊ������ʹ�ó�����ͬ�����ڳ�ʼ��ʱ��������sprite��
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
