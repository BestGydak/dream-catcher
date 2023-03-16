using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberKnockedState : EnemyBaseState
{
    private float _knockTime;
    public override void EnterState(EnemyBaseStateManager enemy, object param = null)
    {
        var bomber = (BomberStateManager) enemy;
        _knockTime = bomber.KnockTime;
    }

    public override void UpdateState(EnemyBaseStateManager enemy)
    {
        var bomber = (BomberStateManager) enemy;
        if(_knockTime > 0)
        {
            _knockTime -= Time.fixedDeltaTime;
            bomber.RigidBody.MovePosition(bomber.transform.position + (Vector3)bomber.KnockDirection * bomber.KnockPower * Time.fixedDeltaTime);
        }
        else
        {
            bomber.SwitchState(bomber.IdleState);
        }
    }
}
