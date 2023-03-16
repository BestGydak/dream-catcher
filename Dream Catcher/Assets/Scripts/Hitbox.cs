using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [SerializeField] private float _multiplier = 1;
    private Health _healthComponent;

    void Awake()
    {
        _healthComponent = transform.parent.GetComponent<Health>();
    }
    
    public void TakeDamage(float value)
    {
        _healthComponent.CurrentHealth -= value * _multiplier;
    }

    public void RestoreHealth(float value)
    {
        _healthComponent.CurrentHealth += value;
    }
}
