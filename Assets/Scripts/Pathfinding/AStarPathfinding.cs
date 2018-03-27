using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Cheezegami.Pathfinding {
    class AStarPathfinding { // Inspired by https://github.com/davecusatis/A-Star-Sharp/blob/master/Astar.cs to create functional aStar pathfinding.
        private List<List<Node>> grid;


        public AStarPathfinding(List<List<Node>> grid) {
            this.grid = grid;
        }
        private int GridRows {
            get {
                return grid[0].Count;
            }
        }
        private int GridCols {
            get {
                return grid.Count;
            }
        }

        public Stack<Node> FindPath(Vector2Int startPos, Vector2Int endPos) {
            Node start = new Node(new Vector2Int(startPos.x, startPos.y), true);
            Node end = new Node(new Vector2Int(endPos.x, endPos.y), true);

            Stack<Node> path = new Stack<Node>();
            List<Node> openList = new List<Node>();
            List<Node> closedList = new List<Node>();
            List<Node> adjacencies;
            Node current = start;

            // add start node to Open List
            openList.Add(start);

            while (openList.Count != 0 && !closedList.Exists(x => x.position == end.position)) {
                current = openList[0];
                openList.Remove(current);
                closedList.Add(current);
                adjacencies = GetAdjacentNodes(current);


                foreach (Node n in adjacencies) {
                    if (!closedList.Contains(n) && n.walkable) {
                        if (!openList.Contains(n)) {
                            n.parent = current;
                            n.distanceToEnd = Math.Abs(n.position.x - end.position.y) + Math.Abs(n.position.y - end.position.y);
                            n.cost = 1 + n.parent.cost;
                            openList.Add(n);
                            openList = openList.OrderBy(node => node.F).ToList<Node>();
                        }
                    }
                }
            }

            // construct path, if end was not closed return null
            if (!closedList.Exists(x => x.position == end.position)) {
                return null;
            }

            // if all good, return path
            Node temp = closedList[closedList.IndexOf(current)];
            while (temp.parent != start && temp != null) {
                path.Push(temp);
                temp = temp.parent;
            }
            return path;
        }

        private List<Node> GetAdjacentNodes(Node n) {
            List<Node> temp = new List<Node>();

            int row = n.position.y;
            int col = n.position.x;

            if (row + 1 < GridRows) {
                temp.Add(grid[col][row + 1]);
            }
            if (row - 1 >= 0) {
                temp.Add(grid[col][row - 1]);
            }
            if (col - 1 >= 0) {
                temp.Add(grid[col - 1][row]);
            }
            if (col + 1 < GridCols) {
                temp.Add(grid[col + 1][row]);
            }

            return temp;
        }
    }
}
