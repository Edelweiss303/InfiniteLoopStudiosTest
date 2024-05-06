using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongGun : Weapon
{
    [SerializeField] private Transform muzzle;
    [SerializeField] private Projectile projectile;

    [SerializeField] private float damage;
    [SerializeField] private float range;
    [SerializeField] private float velocity;
    [SerializeField] private float fireRate;

    private float fireTimer;
    private bool firing;

    private void Awake()
    {
        fireTimer = fireRate;
        firing = false;
    }

    private void Update()
    {
        if (!firing)
        {
            return;
        }

        if(fireTimer <= 0)
        {
            fireTimer = fireRate;
        }

        if (fireTimer == fireRate)
        {
            Projectile currentProjectile = Instantiate(projectile, muzzle.position, muzzle.rotation);
            currentProjectile.Init(velocity, range, damage);
            AudioManager.Instance.PlaySound("LongGunShot");
        }

        fireTimer -= Time.deltaTime;
    }

    public override void Attack()
    {
        fireTimer = 0.0f;
        firing = true;
    }

    public override void CancelAttack()
    {
        firing = false;
    }
}
