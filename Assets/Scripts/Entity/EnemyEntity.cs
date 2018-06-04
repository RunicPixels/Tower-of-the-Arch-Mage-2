using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cheezegami.Pathfinding;
using Panda;

public class EnemyEntity : LivingEntity, IProjectileShooter {
    [Task]
    public bool canMove;
    [Task]
    public bool hasPath;
    [Task]
    public bool canAttack;

    public GameObject projectile;
    public bool knowPlayerPosition;
    public bool diagonalMovement;
    public int attackRange;

    [HideInInspector]
    public GameObject player;

    private float shootCD = 0;
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

    [Task]
    public void FindPlayer() {
        player = FindObjectOfType<PlayerEntity>().gameObject;
        playerPosition = new Vector2Int(Mathf.RoundToInt(player.transform.position.x), Mathf.RoundToInt(player.transform.position.y));
    }

    [Task]
    public void FindPath() {
        FindPlayer();
        position = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
        path = finder.FindPath(position, playerPosition);
        hasPath = true;

        if (path != null) {
            if (path.Count <= attackRange) {
                return;
            }
            Node nextNode = path.Pop();
            targetPosition = nextNode.position;
        }
        else {
            targetPosition = position;
        }
    }
    [Task]
    public bool CheckCanAttack() {
        if (path == null) {
            hasPath = false;
            return false;
        }
        else if (path.Count <= attackRange) {
            canAttack = true;
            return true;
        }
        else {
            return false;
        }
    }

    [Task]
    public override void Move() {
        if(CheckCanAttack() == false) {
            canAttack = false;
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
                hasPath = false;
            }
        }
    }

    [Task]
    public void Attack() {
        if (shootCD <= 0) {
            canAttack = false;
            hasPath = false;
            Shoot(player.transform.position-transform.position);
        }
        else {
            shootCD -= Time.deltaTime;
        }
    }

    public void SnapPosition() {
        transform.position = Vector3Int.RoundToInt(transform.position);
    }

    public override void Die() {
        Destroy(gameObject);
    }

    public void Shoot(Vector2 direction) {
        shootCD = 1;
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
}
