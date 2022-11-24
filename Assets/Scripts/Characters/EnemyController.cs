using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// 看守、巡逻、追击、死亡
public enum EnemyStates { NONE, GUARD, PATROL, CHASE, DEAD }

[RequireComponent(typeof(NavMeshAgent))]

public class EnemyController : MonoBehaviour
{
    public EnemyStates enemyStates;
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        SwitchStates();
    }

    void SwitchStates()
    {
        switch(enemyStates)
        {
            case EnemyStates.GUARD:
                break;
            case EnemyStates.PATROL:
                break;
            case EnemyStates.CHASE:
                break;
            case EnemyStates.DEAD:
                break;
        }
    }
}
