using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiStick : MonoBehaviour
{
    EnemyBaseStateManager stateManager;
    GameObject player;
    void Awake()
    {
        stateManager = transform.parent.GetComponent<EnemyBaseStateManager>();
        player = FindObjectOfType<PlayerStateManager>()?.gameObject;
    }
    
    private void OnTriggerStay2D(Collider2D other) 
    {
        if(player == null) return;

        var otherEnemy = other.transform.parent;
        if(otherEnemy == null 
            || otherEnemy.GetComponent<EnemyBaseStateManager>() == null) return;

        var fromThisToPlayer = Vector2.Distance(transform.position, player.transform.position);
        var fromOtherToPlayer = Vector2.Distance(other.transform.position, player.transform.position);
        var thisDesiredDistance = transform.parent.GetComponent<EnemyBaseStateManager>().DesiredDistance;
        var otherDesiredDistance = otherEnemy.GetComponent<EnemyBaseStateManager>().DesiredDistance;

        if(fromThisToPlayer > fromOtherToPlayer && thisDesiredDistance >= otherDesiredDistance)
            stateManager.CloserEnemiesNearby.Add(otherEnemy.gameObject);
        else
            stateManager.CloserEnemiesNearby.Remove(otherEnemy.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        var otherEnemy = other.transform.parent;
        if(otherEnemy != null)
            stateManager.CloserEnemiesNearby.Remove(otherEnemy.gameObject);
    }
}
