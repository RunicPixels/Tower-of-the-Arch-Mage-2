using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : Entity {
    public float speed = 1;
    private Vector3 targetPosition;
    // Use this for initialization
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        internalCD = 1;
    }

    // Update is called once per frame
    private void Update() {
        Move();
        //SnapPosition();
    }

    public override void Move() {
        float step = speed * Time.deltaTime;
        if (cooldown < 0) {
            if (Input.GetKey(KeyCode.RightArrow)) {
                basePosition = transform.position;
                RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, walkDistance);
                if (hit.transform != null && hit.transform.gameObject.tag == "Wall") {
                    Debug.Log(hit.transform.gameObject.tag);
                }
                    
                else {
                    targetPosition = transform.position + transform.right;
                    cooldown += internalCD;
                }
            }
            if (Input.GetKey(KeyCode.LeftArrow)) {
                basePosition = transform.position;
                RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right * -1, walkDistance);
                if (hit.transform != null && hit.transform.gameObject.tag == "Wall") {
                    Debug.Log(hit.transform.gameObject.tag);
                }

                else {
                    targetPosition = transform.position + transform.right * -1;
                    cooldown += internalCD;
                }
            }
            if (Input.GetKey(KeyCode.DownArrow)) {
                basePosition = transform.position;
                RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up * -1, walkDistance);
                if (hit.transform != null && hit.transform.gameObject.tag == "Wall") {
                    Debug.Log(hit.transform.gameObject.tag);
                }

                else {
                    targetPosition = transform.position + transform.up * -1;
                    cooldown += internalCD;
                }
            }
            if (Input.GetKey(KeyCode.UpArrow)) {
                basePosition = transform.position;
                RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, walkDistance);
                if (hit.transform != null && (hit.transform.gameObject.tag == "Wall" || hit.transform.gameObject.tag == "Enemy")) {
                    Debug.Log(hit.transform.gameObject.tag);
                }
                else {
                    targetPosition = transform.position + transform.up;
                    cooldown += internalCD;
                }
            }
        }
        else {
            cooldown -= speed * Time.deltaTime;
           
        }
        if (transform.position != targetPosition && cooldown > 0) {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
        }
        if(transform.position == targetPosition || cooldown < 0) {
            SnapPosition();
        }
        //SnapPosition();


    }
    public void SnapPosition() {
        transform.position = Vector3Int.RoundToInt(transform.position);
    }
    public void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Wall") {
            transform.position = targetPosition;
        }
    }
}
