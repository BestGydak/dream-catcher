using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
public class PlayerAliveState : PlayerBaseState
{
    private bool _isReady;
    private PlayerStateManager _player;
    public override void EnterState(PlayerStateManager player)
    {
        _player = player;
    }

    public override void UpdateState(PlayerStateManager player)
    {
        MovePlayer();
    }

    public override void OnFire(InputValue value)
    {
        if(_player.FireTimer <= 0 && value.isPressed && _player.CurrentAmmo > 0)
        {
            InitializeBullet();
            PlaySoundEffect();
            _player.CurrentAmmo -= 1;
            _player.FireTimer = _player.FireCooldown;
        }
    }

    public override void OnMove(InputValue value)
    {
        _player.MoveDirection = value.Get<Vector2>();
    }

    public override void OnAdditionalFire(InputValue value)
    {
        if(_player.PunchTimer > 0) return;
        var hitboxes = _player.Hand.EnemiesHitboxes.ToList();
        foreach(var enemyHitbox in hitboxes)
        {
            KnockEnemy(enemyHitbox);
        }
        var bullets = _player.Hand.EnemiesBullets.ToList();
        foreach (var bullet in bullets)
        {
            DeflectBullet(bullet);
        }
        _player.PunchTimer = _player.PunchCooldown;
    }

    private void MovePlayer()
    {
        var direction = _player.RigidBody.position + _player.MoveDirection * _player.MoveSpeed * Time.fixedDeltaTime;
        _player.RigidBody.MovePosition(direction);
    }
    
    private void KnockEnemy(Hitbox enemyHitbox)
    {
        if(enemyHitbox == null) return;
        enemyHitbox.TakeDamage(_player.PunchDamage);
        var direction = (enemyHitbox.transform.position - _player.transform.position).normalized;
        enemyHitbox.GetComponentInParent<EnemyBaseStateManager>().HandleKnock(direction);
    }

    private void DeflectBullet(BulletMovement bullet)
    {
        if(bullet == null) return;
        bullet.Direction = bullet.transform.position - _player.transform.position;
        bullet.gameObject.layer = LayerMask.NameToLayer("PlayerBullet");
    }

    private void InitializeBullet()
    {
            var direction = Input.mousePosition - Camera.main.WorldToScreenPoint(_player.Gun.transform.position);
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            var weaponVector = direction.normalized * _player.WeaponLength;

            var newBullet = Object.Instantiate(_player.Bullet, _player.Gun.transform.position + weaponVector, Quaternion.identity);
            newBullet.GetComponent<BulletMovement>().Direction = direction;
            newBullet.GetComponent<BulletMovement>().Speed = _player.BulletSpeed;
            newBullet.GetComponent<DamageDealer>().Damage = _player.BulletDamage;
    }

    private void PlaySoundEffect()
    {
        if(_player.ShootSFX == null) return;

        var sfx = Object.Instantiate(_player.ShootSFX);
        Object.Destroy(sfx.gameObject, sfx.Audio.clip.length);
    }
}
