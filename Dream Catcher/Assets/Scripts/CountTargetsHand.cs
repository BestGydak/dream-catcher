using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CountTargetsHand : MonoBehaviour
{
    public HashSet<Hitbox> EnemiesHitboxes = new HashSet<Hitbox>();
    public HashSet<BulletMovement> EnemiesBullets = new HashSet<BulletMovement>();
    private void OnTriggerEnter2D(Collider2D other) 
    {
        var hitbox = other.GetComponent<Hitbox>();
        if(hitbox != null)
        {
            EnemiesHitboxes.Add(hitbox);
        }
        var bulletMovement = other.GetComponent<BulletMovement>();
        if(bulletMovement != null)
        {
            
            EnemiesBullets.Add(bulletMovement);
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        EnemiesHitboxes.Remove(other.GetComponent<Hitbox>());
        EnemiesBullets.Remove(other.GetComponent<BulletMovement>());
    }
}
