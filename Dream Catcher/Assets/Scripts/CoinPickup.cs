using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    private bool _isPickedUp = false;
    [SerializeField] private int _value = 1;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        var playerShoot = other.GetComponentInParent<PlayerStateManager>();
        if(playerShoot == null || _isPickedUp) return;
        playerShoot.Coins += _value;
        _isPickedUp = true;
        Destroy(this.gameObject);
    }
}
