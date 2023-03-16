using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public float Damage;
    bool isDamageDealt = false;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        var hitbox = other.gameObject.GetComponent<Hitbox>();
        if(!isDamageDealt && hitbox != null)
        {
            hitbox.TakeDamage(Damage);
            isDamageDealt = true;
        }
    }
}
