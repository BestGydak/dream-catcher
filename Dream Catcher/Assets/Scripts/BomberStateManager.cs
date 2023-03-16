using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberStateManager : EnemyBaseStateManager
{
    public BomberIdleState IdleState = new BomberIdleState();
    public BomberKnockedState KnockedState = new BomberKnockedState();
    public BomberPursueState PursueState = new BomberPursueState();

    [HideInInspector] public Rigidbody2D RigidBody;
    [HideInInspector] public PlayerStateManager Player;
    [HideInInspector] public Vector2 KnockDirection;
    
    [SerializeField] public float Speed = 15;
    [SerializeField] public PlaySound ExplodeSFX;

    [Header("Knocking")]
    [SerializeField] public float KnockPower = 10;
    [SerializeField] public float KnockTime = 4;
    [SerializeField] public float IdleTime = 2;
    
    [Header("Pickup")]
    [SerializeField] private GameObject Pickup;
    [HideInInspector] private bool _isPickupDropped;

    private void Awake() 
    {
        RigidBody = GetComponent<Rigidbody2D>();
        Player = FindObjectOfType<PlayerStateManager>();
    }

    private void Start() {
        SwitchState(PursueState);
    }

    private void FixedUpdate() 
    {
        CurrentState.UpdateState(this);
    }

    public override void SwitchState(EnemyBaseState state, object param = null)
    {
        CurrentState = state;
        CurrentState.EnterState(this, param);
    }

    public override void HandleKnock(Vector2 knockDirection)
    {
        if(!_isPickupDropped)
        {
            Instantiate(Pickup, transform.position, Quaternion.identity);
            _isPickupDropped = true;
        }
        KnockDirection = knockDirection;
        SwitchState(KnockedState);
    }

    public void HandleExplosion()
    {
        GetComponent<Health>().TriggerDeath();
        Destroy(this.gameObject);
        PlaySoundEffect();
    }

    private void PlaySoundEffect()
    {
        if(ExplodeSFX == null) return;

        var sfx = Instantiate(ExplodeSFX);
        Destroy(sfx.gameObject, sfx.Audio.clip.length);
    }
}
