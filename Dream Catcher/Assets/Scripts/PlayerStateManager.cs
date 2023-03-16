using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerStateManager : MonoBehaviour
{
    [SerializeField] public GameManager GameManager;
    [SerializeField] public float MoveSpeed = 15;
    [SerializeField] private int _coins;
    [HideInInspector] public Vector2 MoveDirection;
    [HideInInspector] public Rigidbody2D RigidBody;

    [Header("Firing")]
    [SerializeField] public GameObject Bullet;
    [SerializeField] public PlaySound ShootSFX;
    [SerializeField] public float BulletDamage;
    [SerializeField] public float BulletSpeed;
     [SerializeField] public float FireCooldown = 0.1f;
    [SerializeField] public GameObject Gun;
    [SerializeField] public float WeaponLength = 1;
    [SerializeField] private int _maxAmmo = 15;
    [SerializeField] private bool isCapped = true;
    [SerializeField] private int _currentAmmo = 15;
    [HideInInspector] public float FireTimer;

    [Header("Punch")]
    [SerializeField] public float PunchDamage = 10;
    [SerializeField] public float PunchCooldown = 1;
    [HideInInspector] public float PunchTimer;
    [HideInInspector] public CountTargetsHand Hand;

    public int CurrentAmmo
    {
        get
        {
            return _currentAmmo;
        }
        set
        {
            if(isCapped)
                _currentAmmo = (int)Mathf.Min(value, MaxAmmo);
            else
                _currentAmmo = value;
            OnAmmoCountChanged?.Invoke(this);
        }
    }

    public int MaxAmmo
    {
        get
        {
            return _maxAmmo;
        }
        set
        {
            _maxAmmo = value;
            OnAmmoCountChanged?.Invoke(this);
        }
    }

    public int Coins
    {
        get => _coins;
        set
        {
            _coins = value;
            OnCoinsCountChanged?.Invoke(this);
        }
    }

    public event Action<PlayerStateManager> OnAmmoCountChanged;
    public event Action<PlayerStateManager> OnStateChanged;
    public event Action<PlayerStateManager> OnCoinsCountChanged;
    
    public PlayerAliveState AliveState = new PlayerAliveState();
    public PlayerInShopState InShopState = new PlayerInShopState();
    public PlayerBaseState CurrentState;

    


    private void Awake() 
    {
        RigidBody = GetComponent<Rigidbody2D>();
        Hand = GetComponentInChildren<CountTargetsHand>();
    }

    private void Start() 
    {
        SwitchState(AliveState);
    }

    private void FixedUpdate() 
    {
        CurrentState.UpdateState(this);
    }

    private void Update() 
    {
        UpdateTimers();
    }

    public void SwitchState(PlayerBaseState state)
    {
        CurrentState = state;
        state.EnterState(this);
        OnStateChanged?.Invoke(this);
    }

    private void OnAdditionalFire(InputValue value)
    {
        CurrentState.OnAdditionalFire(value);
    }

    private void OnMove(InputValue value)
    {
        CurrentState.OnMove(value);
    }

    private void OnFire(InputValue value)
    {
        CurrentState.OnFire(value);
    }

    private void OnOpenShop(InputValue value)
    {
        if(GameManager.CurrentState == GameManager.PreparationState && CurrentState == AliveState)
        {
            SwitchState(InShopState);
        }
        else
        {
            SwitchState(AliveState);
        }
    }

    private void UpdateTimers()
    {
        PunchTimer = Mathf.Max(0, PunchTimer - Time.deltaTime);
        FireTimer = Mathf.Max(0, FireTimer - Time.deltaTime);
    }
}
