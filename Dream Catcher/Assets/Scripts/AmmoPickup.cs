using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    private bool _isPickedUp = false;
    [SerializeField] private int _value = 10;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        var playerShoot = other.GetComponentInParent<PlayerStateManager>();
        if(playerShoot == null || _isPickedUp) return;
        playerShoot.CurrentAmmo += _value;
        _isPickedUp = true;
        Destroy(this.gameObject);
    }
}
