using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class SmothJump : MonoBehaviour
{
    private List<GroundDetector> groundDetectors;
    private IPlayerState playerState;
    [SerializeField]
    public PlayerDataSpace PlayerDataSpace;
    public PlayerState PlayerState;

    private Vector2 move;

    

    private void Awake()
    {
        PlayerDataSpace.Rigidbody = GetComponent<Rigidbody2D>();
        PlayerDataSpace.Animator = GetComponent<Animator>();
        PlayerDataSpace.Input = new PlayerInputControl();
        PlayerDataSpace.SwitchState = StateTransform;

        groundDetectors = gameObject.GetComponentsInChildren<GroundDetector>().ToList<GroundDetector>();

        PlayerDataSpace.Rigidbody.gravityScale = 0;

        PlayerDataSpace.Input.Player.Jump.started += Jump;
        PlayerDataSpace.Input.Player.Jump.canceled += JumpCancel;
        PlayerDataSpace.Input.Player.Jump.Enable();

        //Input.Player.Move.started += MovePerformed;
        PlayerDataSpace.Input.Player.Move.performed += MovePerformed;
        PlayerDataSpace.Input.Player.Move.canceled += MoveCancel;
        PlayerDataSpace.Input.Player.Move.Enable();

        //Rigidbody.drag = MaxSpeed / (moveSpeed - Time.deltaTime);

        playerState = new Idle(PlayerDataSpace);
    }
    private void Update()
    {

        StateControl();

        Move();

    }

    private void StateControl()
    {
        PlayerDataSpace.isGrounded = groundDetectors[0].isDetected || groundDetectors[1].isDetected;

        playerState.Update();

    }


    private void Move()
    {
        
        if (move.x != 0)
        {
            if (move.x < 0)

                transform.localScale = new Vector3(-1, 1, 0);

            else if (move.x > 0)

                transform.localScale = new Vector3(1, 1, 0);

            if (Math.Abs(PlayerDataSpace.Rigidbody.velocity.x) < PlayerDataSpace.MaxSpeed)

                PlayerDataSpace.Rigidbody.AddForce(new Vector3(move.x, 0, 0) * PlayerDataSpace.moveSpeed * PlayerDataSpace.Rigidbody.mass, ForceMode2D.Impulse);

            else

                PlayerDataSpace.Rigidbody.velocity = new Vector3(PlayerDataSpace.MaxSpeed * move.x, PlayerDataSpace.Rigidbody.velocity.y, 0);
        }
    }

    #region 挂载到输入系统

    private void Jump(InputAction.CallbackContext context)
    {
        if(PlayerDataSpace.isGrounded)
            StateTransform(PlayerState.Jump);
    }

    private void JumpCancel(InputAction.CallbackContext context)
    {
        if (PlayerDataSpace.Rigidbody.velocity.y < 0)
            return;
        PlayerDataSpace.Rigidbody.velocity = new Vector2(PlayerDataSpace.Rigidbody.velocity.x, 0);
        playerState.Exit();

    }

    private void MovePerformed(InputAction.CallbackContext context)
    {
        
        move = context.ReadValue<Vector2>();

    }
    private void MoveCancel(InputAction.CallbackContext context)
    {
        move.x = 0;
        PlayerDataSpace.Rigidbody.velocity = new Vector2(0, PlayerDataSpace.Rigidbody.velocity.y);
    }
    #endregion


    public void StateTransform(PlayerState State)
    {
        PlayerState = State;
        switch (State)
        {
            case PlayerState.Jump:
                
                playerState = new IJump(PlayerDataSpace);
                break;
            case PlayerState.Land:
                
                playerState = new ILand(PlayerDataSpace);
                break;
            case PlayerState.Idle:

                playerState = new Idle(PlayerDataSpace);
                break;
            case PlayerState.Move:

                playerState = new IMove(PlayerDataSpace);
                break;
            default:
                throw new NotImplementedException("No this State");
                
        }

        playerState.Enter();
    }


}
