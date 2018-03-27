using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Cheezegami.Pathfinding {
    public class NodeList {
        private char[,] map;
        private int width;
        private int height;

        public NodeList(PathGenerator generator) {
            this.map = generator.map;
            width = generator.mapLength;
            height = generator.mapHeight;

        }

        public List<List<Node>> ConstructGrid() {

            List<List<Node>> temp = new List<List<Node>>();
            bool walkable = false;
            float tempX = 0;
            float tempY = 0;

            for (int x = 0; x < width; x++) {
                temp.Add(new List<Node>());
                for (int y = 0; y < height; y++) {
                    if (map[x,y] == PathGenerator.WallTileID || map[x,y] == PathGenerator.BorderTileID) {
                        walkable = false;
                    }
                    else {
                        walkable = true;
                    }
                    temp[x].Add(new Node(new Vector2Int(x, y), walkable));
                    tempX += 32;
                }
                tempX = 0;
                tempY += 32;
            }
            return temp;
        }
    }
}
