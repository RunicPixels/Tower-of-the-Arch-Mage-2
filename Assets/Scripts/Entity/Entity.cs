using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour {
    public int walkDistance = 1;
    public float speed = 1;
    public float internalCD = 0.05f;
    public Vector3 basePosition;
    public float cooldown = 0;
    public Rigidbody2D rb;

    public abstract void Move();
}
