using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBattleState : GameBaseState
{
    public override void EnterState(GameManager manager)
    {
        foreach(var spawn in manager.Spawns)
        {
            spawn.StartSpawn();
        }
    }

    public override void UpdateState(GameManager manager)
    {
        if(manager.Spawns.Count == 0)
        {
            manager.SwitchState(manager.PreparationState);
        }
    }
}
