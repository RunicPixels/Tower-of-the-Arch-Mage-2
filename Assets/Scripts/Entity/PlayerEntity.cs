using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cheezegami.Pathfinding;

public class PlayerEntity : LivingEntity {
    private Vector3 targetPosition;
    public GameObject projectile;
    public float shootCooldown = 1;
    private float shootCooldownCounter = 0;

    // Use this for initialization
    public override void Start() {
        base.Start();
        Debug.Log(mainCollider);
        targetPosition = transform.position;
        moveCooldown = 1f;
    }

    // Update is called once per frame
    public override void Update() {
        base.Update();
    }

    public void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Wall") {
            transform.position = targetPosition;
        }
    }

    public override void Move() {
        // Moving
        float step = speed * Time.deltaTime;
        if (moveCooldownCounter < 0) {
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W)) {
                basePosition = transform.position;
                RaycastHit2D hit;
                if (Input.GetKey(KeyCode.D)) {
                    hit = Physics2D.Raycast(transform.position, transform.right, walkDistance);
                    targetPosition = transform.position + transform.right;
                }
                else if (Input.GetKey(KeyCode.A)) {
                    hit = Physics2D.Raycast(transform.position, transform.right * -1, walkDistance);
                    targetPosition = transform.position + transform.right * -1;
                }
                else if (Input.GetKey(KeyCode.S)) {
                    hit = Physics2D.Raycast(transform.position, transform.up * -1, walkDistance);
                    targetPosition = transform.position + transform.up * -1;
                }
                else if (Input.GetKey(KeyCode.W)) {
                    hit = Physics2D.Raycast(transform.position, transform.up, walkDistance);
                    targetPosition = transform.position + transform.up;
                }
                else {
                    hit = Physics2D.Raycast(transform.position, transform.position, 0);
                }
                if (CheckHitCollision(hit) == true) {
                    
                    targetPosition = basePosition;
                }
                else {
                    moveCooldownCounter += moveCooldown;
                }
            }
        }
        else {
            moveCooldownCounter -= speed * Time.deltaTime;
        }
        if (transform.position != targetPosition && moveCooldownCounter >= 0) {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
        }
        else if (transform.position == targetPosition || moveCooldownCounter < 0) {
            SnapPosition();
        }

        // Shooting
        if (shootCooldownCounter < 0 ) {
            if (Input.GetKey(KeyCode.RightArrow)) {
                Shoot(Vector2.right);
                shootCooldownCounter = shootCooldown;
            }
            else if (Input.GetKey(KeyCode.LeftArrow)) {
                Shoot(Vector2.left);
                shootCooldownCounter = shootCooldown;
            }
            else if (Input.GetKey(KeyCode.DownArrow)) {
                Shoot(Vector2.down);
                shootCooldownCounter = shootCooldown;
            }
            else if (Input.GetKey(KeyCode.UpArrow)) {
                Shoot(Vector2.up);
                shootCooldownCounter = shootCooldown;
            }
        }
        else {
            shootCooldownCounter -= Time.deltaTime;
        }
        //SnapPosition();


    }

    public bool CheckHitCollision(RaycastHit2D hit) {
        if (hit.transform != null) {
            if (hit.transform.gameObject.GetComponent<DoorEntity>() != null) {
                DoorEntity door = hit.transform.gameObject.GetComponent<DoorEntity>();
                door.OpenDoor();
                return true;
            }
            if (hit.transform.gameObject.tag == "Wall" || hit.transform.gameObject.tag == "Enemy") {
                return true;
            }
            Debug.Log(hit.transform.gameObject.tag);
        }
        return false;
    }

    public void SnapPosition() {
        transform.position = Vector3Int.RoundToInt(transform.position);
        targetPosition = transform.position;
    }

    private void Shoot(Vector2 direction) {
        GameObject proj = Instantiate(projectile, transform.position, Quaternion.identity, null);
        ProjectileEntity projEntity;
        if (proj.GetComponent<ProjectileEntity>() != null) {
            projEntity = proj.GetComponent<ProjectileEntity>();
        }
        else {
            projEntity = proj.AddComponent<ProjectileEntity>();
        }
        projEntity.caster = this;
        projEntity.Direction = direction;
        
    }

    public override void Die() {
        // GAME OVERRRRRR
    }
}
