using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

// Any Living Object I.E. Player / AI
public abstract class LivingEntity : MovingEntity, IDamageble {
    public int health = 5;
    public int walkDistance = 1;
    [HideInInspector]
    public float moveCooldownCounter = 0f;
    
    [HideInInspector]
    public float moveCooldown = 0.05f;

    public void TakeDamage(int damage) {
        health -= damage;
        if(health < 0) {
            Die();
        }
    }
    public abstract void Die();
}
