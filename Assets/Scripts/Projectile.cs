using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float bulletVelocity;
    private float bulletRange;
    private float bulletDamage;

    private float distanceTravelled;

    public void Init(float velocity, float range, float dmg)
    {
        bulletVelocity = velocity;
        bulletRange = range;
        bulletDamage = dmg;
    }

    private void Update()
    {
        float currentDIstance = bulletVelocity * Time.deltaTime;

        transform.Translate(Vector3.right * currentDIstance);

        distanceTravelled += currentDIstance;

        if(Mathf.Abs(distanceTravelled) > bulletRange)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        EnemyController enemy = other.gameObject.GetComponent<EnemyController>();
        if (enemy != null)
        {
            enemy.DoDamage(bulletDamage);
            Destroy(gameObject);
        }
    }
}
