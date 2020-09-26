using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Unit
{
    public  float health = 1;
  
    protected virtual void Awake() {}
    protected virtual void Start() {}
    protected virtual void Update() {
        if (health <= 0) {
            Die();
        }
    }
    
protected virtual void OnTriggerEnter2D(Collider2D collider)
{
    Arrow arrow = collider.GetComponent<Arrow>();
    if (arrow)
    {
        ReciveDamage();
    }

    Hero hero = collider.GetComponent<Hero>();
    if (hero)
    {

        hero.ReciveDamage();
    }
  }
    public void Damage(float damage) {
        health -= damage;
    }
}
    
