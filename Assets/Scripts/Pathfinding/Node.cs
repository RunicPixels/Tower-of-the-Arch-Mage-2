using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {
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


}
