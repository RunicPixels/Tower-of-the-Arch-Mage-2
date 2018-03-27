using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cheezegami.Pathfinding {
    public class Node {
        public Node parent;
        public bool walkable;
        public Vector2Int position;
        public int distanceToEnd;
        public int cost;
        public float F {
            get {
                if (distanceToEnd != -1 && cost != -1)
                    return distanceToEnd + cost;
                else
                    return -1;
            }
        }
        public Node(Vector2Int pos, bool walkable) {
            this.parent = null;
            this.position = pos;
            this.distanceToEnd = 1000;
            this.cost = 1;
            this.walkable = walkable;
        }
    }
}
