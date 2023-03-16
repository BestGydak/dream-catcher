using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePreparationState : GameBaseState
{
    
    public override void EnterState(GameManager manager)
    {
        manager.CurrentRound += 1;
        manager.AllSpawnCount = manager.BomberSpawnsCount +  manager.MarksmanSpawnsCount;
        manager.CurrentSpawnCount = manager.AllSpawnCount;
        PlaceSpawners(manager, manager.BomberSpawner.GetComponent<SpawnerStateManager>(), manager.BomberSpawnsCount);
        PlaceSpawners(manager, manager.MarksmanSpawner.GetComponent<SpawnerStateManager>(), manager.MarksmanSpawnsCount);
        if(manager.CurrentRound % manager.RoundsToIncreaseBomberSpawners == 0 && manager.CurrentRound != 0)
        {
            manager.BomberSpawnsCount += manager.BomberSpawnIncrement;
        }
        if(manager.CurrentRound % manager.RoundsToIncreaseMarksmanSpawners == 0 && manager.CurrentRound != 0)
        {
            manager.MarksmanSpawnsCount += manager.MarksmanSpawnIncrement;
        }
    }

    public override void UpdateState(GameManager manager)
    {
        
    }

    private void PlaceSpawners(GameManager manager, SpawnerStateManager spawner, int count)
    {
        for(var i = 0; i < count; i++)
        {
            var newSpawn = SetupNewSpawn(manager, spawner);
            newSpawn.OnEnd += (spawner) => manager.Spawns.Remove(newSpawn);
            manager.Spawns.Add(newSpawn);
        }
    }

    private SpawnerStateManager SetupNewSpawn(GameManager manager, SpawnerStateManager spawner)
    {
        var newX = Random.Range(manager.MinBounds.x, manager.MaxBounds.x);
        var newY = Random.Range(manager.MinBounds.y, manager.MaxBounds.y);
        var newPosition = new Vector2(newX, newY);
        return Object.Instantiate(spawner, newPosition, Quaternion.identity); 
    }

}
