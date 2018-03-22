using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovePlayer : MonoBehaviour {
    public int walkDistance = 1;
    private float internalCD = 0.05f;
    private Vector3 basePosition;
    public float cooldown = 0;
    private Rigidbody2D rb;
	// Use this for initialization
	private void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	private void Update () {
        Move();
        SnapPosition();
    }

    public void Move() {
        if (cooldown < 0) {
            if (Input.GetKey(KeyCode.RightArrow)) {
                basePosition = transform.position;
                rb.MovePosition(transform.position + transform.right * walkDistance);
                cooldown += internalCD;
            }
            if (Input.GetKey(KeyCode.LeftArrow)) {
                basePosition = transform.position;
                rb.MovePosition(transform.position + transform.right * -1 * walkDistance);
                cooldown += internalCD;
            }
            if (Input.GetKey(KeyCode.DownArrow)) {
                basePosition = transform.position;
                rb.MovePosition(transform.position + transform.up * -1 * walkDistance);
                cooldown += internalCD;
            }
            if (Input.GetKey(KeyCode.UpArrow)) {
                basePosition = transform.position;
                rb.MovePosition(transform.position + transform.up * walkDistance);
                cooldown += internalCD;
            }
        }
        else {
            cooldown -= Time.deltaTime;
        }
        SnapPosition();
             

    }
    public void SnapPosition() {
        transform.position = Vector3Int.RoundToInt(transform.position);
    }
    public void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Wall") {
            transform.position = basePosition;
        }
    }
}
