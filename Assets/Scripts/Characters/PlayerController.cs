using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent agent;

    private Animator anim;

    // 自身变量的获取放在Awake当中
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        // 这个脚本生命周期开始就通过单例模式将MoveToTarget添加到OnMouseClicked事件中去
        MouseManager.Instance.OnMouseClicked += MoveToTarget;
    }

    private void Update()
    {
        SwitchAnimation();
    }

    private void SwitchAnimation()
    {
        anim.SetFloat("Speed", agent.velocity.sqrMagnitude);
    }

    public void MoveToTarget(Vector3 target)
    {
        agent.destination = target;
    }
}
