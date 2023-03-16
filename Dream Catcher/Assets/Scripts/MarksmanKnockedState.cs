using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarksmanKnockedState : EnemyBaseState
{
    private float knockTime;

    public override void EnterState(EnemyBaseStateManager enemy, object param = null)
    {
        var marksman = (MarksmanStateManager) enemy;
        knockTime = marksman.KnockTime;
    }

    public override void UpdateState(EnemyBaseStateManager enemy)
    {
        var marksman = (MarksmanStateManager) enemy;   
        if(knockTime >= 0)
        {
            knockTime -= Time.fixedDeltaTime;
            marksman.RigidBody.MovePosition(enemy.transform.position + (Vector3)marksman.KnockDirection * marksman.KnockPower * Time.fixedDeltaTime);
        }
        else
        {
            marksman.SwitchState(marksman.IdleState);
        }
    }
}
