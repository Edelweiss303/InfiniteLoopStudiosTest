using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Character
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void DoDamage(float damage)
    {
        CurrentHealth -= damage;
    }

    protected override void Kill()
    {
        rb.useGravity = false;
        capsuleCollider.enabled = false;
        animator.SetTrigger("Die");
    }
}
