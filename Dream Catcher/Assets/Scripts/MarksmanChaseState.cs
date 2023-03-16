using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarksmanChaseState : EnemyBaseState
{
    public override void EnterState(EnemyBaseStateManager enemy, object value = null)
    {
        
    }

    public override void UpdateState(EnemyBaseStateManager enemy)
    {
        var marksman = (MarksmanStateManager) enemy;
        
        marksman.Chase();
        marksman.Shoot();
    }
}
