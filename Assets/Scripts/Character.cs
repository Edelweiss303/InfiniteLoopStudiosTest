using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{

    [Header("Combat Parameters")]
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    [Space]
    [Header("Components")]
    [SerializeField] protected Animator animator;
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected CapsuleCollider capsuleCollider;

    public float CurrentHealth
    {
        get { return currentHealth; }
        set
        {
            currentHealth = value;
            if (value <= 0)
            {
                Kill();
            }
        }
    }

    private void Awake()
    {
        currentHealth = maxHealth;
    }
    protected abstract void Kill();
    public abstract void DoDamage(float damage);
}
