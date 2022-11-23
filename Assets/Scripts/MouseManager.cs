using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// 序列化
[System.Serializable]
public class EventVector3 : UnityEvent<Vector3> { }

public class MouseManager : MonoBehaviour
{
    // 碰撞信息
    RaycastHit hitInfo;

    // 事件，在Inspector面板将人物NavMashAgent的destination绑定到该事件
    public EventVector3 OnMouseClicked;

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
        }
    }
}
