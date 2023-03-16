using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    private bool _isPickedUp;
    [SerializeField] private float _value;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        var hitbox = other.GetComponent<Hitbox>();
        if(hitbox == null || _isPickedUp) return;
        hitbox.RestoreHealth(_value);
        _isPickedUp = true;
        Destroy(this.gameObject);
    }
}
