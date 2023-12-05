using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState { 
    Jump,
    Land,
    Idle,
    Move

}

public interface IState
{

}

public interface IPlayerState : IState
{
    public void Enter()
    {

    }
    public void Exit() {
    
    }
    public void Update() {
    
    
    }

    
}
public class ILand : IPlayerState
{
    private PlayerDataSpace space;
    private float Timer;

    public ILand(PlayerDataSpace space)
    {
        this.space = space;
    }

    public void Enter()
    {
        space.Input.Player.Move.Disable();
    }

    public void Exit()
    {
        space.SwitchState(PlayerState.Idle);

    }

    public void Update()
    {


        if (space.LandTimer >= space.DelayTime)
        {
            space.isLand = false;
            space.Input.Player.Move.Enable();
            Exit();
        }

    }
}


public class IJump : IPlayerState
{
    private PlayerDataSpace space;
    private float Timer;
    

    public IJump(PlayerDataSpace dataSpace)
    {
        space = dataSpace;
    }


    public void Enter()
    {
    }
    public void Exit() 
    {
        if (Timer < space.MinJumpTime)

            return;
        
        space.SwitchState(PlayerState.Land);   
    }
    public void Update()
    {
        Timer += Time.deltaTime;
        space.Rigidbody.velocity = new Vector2(space.Rigidbody.velocity.x, space.JumpSpeedCurve.Evaluate(Timer));
        Debug.Log(space.JumpSpeedCurve.Evaluate(Timer));
        if(space.isGrounded)
            Exit();
    }
}

public class IMove : IPlayerState 
{
    public PlayerDataSpace space;

    public IMove(PlayerDataSpace dataSpace)
    {
        space = dataSpace;
    }

    public void Enter()
    {

    }

    public void Exit()
    {

    }

    public void Update() {
    
    
    
    }



}

public class Idle : IPlayerState
{
    public PlayerDataSpace space;
    public Idle(PlayerDataSpace space)
    {
        this.space = space;
    }

    public void Enter()
    {

    }

    public void Exit() 
    {
        
    }
    
    public void Update()
    {
        

    }
}
