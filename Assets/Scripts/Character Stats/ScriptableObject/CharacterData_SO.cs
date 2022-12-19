using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "New Data", menuName = "Character Stats/Data")]

public class CharacterData_SO : ScriptableObject
{
    [Header("Stats Info")]
    public int maxHealth;
    public int currentHealth;
    public int baseDefence;
    public int currentDefence;

    [Header("Kill")]
    public int killPoint; // 击杀得分、击杀获得的经验值

    [Header("Level")]
    public int currentLevel;
    public int maxLevel;
    public int baseExp;
    public int currentExp;
    public float levelBuff; // 升级获得的增益

    
    // 等级提升对levelBuff的增益，每升一级提升相当于levelBuff的程度
    public float LevelMultiplier
    {
        get { return 1 + (currentLevel - 1) * levelBuff; }
    }

    public void UpdateExp(int point)
    {
        currentExp += point;

        if(currentExp >= baseExp)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        // 提升的数据方法
        // currentLevel = Math.Clamp(currentLevel + 1, 0, maxLevel);
        currentLevel = Math.Min(currentLevel + 1, maxLevel);
        baseExp += (int)(baseExp * LevelMultiplier);

        maxHealth = (int)(maxHealth * LevelMultiplier);
        currentHealth = maxHealth;
        // Debug.Log("LEVEL UP! CurrentLevel:" + currentLevel + " Max Health:" + maxHealth);
    }
}
