using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathVisualizer : MonoBehaviour
{
    public List<GameObject> dungeonShapes;
    //public int minMapPerc = 75; // Minimum Map Percentage.

    private PathGenerator pathGenerator;
    ///private bool[,] visitedCells;
    private Dictionary<char, GameObject> dungeonGameObjects = new Dictionary<char, GameObject>();

    public int spriteSizeX = 1;
    public int spriteSizeY = 1;

    public int xPosition = 0;
    public int yPosition = 0;

    public bool allowGeneration = true;
    // Use this for initialization
    void Start()
    {
        pathGenerator = GetComponent<PathGenerator>();

        LoadDungeonCharacterMappings();
        //GenerateDungeon();
        //mapGenerator.DisplayMap();
        InstantiateDungeonPieces();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LoadDungeonCharacterMappings()
    {
        foreach (GameObject dungeonShape in dungeonShapes)
        {
            char gameObjectCharacter = dungeonShape.GetComponent<DungeonCharacterMapping>().characterMapping;
            dungeonGameObjects.Add(gameObjectCharacter, dungeonShape);
        }
    }

    //private void GenerateDungeon()
    //{
    //    //int visitedCellsCount = 0; // Aantal bezochte cellen.

    //    //int enoughMap = (pathGenerator.mapLength - 2) * (pathGenerator.mapHeight - 2) * minMapPerc / 100; // Zorgt ervoor dat een map altijd minstens minMapPerc van de mogelijke ruimte in beslag neemt.
    //    // int failSafe = 0; // Failsafe om te voorkomen dat de map in een infinite loop komt.

    //    //while (visitedCellsCount < enoughMap && failSafe < 500) // Zolang de map niet groot genoeg is en er minder dan 500x geprobeerd is deze te verbeteren.
    //    // {
    //    //   if (failSafe > 0) Debug.Log("Recreating map, " + visitedCellsCount + " rooms is too small, it needs at least " + enoughMap + " rooms... ;("); // Als er 1 of meer keer is geprobeerd een map te maken en deze te klein is.
    //    pathGenerator.InitializeMap(rooms, roomTries); // Initialiseren van de map.
    //                                   //visitedCells = pathGenerator.TraverseMap(); // Kijken naar hoe de map afgelopen kan worden.
    //                                   //visitedCellsCount = pathGenerator.GetVisitedCellsCount(visitedCells); // Hoe veel kamers kunnen er berijkt worden in de de map.
    //                                   //    failSafe++; // Optellen van aantal pogingen.
    //}

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
