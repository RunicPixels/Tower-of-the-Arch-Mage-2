using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cheezegami.Pathfinding;

public class EnemyEntity : LivingEntity {
    public bool canMove;
    public bool knowPlayerPosition;
    public bool diagonalMovement;
    [HideInInspector]
    public GameObject player;

    private char[,] map;
    private PathGenerator generator;
    private NodeList list;
    private List<List<Node>> grid;
    private AStarPathfinding finder;
    private Stack<Node> path;
    private Vector2Int position;
    private Vector2Int playerPosition;
    private Vector2Int targetPosition;

    // Use this for initialization
    public override void Start() {
        base.Start();
        generator = FindObjectOfType<PathGenerator>();
        map = generator.map.Clone() as char[,];
        list = new NodeList(generator);
        grid = list.ConstructGrid();
        finder = new AStarPathfinding(grid);
    }

    // Update is called once per frame
    public override void Update() {
        player = FindObjectOfType<PlayerEntity>().gameObject;
        if (moveCooldownCounter <= 0) {
            FindPath();
        }
        base.Update();
    }

    public void FindPath() {
        position = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
        playerPosition = new Vector2Int(Mathf.RoundToInt(player.transform.position.x), Mathf.RoundToInt(player.transform.position.y));
        path = finder.FindPath(position, playerPosition);
        
        if (path != null) {
            Node nextNode = path.Pop();
            targetPosition = nextNode.position;

        }
        else {
            targetPosition = position;
        }
    }
    public override void Move() {
        float step = speed * Time.deltaTime;
        if (moveCooldownCounter < 0) {
            
            moveCooldownCounter += moveCooldown;
        }
        else {
            moveCooldownCounter -= speed * Time.deltaTime;

        }
        Vector3 targetPositionVector3 = new Vector3(targetPosition.x, targetPosition.y);
        if (transform.position != targetPositionVector3 && moveCooldown >= 0) {
            transform.position = Vector3.MoveTowards(transform.position, targetPositionVector3, step);
        }
        if (transform.position == targetPositionVector3 || moveCooldown < 0) {
            SnapPosition();
        }


    }
    public void SnapPosition() {
        transform.position = Vector3Int.RoundToInt(transform.position);
    }

    public override void Die() {
        Destroy(gameObject);
    }

}
