using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{   
    [SerializeField] private int damage;
    public float attackSpeed;
    [SerializeField] private bool canAttack;

    private void Start() {
        InitVariables();
    }

    public void DealDamage(CharacterStats statsToDamage)
    {   
        statsToDamage.TakeDamage(damage);


    }

    public override void Die()
    {
        base.Die();
        Destroy(gameObject);
    }

    public override void InitVariables()
    {
        maxHealth = 30;
        SetHealthTo(maxHealth);
        isDead = false;

        damage = 5;
        attackSpeed = 1f;
        canAttack = true;
    }
}
