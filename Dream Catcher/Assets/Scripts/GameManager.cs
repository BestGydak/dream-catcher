using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class GameManager : MonoBehaviour
{
    [Header("Map")]
    [SerializeField] public float HorizontalMapPadding;
    [SerializeField] public float VerticalMapPadding;

    [Header("Player")]
    [SerializeField] public PlayerStateManager Player;

    [Header("Marksman Spawner")]
    [SerializeField] public GameObject MarksmanSpawner;
    [SerializeField] public int MarksmanSpawnsCount = 1;
    [SerializeField] public int MarksmanSpawnIncrement = 1;
    [SerializeField] public int RoundsToIncreaseMarksmanSpawners = 2;

    [Header("Bomber Spawner")]
    [SerializeField] public GameObject BomberSpawner;
    [SerializeField] public int BomberSpawnsCount = 0;
    [SerializeField] public int BomberSpawnIncrement = 1;
    [SerializeField] public int RoundsToIncreaseBomberSpawners = 3;

    [Header("Shop")]
    [SerializeField] public UIManager UIManager;

    [Header("Ammo")]
    [SerializeField] public int AmmoBonus = 10;
    [SerializeField] public int AmmoPrice = 3;

    [Header("Ammo Reserve")]
    [SerializeField] public int MaxAmmoBonus = 10;
    [SerializeField] private int _maxAmmoPrice = 3;
    [SerializeField] public int AmmoPriceIncrement = 3;

    [Header("Health")]
    [SerializeField] public int HealthBonus = 20;
    [SerializeField] public int HealthPrice = 3;

    [Header("Health Reserve")]
    [SerializeField] public int MaxHealthBonus = 20;
    [SerializeField] private int _maxHealthPrice = 3;
    [SerializeField] public int HealthPriceIncrement = 3;

    public int MaxHealthPrice
    {
        get
        {
            return _maxHealthPrice;
        }
        set
        {
            _maxHealthPrice = value;
            OnMaxHealthPriceChanged?.Invoke(this);
        }
    }

    public int MaxAmmoPrice
    {
        get
        {
            return _maxAmmoPrice;
        }
        set
        {
            _maxAmmoPrice = value;
            OnMaxAmmoPriceChanged?.Invoke(this);
        }
    }

    public GameBattleState BattleState = new GameBattleState();
    public GamePreparationState PreparationState = new GamePreparationState();
    public GameBaseState CurrentState;
    public event Action<GameManager> OnStateChanged;

    [HideInInspector] public Vector2 MinBounds;
    [HideInInspector] public Vector2 MaxBounds;
    [HideInInspector] public int AllSpawnCount;
    [HideInInspector] public int CurrentSpawnCount;
    [HideInInspector] public HashSet<SpawnerStateManager> Spawns = new HashSet<SpawnerStateManager>();

    private int _currentRound = -1;
    [HideInInspector] public int CurrentRound
    {
        get
        {
            return _currentRound;
        }
        set
        {
            _currentRound = value;
            OnRoundChanged?.Invoke(this);
        }
    }
    public event Action<GameManager> OnRoundChanged;
    public event Action<GameManager> OnMaxAmmoPriceChanged;
    public event Action<GameManager> OnMaxHealthPriceChanged;

    private void Awake() 
    {
        InitBounds();
    }

    private void Start() 
    {
        SwitchState(PreparationState);
    }

    private void FixedUpdate() 
    {
        CurrentState.UpdateState(this);
    }

    public void SwitchState(GameBaseState state)
    {
        CurrentState = state;
        CurrentState.EnterState(this);
        OnStateChanged?.Invoke(this);
    }

    private void InitBounds()
    {
        MinBounds = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        MinBounds = new Vector2(MinBounds.x + HorizontalMapPadding, MinBounds.y + VerticalMapPadding);
        MaxBounds = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        MaxBounds = new Vector2(MaxBounds.x - HorizontalMapPadding, MaxBounds.y - VerticalMapPadding);
    }

    private void OnStartBattle(InputValue value)
    {   
        if(CurrentState != BattleState)
        {
            SwitchState(BattleState);
        }
    }

    public void BuyMaxAmmo()
    {
        if(Player.Coins < MaxAmmoPrice) return;
        Player.Coins -= MaxAmmoPrice;
        Player.MaxAmmo += MaxAmmoBonus;
        MaxAmmoPrice += AmmoPriceIncrement;
        Debug.Log("Ammo bought");
    }

    public void BuyMaxHealth()
    {
        if(Player.Coins < MaxHealthPrice) return;
        Player.Coins -= MaxHealthPrice;
        Player.GetComponent<Health>().MaxHealth += MaxHealthBonus;
        MaxHealthPrice += HealthPriceIncrement;
    }

    public void BuyAmmo()
    {
        if(Player.Coins < AmmoPrice) return;
        Player.Coins -= AmmoPrice;
        Player.CurrentAmmo += AmmoBonus;
    }

    public void BuyHealth()
    {
        if(Player.Coins < HealthPrice) return;
        Player.Coins -= HealthPrice;
        Player.GetComponent<Health>().CurrentHealth += HealthBonus;
    }
}
