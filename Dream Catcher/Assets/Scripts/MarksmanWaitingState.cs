using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarksmanWaitingState : EnemyBaseState
{
    public override void EnterState(EnemyBaseStateManager enemy, object param = null)
    {
        
    }

    public override void UpdateState(EnemyBaseStateManager enemy)
    {
        var marksman = (MarksmanStateManager) enemy;
        if(marksman.CloserEnemiesNearby.Count > 0)
        {
            marksman.Shoot();
        }
        else
        {
            marksman.SwitchState(marksman.ChaseState);
        }
    }
}
