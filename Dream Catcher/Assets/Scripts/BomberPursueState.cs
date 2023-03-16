using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberPursueState : EnemyBaseState
{
    public override void EnterState(EnemyBaseStateManager enemy, object param = null)
    {
        
    }

    public override void UpdateState(EnemyBaseStateManager enemy)
    {
        var bomber = (BomberStateManager) enemy;
        if(bomber.Player == null) return;
        if(bomber.CloserEnemiesNearby.Count == 0)
        {
            bomber.RigidBody.MovePosition(Vector2.MoveTowards(bomber.transform.position, bomber.Player.transform.position, bomber.Speed * Time.fixedDeltaTime));
        }
    }
}
