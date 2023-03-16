using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerSpawnState : SpawnerBaseState
{
    private float _pauseTime;
    private SpawnerStateManager _spawner;
    public override void EnterState(SpawnerStateManager spawner)
    {
        _pauseTime = 0;
        _spawner = spawner;
    }

    public override void UpdateState(SpawnerStateManager spawner)
    {
        if(spawner.MaxEnemies == spawner.LivingEnemies)
        {
            spawner.SwitchState(spawner.WaitingState);
        }
        else if(_pauseTime <= 0 && spawner.SpawnedEnemies < spawner.EnemiesOverall)
        {
            SpawnEnemy(spawner);
            _pauseTime = Random.Range(spawner.MinSpawnTime, spawner.MaxSpawnTime);
        }
        else
        {
            _pauseTime -= Time.fixedDeltaTime;
        }
    }

    private void SpawnEnemy(SpawnerStateManager spawner)
    {
        var newEnemy = Object.Instantiate(spawner.Enemy, spawner.transform.position, Quaternion.identity);
        spawner.LivingEnemies += 1;
        newEnemy.GetComponent<Health>().OnDeath += HandleDeath;
        spawner.SpawnedEnemies += 1;
    }

    private void HandleDeath(GameObject enemy)
    {
        _spawner.LivingEnemies -= 1;
        _spawner.DeadEnemies += 1;
    }
}
