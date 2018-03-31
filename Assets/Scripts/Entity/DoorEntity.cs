using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class DoorEntity : Entity {
    public Sprite closedSprite;
    public Sprite openSprite;
    public int enemyResistance = 5; // Amount of hits it takes for an enemy to break through a door
    public float invulnerableTime = 0.2f; // Durability time of a door in seconds.
    private float invulTimer = 0;

    private SpriteRenderer spriteRenderer;
    private void Start() {
        if (gameObject.GetComponent<SpriteRenderer>() != null) {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        else {
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        }
        spriteRenderer.sprite = closedSprite;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (invulTimer <= 0) {
            if (collision.gameObject.tag == "Player") {
                OpenDoor();
            }
            if (collision.gameObject.tag == "Enemy") {
                if (enemyResistance > 0) {
                    enemyResistance -= 1;
                    invulTimer = invulnerableTime;

                }
                else {
                    OpenDoor();
                }
            }
        }
    }
    public void Update() {
        if (invulTimer > 0) {
            invulTimer -= Time.deltaTime;
        }
    }

    public void OpenDoor () {
        spriteRenderer.sprite = openSprite;
        GetComponent<Collider2D>().enabled = false;
    }
}
