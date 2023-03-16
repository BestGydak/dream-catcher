using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberIdleState : EnemyBaseState
{
    private float _idleTime;

    public override void EnterState(EnemyBaseStateManager enemy, object param = null)
    {
        var bomber = (BomberStateManager) enemy;
        _idleTime = bomber.IdleTime;
    }


    public override void UpdateState(EnemyBaseStateManager enemy)
    {
        var bomber = (BomberStateManager) enemy;
        if(_idleTime > 0)
        {
            _idleTime -= Time.fixedDeltaTime;
        }
        else
        {
            bomber.SwitchState(bomber.PursueState);
        }
    }
}
