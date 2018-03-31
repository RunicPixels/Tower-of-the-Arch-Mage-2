using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

// Any Moving Object I.E Projectiles
public abstract class MovingEntity : Entity {
    [HideInInspector]
    public Collider2D mainCollider;
    public float speed = 1;
    [HideInInspector]
    public Rigidbody2D rb;
    [HideInInspector]
    public Vector3 basePosition;

    public virtual void Start() {
        if (GetComponent<Rigidbody2D>() != null) {
            rb = GetComponent<Rigidbody2D>();
        }
        if (GetComponent<Collider2D>() != null) {
            mainCollider = GetComponent<Collider2D>();
        }
        basePosition = transform.position;
    }

    public virtual void Update() {
        Move();
    }

    public abstract void Move();
}
