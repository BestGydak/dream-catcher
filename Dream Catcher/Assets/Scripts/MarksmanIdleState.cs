using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarksmanIdleState : EnemyBaseState
{
    private float _idleTime;
    public override void EnterState(EnemyBaseStateManager enemy, object param = null)
    {
        var marksman = (MarksmanStateManager) enemy;
        _idleTime = marksman.IdleTime;
    }

    public override void UpdateState(EnemyBaseStateManager enemy)
    {
        var marksman = (MarksmanStateManager) enemy;
        if(_idleTime >= 0)
        {
            _idleTime -= Time.fixedDeltaTime;
        }
        else
        {
            var pauseTime = Random.Range(marksman.MinShootPauseAfterKnock,
            marksman.MaxShootPauseAfterKnock);
            marksman.SwitchState(marksman.PauseStage, pauseTime);
        }
    }

}
