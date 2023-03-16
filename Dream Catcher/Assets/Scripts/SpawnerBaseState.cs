using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnerBaseState 
{
    public abstract void EnterState(SpawnerStateManager spawner);

    public abstract void UpdateState(SpawnerStateManager spawner);
}
