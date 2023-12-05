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

    [Header("�ٶȲ���")]
    public float MaxSpeed;
    public float moveSpeed;
    public float jumpForce;

    #region ״̬����
    public bool isGrounded;
    public bool isFalling;
    public bool isLand;

    public float LandTimer;
    public float JumpTime;
    public float MinJumpTime;

    [SerializeField] public AnimationCurve MoveSpeedCurve;
    [SerializeField] public AnimationCurve JumpSpeedCurve;
    [Header("�����ʱ")]
     public float DelayTime;
    #endregion
}