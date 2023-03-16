using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    
    [SerializeField] private float _health;
    [SerializeField] private bool _isCapped;
    [SerializeField] private float _maxHealth = 100;
    [SerializeField] private bool _isGod;
    [SerializeField] public PlaySound HitSFX;

    private bool _isDead;

    public event Action<GameObject> OnDeath;
    public event Action<Health> OnHealthChanged;

    public float CurrentHealth
    {
        get
        {
            return _health;
        }
        set
        {
            if(_isGod) return;
            
            if(value < _health)
            {
                PlayHitEffect();
            }
            if(_isCapped)
                _health = Mathf.Min(value, MaxHealth);
            else 
                _health = value;
            if(_health <= 0)
                TriggerDeath();
            OnHealthChanged?.Invoke(this);
        }
    }

    public float MaxHealth
    {
        get  => _maxHealth;
        
        set
        {
            _maxHealth = value;
            OnHealthChanged?.Invoke(this);
        }
    }

    public void TriggerDeath()
    {
        if(_isDead) return;
        _isDead = true;
        OnDeath?.Invoke(this.gameObject);
        Destroy(gameObject);
    }

    private void PlayHitEffect()
    {
        if(HitSFX == null) return;

        var sfx = Instantiate(HitSFX);
        Destroy(sfx.gameObject, sfx.Audio.clip.length);
    }
}
