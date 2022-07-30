using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingleTon<GameManager>
{
    public CharacterStats playerStats;

    List<IEndGameObserver> endGameObeverses = new List<IEndGameObserver>();
    protected override void Awake()
    {
        base.Awake();
    }
    public void RigisterPlayer(CharacterStats characterStats)
    {
        playerStats = characterStats;
    }

    public void AddObserver(IEndGameObserver observer)
    {
        endGameObeverses.Add(observer);
    }
    public void RemoveObserver(IEndGameObserver observer)
    {
        endGameObeverses.Remove(observer);
    }

    public void NotifyObservers()
    {
        foreach (var observer in endGameObeverses)
        {
            observer.EndsNotify();
        }
    }
}
