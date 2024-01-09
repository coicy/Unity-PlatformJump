# Unity-PlatformJump
一个轮子，制作了一个不基于Unity物理系统的平台跳跃机制

**<mark>./Assets/Scripts/SmothJump.cs</mark>**

### 原理
1. 使用一个有限状态机管理角色的跳跃状态
2. 跳跃和横向移动可以同时进行，所以把横向移动单独封装一个函数，而不纳入有限状态机中
3. 跳跃时（或者说获取到Jump输入时），从Idle状态转向Jump状态。
4. 当Jump输入取消时，或者检测到落地时，离开Jump状态，进入Land状态。
5. 当获取到Move输入时，同时获取Move输入的一个Vector2向量记作move，然后为角色的横向速度赋值为move*Speed

**<mark>./Assets/Scripts/IPlayerState.cs</mark>**

### 原理
 在这个文件中包含了角色的有限状态基类和所有其他有限状态。
- 角色状态接口
      包含了进入，退出和逐帧刷新三个方法。以下是继承接口的所有类
    - 跳跃类（IJump）
       在这个类中
        - 在逐帧刷新时，获取PlayerDataSpace中的跳跃速度曲线，逐帧获取曲线的y坐标，即速度，赋值给玩家的刚体速度的y方向；如果检测到玩家落地，则退出状态。
        - 退出状态时，进入落地状态。
    - 落地类（ILand）
       在这个类中
       - 在逐帧刷新时进行一个计时。当计时大于PlayerDataSpace中的落地时间时，退出状态。
       - 退出状态时，进入Idle待机状态。
    - 土狼时间类（ICoyoteTime）
      在这个类中
      - 在逐帧刷新中进行一个计时。当计时大于PlayerDataSpace中的土狼时间时，退出状态。
      - 退出状态时，进入Idle待机状态。
    - 待机类
      在这个类中，什么都不做。这只是一个定义“你的角色什么都没有做”的类
    - 移动类
       跳跃和横向移动可以同时进行，所以把横向移动单独封装一个函数，而不纳入有限状态机中。因而这个类中暂时什么都不做。

**<mark>./Assets/Scripts/DataSpace.cs</mark>**

### 原理
  这个文件包含了一个基类，即DataSpace类，用来存储状态机的共用数据，即一个“白板”；和一个DataSpace的派生类PlayerDataSpace，用来存储角色的状态数据。
  - 基类DataSpace
    什么都不做。这是一个空类。
  - 派生类PlayerDataSpace
    ```C#
    public Rigidbody2D Rigidbody;
    public Animator Animator;
    public PlayerInputControl Input;
    public Action<PlayerState> SwitchState;

    [Header("速度参数")]
    public float MaxSpeed;
    public float moveSpeed;
    public float jumpForce;

    #region 状态控制
    public bool isGrounded;

    public float JumpTime;
    public float MinJumpTime;

    [SerializeField] public AnimationCurve MoveSpeedCurve;
    [SerializeField] public AnimationCurve JumpSpeedCurve;
    [Header("落地延时")]
     public float DelayTime;
    [Header("土狼时间")]
    public float CoyotoTime;
    #endregion
    ```
    以上是一系列变量，用来存储角色的状态数据。

**<mark>./Assets/Scripts/PlayerInputControl.cs</mark>**

### 原理
  这是一个由Unity新输入系统自动生成的文件。详情请参照Unity新输入系统的教程。

**<mark>./Assets/Scripts/GroundDetector.cs</mark>**

### 原理
  这个脚本负责检测角色是否在地面上。通过```Physics2D.OverlapCircle```方法，检测在碰撞器的一定半径范围内有无标记为地面的碰撞体。如果有，那么判定在地面上。