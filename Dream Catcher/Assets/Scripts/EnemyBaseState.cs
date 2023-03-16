using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState
{
    public abstract void EnterState(EnemyBaseStateManager enemy, object param = null);

    public abstract void UpdateState(EnemyBaseStateManager enemy);
}
