using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarksmanStateManager : EnemyBaseStateManager
{
    [HideInInspector] public Rigidbody2D RigidBody;
    [HideInInspector] public PlayerStateManager Player;
    [HideInInspector] public Hitbox Target;
    private bool _isReloading;
    public MarksmanChaseState ChaseState = new MarksmanChaseState();
    public MarksmanIdleState IdleState = new MarksmanIdleState();
    public MarksmanKnockedState KnockedState = new MarksmanKnockedState();
    public MarksmanShootingPauseStage PauseStage = new MarksmanShootingPauseStage();

    [Header("Movement")]
    [SerializeField] private float _minDesiredDistance = 2;
    [SerializeField] private float _maxDesiredDistance = 14;
    [SerializeField] float speed = 10;

    [Header("Shoot Origins")]
    [SerializeField] GameObject gun;
    [SerializeField] float weaponLength;

    [Header("Firing")]
    [SerializeField] public bool IsFiring = true;
    [SerializeField] float fireCooldown = 0.3f;
    [SerializeField] private float _minInitialPauseTime = 0.2f;
    [SerializeField] private float _maxIntitalPauseTime = 0.4f;
    [HideInInspector] public float InitialPauseTime;
    
    [Header("Bullet")]
    [SerializeField] GameObject bullet;
    [SerializeField] float bulletDamage = 5;
    [SerializeField] float bulletSpeed = 10;


    [Header("Knocking")]
    [SerializeField] private float _knockPower = 3;
    [SerializeField] public float KnockTime = 1.5f;
    [SerializeField] public float IdleTime = 2;
    [SerializeField] public float MinShootPauseAfterKnock = 0.5f;
    [SerializeField] public float MaxShootPauseAfterKnock = 1;
    [HideInInspector] public Vector2 KnockDirection{get => _knockDirection;}
    [HideInInspector] public float KnockPower{get => _knockPower;}

    [Header("Pickup")]
    [SerializeField] public GameObject[] Pickups = new GameObject[2];
    private bool _isPickupDropped = false;

    private Vector2 _knockDirection;

    private void Awake()
    {
        Player = FindObjectOfType<PlayerStateManager>();
        Target = Player?.GetComponentInChildren<Hitbox>();
        RigidBody = GetComponent<Rigidbody2D>();
        DesiredDistance = Random.Range(_minDesiredDistance, _maxDesiredDistance);
        InitialPauseTime = Random.Range(_minInitialPauseTime, _maxIntitalPauseTime);
    }

    private void Start() 
    {
        SwitchState(PauseStage, InitialPauseTime);   
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
            var pickupIndex = Mathf.FloorToInt(Random.Range(0, (float)Pickups.Length - 0.00000001f));
            Instantiate(Pickups[pickupIndex], transform.position, Quaternion.identity);
            _isPickupDropped = true;
        }
        _knockDirection = knockDirection;
        SwitchState(KnockedState);
    }

    public void Chase()
    {
        if(Player == null) return;
        
        if(Vector2.Distance(transform.position, Player.transform.position) > DesiredDistance
        && CloserEnemiesNearby.Count == 0)
        {
            RigidBody.MovePosition(Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.fixedDeltaTime));
        }
    }
    
    public void Shoot()
    {
        if(_isReloading || !IsFiring || Player == null) return;
        CreateNewBullet();
        StartCoroutine(WeaponCooldown());
    }

    IEnumerator WeaponCooldown()
    {
        _isReloading = true;
        yield return new WaitForSeconds(fireCooldown);
        _isReloading = false;
    }

    private void CreateNewBullet()
    {
        var anticipatedLocation = Target.transform.position + (Vector3)Player.MoveDirection * Player.MoveSpeed * Time.fixedDeltaTime;
        var direction = anticipatedLocation - gun.transform.position;
        var weaponVector = direction.normalized * weaponLength;

        var newBullet = Instantiate(bullet, gun.transform.position + weaponVector, Quaternion.identity);
        newBullet.GetComponent<BulletMovement>().Direction = direction;
        newBullet.GetComponent<BulletMovement>().Speed = bulletSpeed;
        newBullet.GetComponent<DamageDealer>().Damage = bulletDamage;
    }
}
