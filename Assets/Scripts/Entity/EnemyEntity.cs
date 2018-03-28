using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cheezegami.Pathfinding;

public class EnemyEntity : Entity {
    public bool canMove;
    public bool knowPlayerPosition;
    public bool diagonalMovement;
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
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        generator = FindObjectOfType<PathGenerator>();
        map = generator.map.Clone() as char[,];
        list = new NodeList(generator);
        grid = list.ConstructGrid();
        finder = new AStarPathfinding(grid);
    }

    // Update is called once per frame
    private void Update() {
        player = FindObjectOfType<PlayerEntity>().gameObject;
        if (cooldown < 0) {
            FindPath();
        }
        Move();
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
        if (cooldown < 0) {
            
            cooldown += internalCD;
        }
        else {
            cooldown -= speed * Time.deltaTime;

        }
        Vector3 targetPositionVector3 = new Vector3(targetPosition.x, targetPosition.y);
        if (transform.position != targetPositionVector3 && cooldown > 0) {
            transform.position = Vector3.MoveTowards(transform.position, targetPositionVector3, step);
        }
        if (transform.position == targetPositionVector3 || cooldown < 0) {
            SnapPosition();
        }


    }
    public void SnapPosition() {
        transform.position = Vector3Int.RoundToInt(transform.position);
    }
}
