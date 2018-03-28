using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathVisualizer : MonoBehaviour
{
    public List<GameObject> dungeonShapes;
    //public int minMapPerc = 75; // Minimum Map Percentage.

    public int spriteSizeX = 1;
    public int spriteSizeY = 1;

    public int xPosition = 0;
    public int yPosition = 0;

    public bool allowGeneration = true;

    private PathGenerator pathGenerator;
    private Dictionary<char, GameObject> dungeonGameObjects = new Dictionary<char, GameObject>();

    // Use this for initialization
    private void Start()
    {
        pathGenerator = GetComponent<PathGenerator>();

        LoadDungeonCharacterMappings();
        InstantiateDungeonPieces();
    }

    private void LoadDungeonCharacterMappings()
    {
        foreach (GameObject dungeonShape in dungeonShapes)
        {
            char gameObjectCharacter = dungeonShape.GetComponent<DungeonCharacterMapping>().characterMapping;
            dungeonGameObjects.Add(gameObjectCharacter, dungeonShape);
        }
    }

    // Debug.Log("Reachable room check sucessful! The map has " + visitedCellsCount + " rooms. Which is more than the minimal required " + enoughMap + " rooms! ;D"); // Gegevens over succesvolle map grote doorsturen naar console.
    private void InstantiateDungeonPieces()
    {
        for (int r = 1; r < pathGenerator.mapLength - 1; r++)
        {
            for (int c = 1; c < pathGenerator.mapHeight - 1; c++)
            {
                char ch = pathGenerator.map[r, c];
                //Debug.Log(ch + " at " + r + "," + c);


                if (!dungeonGameObjects.ContainsKey(ch) /*|| !visitedCells[r, c]*/)
                {
                    //Debug.Log("Character at " + r + "," + c + " = " + pathGenerator.map[r, c] + " ( not found )");

                    continue;

                }
                else
                {
                    GameObject dungeonGameObject = dungeonGameObjects[ch];
                    //allowGeneration = true;
                    GameObject tile = Instantiate(dungeonGameObject, new Vector3(r * spriteSizeX + xPosition, c * spriteSizeY + yPosition, 0), dungeonGameObject.transform.rotation);


                    var randomizer = tile.GetComponent<SpriteRandomizer>();
                    if (tile.GetComponentInChildren<SpriteRandomizer>() != null)
                    {
                        tile.GetComponentInChildren<SpriteRandomizer>().Randomize();
                    }

                    //allowGeneration = false;
                    //Debug.Log("Character at " + r + "," + c + " = " + pathGenerator.map[r, c] + " (" + dungeonGameObject + ")");
                }
            }
        }
    }
}
