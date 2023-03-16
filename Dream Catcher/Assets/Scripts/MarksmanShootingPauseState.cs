using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarksmanShootingPauseStage : EnemyBaseState
{
    float pauseTime;
    public override void EnterState(EnemyBaseStateManager enemy, object param = null)
    {
        var marksman = (MarksmanStateManager) enemy;
        pauseTime = (float) param;
    }

    public override void UpdateState(EnemyBaseStateManager enemy)
    {
        var marksman = (MarksmanStateManager) enemy;
        if(pauseTime > 0)
        {
            pauseTime -= Time.fixedDeltaTime;
            marksman.Chase();
        }
        else
        {
            marksman.SwitchState(marksman.ChaseState);
        }
    }
}
