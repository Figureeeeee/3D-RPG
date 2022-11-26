# 3D RPG项目笔记

Unity版本：2021.3.6

## Packages Lists

* Universal RP(Universal Render Pipeline)
* Polybrush
* ProBuilder
* ProGrids
* Cinemachine

## Assets Lists

* Dog Knight PBR Polyart
* FREE Skybox Extended Shader
* Low-Poly Simple Nature Pack
* Some Cursors(bilibili M_Studio)

## 开发日志
### 2022/11/24
* 先前的日志都没写，从今天开始记录一下！
回顾本次开发：
1. 创建项目，导入素材，这些就略过了，引入的包上面也写了，注意要将整个项目升级到URP
2. 使用Probuilder创建地形（主要是为了自定义顶点数量，刷地形的时候会更好），使用Polybrush构建地形（起伏、颜色），刷上一些物品（树、花花草草、石头）
3. 使用Navigation和NavMeshAgent（挂载在Player身上的组件）对场景中的物体进行一个简单的烘焙，表现为Player占有体积，不能穿过树之类的物品（不能穿模，NotWalkable）
4. 然后是脚本实现人物移动，功能实现大致描述：使用NavMeshAgent组件和一个名为MouseManager的CSharp脚本（该脚本挂载在游戏场景中的一个空物体身上）
    MouseManager的逻辑是通过鼠标点击事件获取一个Vector3的参数，这个参数绑定了Player身上的NavMeshAgent组件的destination，可以获取目标点的值，然后在定义一个射线，射线由相机指向destination目标点，并返回一个hitInfo，如果鼠标左键点击到了地面就将值传回给destination，这样就实现了人物移动
5. 通过单例模式去实现向鼠标触发事件中添加行为比如：MoveToTarget
6. 优化了鼠标指针的外观（还是单独设置的好看hhh），以及相机跟随（cinemachine）
7. Animator Blend Tree实现人物动画（站立、走路）
8. 解决一下潜在问题：
    人走到树等障碍物之后会被遮挡住：这里用Shader Graph创建被遮挡后的材质，然后通过urp（通用渲染管线，可编辑）去设置（人物被遮挡时和不被遮挡时的材质），之后可以补一下shader和渲染管线的知识qwq
    被树等障碍物遮挡到的地方无法移动过去：可以将所有树的图层都设置为IgnoreRaycast也可以将树的MeshCollider组件关闭（因为在MouseManager中目标点的位置就是通过collider获取的）
9. 实现了点击Enemy，Player会转向Enemy并移动过去，进行一次攻击的动画，可通过点击地面其他位置提前取消动作
10. 跟随人物的相机增设一个CinemachineFreeLook，可以通过键鼠去改变跟随人物的相机视角
11. 脚本实现Enemy在一定范围内找到Player

### 2022/11/25

1. 继前面Enemy找到Player之前会在一定范围内随机走动，找到Player之后会导航到Player身边并进行攻击动画
2. 完成了人物基本属性和数值以及攻击属性（实现了继承自ScriptableObject的类）

### 2022/11/26

1. 首先是继前一天完成的人物基本数值之后，实现了攻击数值计算，并且在当前生命值为0时播放死亡动画，Enemy在播放完死亡动画的若干秒后消失，Player死亡后倒地

2. 通过范型去实现单例模式，然后通过接口实现观察者模式的订阅和广播

   > 这里写一下对范型实现单例模式和广播的个人理解：
   >
   > * 单例模式就是指只会创建一次对象的设计模式，并且确保一个类只有一个实例，并且会自行实例化（私有的构造方法，且成员变量和方法需是静态的）
   > * 一个项目中有许多地方需要用到单例模式，如一些Manager，本项目中用到了MouseManager和GameManager
   > * 观察者模式真的是很方便呀，一些信息需要传达给多个对象，可以让这些对象都实现同一个接口，并且在生命周期的OnEnable和OnDisable中将他们添加到和删除自观察者列表，当需要传递信息时，可以对列表中的所有观察者都实现相关方法

3. 还要提到一点就是感觉Debug.Log比断点好用hhh，有些bug都不知道在哪打断点，直接在对应位置Debug就好了嘿嘿