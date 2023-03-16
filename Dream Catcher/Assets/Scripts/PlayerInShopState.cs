using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInShopState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        player.MoveDirection = new Vector2(0, 0);
    }

    public override void UpdateState(PlayerStateManager player)
    {
       
    }

    public override void OnAdditionalFire(InputValue value)
    {
        
    }

    public override void OnFire(InputValue value)
    {
        
    }

    public override void OnMove(InputValue value)
    {
       
    }
}
