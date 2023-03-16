using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerWaitingState : SpawnerBaseState
{
    public override void EnterState(SpawnerStateManager spawner)
    {
        
    }

    public override void UpdateState(SpawnerStateManager spawner)
    {
        if(spawner.LivingEnemies < spawner.MaxEnemies)
        {
            spawner.SwitchState(spawner.SpawnState);
        }
    }
}
