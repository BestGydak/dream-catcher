using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    [SerializeField] float duration = 1;
    [SerializeField] float intensity = 5;
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.transform.parent != null
        && other.transform.parent.GetComponent<PlayerStateManager>() != null)
        {
            Camera.main.GetComponent<CameraShake>().Shake(duration, intensity);
            GetComponentInParent<BomberStateManager>().HandleExplosion();
        }   
    }
}
