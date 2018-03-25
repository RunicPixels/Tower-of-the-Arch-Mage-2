using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : Entity {
    public PathGenerator generator;
    public GameObject player;
    public bool canMove;
    public bool knowPlayerPosition;

    public int searchDistance = 256; // Distance to which the enemy can detect a player.

    // Use this for initialization
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        generator = FindObjectOfType<PathGenerator>();
        
    }

    // Update is called once per frame
    private void Update() {
        player = FindObjectOfType<PlayerEntity>().gameObject;
        Move();
    }

    public override void Move() {
        if (cooldown < 0) {
            Vector2Int position = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
            Vector2Int playerPosition = new Vector2Int(Mathf.RoundToInt(player.transform.position.x), Mathf.RoundToInt(player.transform.position.y));

            if (CheckIfPathExists(generator.map.Clone() as char[,], position, playerPosition) == 1) {
                basePosition = transform.position;
                rb.MovePosition(transform.position + transform.right * walkDistance);
                cooldown += internalCD;
                Debug.Log("Enemy Moving Right");
            }
            if (CheckIfPathExists(generator.map.Clone() as char[,], position, playerPosition) == 2) {
                basePosition = transform.position;
                rb.MovePosition(transform.position + transform.right * -1 * walkDistance);
                cooldown += internalCD;
            }
            if (CheckIfPathExists(generator.map.Clone() as char[,], position, playerPosition) == 3) {
                basePosition = transform.position;
                rb.MovePosition(transform.position + transform.up * -1 * walkDistance);
                cooldown += internalCD;
            }
            if (CheckIfPathExists(generator.map.Clone() as char[,], position, playerPosition) == 4) {
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


    /// <summary>Recursive Method that provides which path to go to : 
    /// Returns: 0 = Blocked Path, 1 = Right, 2 = Up, 3 = Left, 4 = Down</summary>
    /// <returns>0 = Blocked Path, 1 = Right, 2 = Up, 3 = Left, 4 = Down</returns>
    private int CheckIfPathExists(char[,] map, Vector2Int startPos, Vector2Int endPos, int iteration = 0, int direction = 0) {
        // Map is a 2d area/matrix to search through.
        // startpos = 2d position of beginning
        // endPos = 2d position of end.
        iteration += 1;

        if (startPos == endPos) {
            //Debug.Log("EndPos Found");
            return direction;
        }

        if (iteration > searchDistance || map[startPos.x, endPos.y] == PathGenerator.BorderTileID) {
            return 0;
        }

        map[startPos.x, startPos.y] = 'O';

        Vector2Int dirRight = new Vector2Int(startPos.x + 1, startPos.y);
        Vector2Int dirUp = new Vector2Int(startPos.x, startPos.y + 1);
        Vector2Int dirLeft = new Vector2Int(startPos.x - 1, startPos.y);
        Vector2Int dirDown = new Vector2Int(startPos.x, startPos.y - 1);

        if (map[dirRight.x, dirRight.y] == PathGenerator.FloorTileID) {
            direction = 1;
            if (CheckIfPathExists(map, dirRight, endPos, iteration, direction) != 0) return direction;
        }
        if (map[dirUp.x, dirUp.y] == PathGenerator.FloorTileID) {
            direction = 2;
            if (CheckIfPathExists(map, dirUp, endPos, iteration, direction) != 0) return direction;
        }
        if (map[dirLeft.x, dirLeft.y] == PathGenerator.FloorTileID) {
            direction = 3;
            if (CheckIfPathExists(map, dirLeft, endPos, iteration, direction) != 0) return direction;
        }
        if (map[dirDown.x, dirDown.y] == PathGenerator.FloorTileID) {
            direction = 4;
            if (CheckIfPathExists(map, dirDown, endPos, iteration, direction) != 0) return direction;
        }

        //Debug.Log("returning false on iteration " + iteration);
        return 0;

    }
}
