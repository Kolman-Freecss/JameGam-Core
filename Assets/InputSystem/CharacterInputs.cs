using System;
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
    public bool escape;
    public bool interact;

    public event Action<bool> OnEscapeTrigger;
    public event Action<bool> OnRightClickTrigger;
    
    public event Action<bool> OnInteractTrigger;

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
        LeftClickInput(value.isPressed);
    }
    
    public void OnRightClick(InputValue value)
    {
        RightClickInput(value.isPressed);
    }
    
    public void OnEscape(InputValue value)
    {
        EscapeInput(value.isPressed);
    }
    
    public void OnInteract(InputValue value)
    {
        InteractInput(value.isPressed);
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
        OnRightClickTrigger?.Invoke(rightClick);
    }
    
    public void EscapeInput(bool newEscapeState)
    {
        escape = newEscapeState;
        OnEscapeTrigger?.Invoke(escape);
    }
    
    public void InteractInput(bool newInteractState)
    {
        interact = newInteractState;
        OnInteractTrigger?.Invoke(interact);
    }
    
}