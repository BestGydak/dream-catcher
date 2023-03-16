using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberWaitingState : EnemyBaseState
{
    public override void EnterState(EnemyBaseStateManager enemy, object param = null)
    {
       
    }

    public override void UpdateState(EnemyBaseStateManager enemy)
    {
        var bomber = (BomberStateManager) enemy;
        if(bomber.CloserEnemiesNearby.Count == 0)
        {
            bomber.SwitchState(bomber.PursueState);
        }
    }
}
