using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//filenameΪ�������ļ����ļ�����menuNameΪ�ļ���Ŀ¼�㼶����
[CreateAssetMenu(fileName = "New Data", menuName = "Character Stats/Data")]

public class Character_SO : ScriptableObject
{
    [Header("Stats Info")]
    public int maxHealth;
    public int currentHealth;
    public int baseDefence;
    public int currentDefence;

    [Header("Kill")]
    public int killPoint;

    [Header("Level")]
    public int currentLevel;
    public int maxLevel;
    public int baseExp;
    public int currentExp;
    public float levelBuff;

    public void UpdateExp(int point)
    {
        currentExp += point;
        if (currentExp >= baseExp)
        {
            currentExp -= baseExp;
            LevelUP();
        }
    }

    private void LevelUP()
    {
        currentLevel = Mathf.Clamp(currentLevel + 1, 0, maxLevel);
        baseExp += (int)(baseExp * LevelMultiplier);
        maxHealth = (int)(maxHealth * LevelMultiplier);
        currentHealth = maxHealth;
    }
    public float LevelMultiplier
    {
        get {return 1+(currentLevel-1)*levelBuff ; }
    }
}
