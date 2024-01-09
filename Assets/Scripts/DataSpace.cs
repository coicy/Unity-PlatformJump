using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface DataSpace
{

}

[System.Serializable]
public class PlayerDataSpace : DataSpace
{
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
    // public bool isLand;

    public float JumpTime;
    public float MinJumpTime;

    [SerializeField] public AnimationCurve MoveSpeedCurve;
    [SerializeField] public AnimationCurve JumpSpeedCurve;
    [Header("落地延时")]
     public float DelayTime;
    [Header("土狼时间")]
    public float CoyotoTime;
    #endregion
}