using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEditor.UI;

/*
 * 为了方便后续监听事件，这个地方把拖拽组件的行为用脚本实现
    // 序列化
    [System.Serializable]
    public class EventVector3 : UnityEvent<Vector3> { }
*/

public class MouseManager : Singleton<MouseManager>
{
    // 鼠标指针图像
    public Texture2D point, doorway, attack, target, arrow;

    // 碰撞信息
    RaycastHit hitInfo;

    // 事件，在Inspector面板将人物NavMashAgent的destination绑定到该事件
    public event Action<Vector3> OnMouseClicked;
    // 点击敌人事件
    public event Action<GameObject> OnEnemyClicked;

    protected override void Awake()
    {
        base.Awake();
        //DontDestroyOnLoad(this);
    }

    void Update()
    {
        SetCursorTexture();
        MouseControl();
    }

    // 设置指针样式
    void SetCursorTexture()
    {
        // 获取射线，射线为主相机的local方向与鼠标点击位置的连线
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // 通过射线获取out类型的hitInfo
        if(Physics.Raycast(ray, out hitInfo))
        {
            // 切换鼠标贴图
            switch (hitInfo.collider.gameObject.tag)
            {
                case "Ground":
                    Cursor.SetCursor(target, new Vector2(16, 16), CursorMode.Auto);
                    break;
                case "Enemy":
                    Cursor.SetCursor(attack, new Vector2(16, 16), CursorMode.Auto);
                    break;
                case "Portal":
                    Cursor.SetCursor(doorway, new Vector2(16, 16), CursorMode.Auto);
                    break;
            }
        }
    }

    // 鼠标控制，在获取了hitInfo之后
    void MouseControl()
    {
        // 鼠标左键点击且点击物品的碰撞体不为空（可能会点到地图之外，此时不作反应）
        if (Input.GetMouseButtonDown(0) && hitInfo.collider != null)
        {
            // 如果碰撞信息的碰撞体标签为Ground，就执行
            if (hitInfo.collider.gameObject.CompareTag("Ground"))
                // 鼠标点击事件不为空就将NavMashAgent的destination改为hitInfo的point，即鼠标点击的位置
                OnMouseClicked?.Invoke(hitInfo.point);
            if (hitInfo.collider.gameObject.CompareTag("Enemy"))
                OnEnemyClicked?.Invoke(hitInfo.collider.gameObject);
            if (hitInfo.collider.gameObject.CompareTag("Attackable"))
                OnEnemyClicked?.Invoke(hitInfo.collider.gameObject);
            if (hitInfo.collider.gameObject.CompareTag("Portal"))
                OnMouseClicked?.Invoke(hitInfo.point);
        }
    }
}
