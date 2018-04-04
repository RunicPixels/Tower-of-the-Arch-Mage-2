using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class ProjectileEntity : MovingEntity {

    [HideInInspector]
    public LivingEntity caster;
    public int damage = 2;
    private Vector2 direction;
    private ConstantForce2D force;
    public float lifeTime = 1;
    private bool canDamage = true;

    public Vector2 Direction {
        get {
            return direction;
        }

        set {
            direction = value.normalized;
        }
    }
    public override void Start() {
        base.Start();
        Debug.Log(direction);
        Physics2D.IgnoreLayerCollision(10, 10);
        if(caster != null) {
            Physics2D.IgnoreCollision(mainCollider, caster.mainCollider);
        }
        if(GetComponent<ConstantForce2D>() == null) {
            force = gameObject.AddComponent<ConstantForce2D>();
        }
        else {
            force = GetComponent<ConstantForce2D>();
        }
    }
    public override void Update() {
        lifeTime -= Time.deltaTime;
        if(lifeTime < 0) {
            KillProjectile();
        }
        base.Update();
    }

    public override void Move() {
        rb.velocity = direction.normalized * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.transform != null && caster != null) {
            if (collision.transform.gameObject.GetComponent<IDamageble>() != null) {
                if (collision.transform.tag != caster.tag) {
                    IDamageble damagable = collision.transform.gameObject.GetComponent<IDamageble>();
                    damagable.TakeDamage(damage);
                    KillProjectile();
                }
                else {
                    Physics2D.IgnoreCollision(collision.collider, mainCollider);
                }
            }
        }
        else {
            KillProjectile();
        }
    }
    public void KillProjectile() {
        canDamage = false;
        Destroy(gameObject);
    }
}
