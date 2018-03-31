using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

static class DungeonUtilities {
    public static List<Vector2Int> FindRoomOpenings(char[,] map, DungeonRoom room) {
        List<Vector2Int> openings = new List<Vector2Int>();

        // Left
        for(int i = room.posY; i < room.posY+room.height; i++) {
            if(map[room.posX-1,i] == 'F') {
                openings.Add(new Vector2Int(room.posX - 1, i));
            }
        }
        // Right
        for (int i = room.posY; i < room.posY + room.height; i++) {
            if (map[room.posX + room.width + 1, i] == 'F') {
                openings.Add(new Vector2Int(room.posX + room.width + 1 - 1, i));
            }
        }
        // Bottom
        for (int i = room.posX; i < room.posX + room.width; i++) {
            if (map[i, room.posY - 1] == 'F') {
                openings.Add(new Vector2Int(i, room.posY - 1));
            }
        }
        // Top
        for (int i = room.posX; i < room.posX + room.width; i++) {
            if (map[i, room.posY + room.height] == 'F') {
                openings.Add(new Vector2Int(i, room.posY + room.height));
            }
        }
        return openings;
    }
}

