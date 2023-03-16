using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [HideInInspector] public float Speed = 1;
    private Rigidbody2D _rb2d;
    private Vector2 _direction;
    public Vector2 Direction
    {
        get
        {
            return _direction;
        }
        set
        {
            _direction = value.normalized;
            var angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
        }
    }
    
    void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _rb2d.velocity = _direction * Speed; 
    } 
    
    private void Update() 
    {
        _rb2d.velocity = _direction * Speed; 
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.layer != LayerMask.NameToLayer("PlayerHand"))
        {
            Destroy(gameObject);
        }
    }
}