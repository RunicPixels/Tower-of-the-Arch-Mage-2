using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonRoom
{
    public int roomNumber;
    public int posX;
    public int posY;
    public int width;
    public int height;

    bool hasExit = false;

    public DungeonRoom(int roomNumber,int posX, int posY, int width, int height)
    {
        this.roomNumber = roomNumber;
        this.posX = posX;
        this.posY = posY;
        this.width = width;
        this.height = height;
    }
}
