using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SpawnerStateManager : MonoBehaviour
{
    [SerializeField] public GameObject Enemy;
    
    [Header("Spawn Time")]
    [SerializeField] public float MinSpawnTime = 0.5f;
    [SerializeField] public float MaxSpawnTime = 1;

    [Header("Spawn Properties")]
    [SerializeField] public int MaxEnemies = 3;
    [SerializeField] public int EnemiesOverall = 6;

    [HideInInspector] public int LivingEnemies;
    [HideInInspector] public int DeadEnemies;
    [HideInInspector] public int SpawnedEnemies;

    public event Action<GameObject> OnEnd;

    public SpawnerBaseState CurrentState;
    public SpawnerIdleState IdleState = new SpawnerIdleState();
    public SpawnerWaitingState WaitingState = new SpawnerWaitingState();
    public SpawnerSpawnState SpawnState = new SpawnerSpawnState();

    private void Start() 
    {
        SwitchState(IdleState);
    }

    private void FixedUpdate()
    {
        CurrentState.UpdateState(this);
        CheckSpawn();
    }

    public void SwitchState(SpawnerBaseState state)
    {
        CurrentState = state;
        state.EnterState(this);
    }

    public void StartSpawn()
    {
        SwitchState(SpawnState);
    }

    private void DestroySpawn()
    {
        if(this != null)
            {
                Destroy(this.gameObject);
                OnEnd?.Invoke(this.gameObject);
            } 
    }

    private void CheckSpawn()
    {
        if(DeadEnemies >= EnemiesOverall)
        {
            DestroySpawn();
        }
    }
}
