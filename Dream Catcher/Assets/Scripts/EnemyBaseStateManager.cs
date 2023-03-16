using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseStateManager : MonoBehaviour
{
    [HideInInspector] public float DesiredDistance;
    public EnemyBaseState CurrentState;
    public HashSet<GameObject> CloserEnemiesNearby = new HashSet<GameObject>();
    public abstract void SwitchState(EnemyBaseState state, object param = null);
    public abstract void HandleKnock(Vector2 knockDirection);
}
