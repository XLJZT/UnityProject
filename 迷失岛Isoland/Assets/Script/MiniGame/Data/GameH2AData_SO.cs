using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName= "GameH2AData",menuName ="MiniGame/GameH2AData")]
public class GameH2AData_SO : ScriptableObject
{
    [SceneName] public string gameName;
    public List<BallDetails> ballDataList;

    public List<Connections> lineConnections;
    public List<BallName> startBallOrder;
    public BallDetails GetBallDetails(BallName ballName)
    {
        return ballDataList.Find(b => b.ballName == ballName);
    }

}


[System.Serializable]
public class BallDetails
{
    public BallName ballName;
    public Sprite wrongSprite;
    public Sprite rightSprite;
}

[System.Serializable]
public class Connections
{
    public int from;
    public int to;
}
