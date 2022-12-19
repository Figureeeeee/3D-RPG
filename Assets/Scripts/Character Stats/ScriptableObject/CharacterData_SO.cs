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
    public int killPoint; // ��ɱ�÷֡���ɱ��õľ���ֵ

    [Header("Level")]
    public int currentLevel;
    public int maxLevel;
    public int baseExp;
    public int currentExp;
    public float levelBuff; // ������õ�����

    
    // �ȼ�������levelBuff�����棬ÿ��һ�������൱��levelBuff�ĳ̶�
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
        // ���������ݷ���
        // currentLevel = Math.Clamp(currentLevel + 1, 0, maxLevel);
        currentLevel = Math.Min(currentLevel + 1, maxLevel);
        baseExp += (int)(baseExp * LevelMultiplier);

        maxHealth = (int)(maxHealth * LevelMultiplier);
        currentHealth = maxHealth;
        // Debug.Log("LEVEL UP! CurrentLevel:" + currentLevel + " Max Health:" + maxHealth);
    }
}
