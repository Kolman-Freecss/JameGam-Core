using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class CharacterInputs : MonoBehaviour
{
    [Header("Character Input Values")] public Vector2 move;

    public bool jump;
    public bool sprint;
    public bool leftClick;
    public bool rightClick;

    public void OnMove(InputValue value)
    {
        MoveInput(value.Get<Vector2>());
    }

    public void OnJump(InputValue value)
    {
        JumpInput(value.isPressed);
    }
    
    public void OnSprint(InputValue value)
    {
        SprintInput(value.isPressed);
    }
    
    public void OnLeftClick(InputValue value)
    {
        leftClick = value.isPressed;
    }
    
    public void OnRightClick(InputValue value)
    {
        rightClick = value.isPressed;
    }

    public void MoveInput(Vector2 newMoveDirection)
    {
        move = newMoveDirection;
    }

    public void JumpInput(bool newJumpState)
    {
        jump = newJumpState;
    }
    
    public void SprintInput(bool newSprintState)
    {
        sprint = newSprintState;
    }
    
    public void LeftClickInput(bool newLeftClickState)
    {
        leftClick = newLeftClickState;
    }
    
    public void RightClickInput(bool newRightClickState)
    {
        rightClick = newRightClickState;
    }
    
}