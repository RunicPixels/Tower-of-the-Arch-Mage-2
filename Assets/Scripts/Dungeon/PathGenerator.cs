using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PathGenerator : MonoBehaviour
{
    const char floorTileID = 'F';
    const char wallTileID = 'O';
    const char borderTileID = 'X';
    public int mapRows = 8;
    public int mapColumns = 12;

    public int minRoomWidth = 3;
    public int maxRoomWidth = 9;

    public int minRoomHeight = 3;
    public int maxRoomHeight = 9;

    public int minRoomAmount = 5;
    public int maxRoomAmount = 10;
    public int roomTries = 100;

    public int roomMargin = 1;

    public char[,] map;

    private List<DungeonRoom> roomList = new List<DungeonRoom>();

    // Use this for initialization
    private void Awake()
    {
        InitializeMap();
        DisplayMap();
    }
    // Update is called once per frame
    private void Update()
    {

    }
    // De map naar de console loggen.
    public void DisplayMap()
    {
        string output = "";
        for (int r = 0; r < mapRows; r++)
        {
            for (int c = 0; c < mapColumns; c++)
            {
                output += map[r, c];
            }
            output += "\n";
        }
        Debug.Log(output);
    }

    // De basis van de map initialiseren, zoals de X's plaatsen rondom de map en de andere plekken van de map vrijmaken d.m.v. een O.
    public void InitializeMap()
    {
        map = new char[mapRows, mapColumns];

        int mapSeed = System.DateTime.Now.Millisecond;

        // Put borderTileIDs in top and bottom rows.
        for (int c = 0; c < mapColumns; c++)
        {
            map[0, c] = borderTileID;
            map[mapRows - 1, c] = borderTileID;
        }

        // Put borderTileIDs in the left and right columns.
        for (int r = 0; r < mapRows; r++)
        {
            map[r, 0] = borderTileID;
            map[r, mapColumns - 1] = borderTileID;
        }

        // Set wallTileID for the other map spaces (which means 'free').
        for (int r = 1; r < mapRows - 1; r++)
        {
            for (int c = 1; c < mapColumns - 1; c++)
            {
                map[r, c] = 'O';
            }
        }
        // Define amount of Rooms.
        int rooms = UnityEngine.Random.Range(minRoomAmount, maxRoomAmount);

        GenerateRooms(rooms, roomTries); // Spawn in Rooms
        GenerateCorridors(); // Connect the Rooms with Corridors.


        UnityEngine.Random.InitState(mapSeed);
        Debug.Log("Current seed = " + mapSeed);
    }

    private void GenerateRooms(int rooms, int roomTries)
    {
        int roomNumber = 0;
        while (rooms > 0 && roomTries > 0)
        {
            // Calculate Room Width and Height based on minimum and maximum parameters.
            int roomWidth = UnityEngine.Random.Range(minRoomWidth, maxRoomWidth);
            int roomHeight = UnityEngine.Random.Range(minRoomHeight, maxRoomHeight);
            // Row in which the rooms may spawn, roomWidth and Height to constrain within area.
            int startRow = UnityEngine.Random.Range(2, mapRows - roomWidth - 2);
            int startColumn = UnityEngine.Random.Range(2, mapColumns - roomHeight - 2);
            // Generate a room using the startRow (left beginning), startColumn (top beginning), roomWidth ( width of the room) and roomHeight( height of the room)
            if (CreateRoom(startRow, startColumn, roomWidth, roomHeight))
            {
                // Save the room into a new variable
                roomList.Add(new DungeonRoom(roomNumber, startRow, startColumn, roomWidth, roomHeight));
                rooms--;
                roomNumber++;
            }
            roomTries--;
            //else Debug.Log("Room not generated cause of collision.");
        }
    }
    private bool CreateRoom(int startRow, int startColumn, int roomWidth, int roomHeight)
    {
        // Check whether rooms are already places on the current plot.
        for (int x = 0; x < roomWidth+2*roomMargin; x++)
        {
            // For the Height of the Room.
            for (int y = 0; y < roomHeight+2*roomMargin; y++)
            {
                // Draw the assigned "room" symbol on the to be generated map.
                if (map[startRow - roomMargin + x, startColumn - roomMargin + y] == floorTileID)
                {
                    //Debug.Log("Room Already Build at spot! " + (startRow + x) + "," + (startColumn + y));
                    return false;
                }
            }
        }

        // For the Width of the Room
        for (int x = 0; x < roomWidth; x++)
        {
            // For the Height of the Room.
            for (int y = 0; y < roomHeight; y++)
            {
                // Draw the assigned "room" symbol on the to be generated map.
                map[startRow + x, startColumn + y] = floorTileID;
                //Debug.Log("Tile generated: " + map[startRow + x, startRow + y] + " of which x index = " + x + " and y index = "+ y);
            }
        }

        //Debug.Log("Room Created!");

        return true;
    }

    private void GenerateCorridors()
    {

        for (int i = 0; i < roomList.Count; i++)
        {
            //Debug.Log("Roomlist Roomnumber: " + roomList[i].roomNumber + " posX and PosY : " + roomList[i].posX + "x and " + roomList[i].posY + "y, room size is " + roomList[i].width + " wide, and " + roomList[i].height + " high.");

            // Define Corridor Beginning

            //int retries = 5;
            //int randomRoom = i;
            //while (retries > 0 && randomRoom == i)
            //{
            //    randomRoom = UnityEngine.Random.Range(0, roomList.Count);
            //    retries--;
            //}

            int iPlus1 = i+1;
            if(iPlus1 >= roomList.Count)
            {
                iPlus1 = i;
            }

            // Define Corridor Beginning
            Vector2Int beginPos = new Vector2Int(
                                    UnityEngine.Random.Range(roomList[i].posX, roomList[i].posX + roomList[i].width),
                                    UnityEngine.Random.Range(roomList[i].posY, roomList[i].posY + roomList[i].height));

            // Define Corridor End
            Vector2Int endPos  =  new Vector2Int(
                                    UnityEngine.Random.Range(roomList[iPlus1].posX, roomList[iPlus1].posX + roomList[iPlus1].width),
                                    UnityEngine.Random.Range(roomList[iPlus1].posY, roomList[iPlus1].posY + roomList[iPlus1].height));
            if (CheckIfPathExists(map.Clone() as char[,], beginPos, endPos) == false) {
                CreateCorridor(beginPos.x, beginPos.y, endPos.x, endPos.y); // Draw the Corridor.
            }
            else {
                Debug.Log("Corridor already connected");
            }

            // Get a random position at the edge of each room.
        }


        
        // Check if every rooms has an exit.
    }

    private bool CreateCorridor(int beginX, int beginY, int endX, int endY)
    {
        Debug.Log("Drawing Corridor");

        // Define a point to which each room builds a line to.
        // Declaring the Position of the point in the map.
        int posX;
        int posY;
        // Randomizing the direction of the corridor
        if (map[beginX, endY] != map[endX, beginY]) // XOR to prevent overlapping. It checks whether none of the ends have a floor on them, or both. If both or none have a floor it will randomize the position.
        {
            if (UnityEngine.Random.Range(0f, 1f) > 0.5f)
            {
                // X Position of First room, Y Position of the Second Room.
                posX = beginX;
                posY = endY;
            }
            else
            {
                // X Position of Second room, X position of the first room.
                posX = endX;
                posY = beginY;
            }
        }
        else // If either one direction has a floor it will try to avoid placing it there and instead place it in the other direction to prevent overlapping.
        {
            if (map[beginX, endY] == floorTileID)
            {
                posX = endX;
                posY = beginY;
            }
            else
            {
                posX = beginX;
                posY = endY;
            }
        }

        int minX = Math.Min(beginX, endX); // Define Beginning X Point of Path.
        int maxX = Math.Max(beginX, endX); // Define Ending X Point of Path.

        int minY = Math.Min(beginY, endY); // Define Beginning Y Point of Path.
        int maxY = Math.Max(beginY, endY); // Define Ending Y Point of Path.

        Debug.Log("minX : " + minX + " maxX : " + maxX + " minY : " + minY + " maxY : " + maxY);

        // Draw Horizontal
        for (int x = minX; x <= maxX; x++)
        {
            map[x, posY] = floorTileID; // Place Corridor on Map.
        }
        // Draw Vertical
        for (int y = minY; y <= maxY; y++)
        {
            map[posX, y] = floorTileID; // Place Corridor on Map.
        }
        return true;
    }
    private bool CheckIfPathExists(char[,] map, Vector2Int startPos, Vector2Int endPos, int iteration = 0) {
        // Map is a 2d area/matrix to search through.
        // startpos = 2d position of beginning
        // endPos = 2d position of end.
        iteration += 1;

        if (startPos == endPos) {
            Debug.Log("EndPos Found");
            return true;
        }

        if (iteration > 1024) {
            return false;
        }

        map[startPos.x, startPos.y] = 'X';

        Vector2Int dirRight = new Vector2Int(startPos.x + 1,startPos.y);
        Vector2Int dirUp = new Vector2Int(startPos.x, startPos.y + 1);
        Vector2Int dirLeft = new Vector2Int(startPos.x - 1, startPos.y);
        Vector2Int dirDown = new Vector2Int(startPos.x, startPos.y - 1);

        if (map[dirRight.x,dirRight.y] == floorTileID) if(CheckIfPathExists(map, dirRight, endPos, iteration)) return true;
        if (map[dirUp.x,dirUp.y] == floorTileID) if(CheckIfPathExists(map, dirUp, endPos, iteration)) return true;
        if (map[dirLeft.x,dirLeft.y] == floorTileID) if(CheckIfPathExists(map, dirLeft, endPos, iteration)) return true;
        if (map[dirDown.x,dirDown.y] == floorTileID) if(CheckIfPathExists(map, dirDown, endPos, iteration)) return true;

        Debug.Log("returning false on iteration " + iteration);
        return false;
    }
}