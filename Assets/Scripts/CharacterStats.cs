using System.Collections;
using System.Collections.Generic;
using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{   
   
    [SerializeField] protected int health;
    [SerializeField] protected int maxHealth;
    [SerializeField] protected bool isDead;

    private void Start() {
        InitVariables();
    }

    private void Update() {
        CheckHealth();
    
        //test input
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(10);
        }

    }
    public virtual void CheckHealth(){
        if (health <= 0)
        {   
            health = 0;
            Die();
        }
        
        if(health >= maxHealth)
        {
            health = maxHealth;
        }
    }

    public virtual void Die(){
        isDead = true;
    }

    public void SetHealthTo(int healthToSetTo){
        health = healthToSetTo;
        CheckHealth();
    }

    public void TakeDamage(int damage){
        
        int healthAfterDamage = health - damage;
        SetHealthTo(healthAfterDamage);
    }

    public void Heal(int heal){
        int healthAfterHeal = health + heal;
        SetHealthTo(healthAfterHeal);
    }

    public virtual void InitVariables(){
        maxHealth = 100;
        health = maxHealth;
        isDead = false;
    }

}
