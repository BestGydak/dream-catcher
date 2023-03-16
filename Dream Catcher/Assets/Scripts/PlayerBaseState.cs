using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayerBaseState
{
    public abstract void EnterState(PlayerStateManager player);
    public abstract void UpdateState(PlayerStateManager player);
    public abstract void OnMove(InputValue value);
    public abstract void OnFire(InputValue value);
    public abstract void OnAdditionalFire(InputValue value);
}
