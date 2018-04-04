using System.Collections.Generic;
using UnityEngine;

public class PlaceEntities : MonoBehaviour {
    public int numberOfEnemies = 5;
    private List<DungeonRoom> roomList = new List<DungeonRoom>();
    private PathGenerator pathGen;

    //Player
    public GameObject playerObject;
    private GameObject playerInstance;

    //Enemies
    public GameObject enemyObject;
    private GameObject[] enemyInstance;

    //Doors
    public GameObject doorObject;
    private List<GameObject> doorInstance;

    //Stairs
    public GameObject stairObject;
    private GameObject stairInstance;

	// Use this for initialization
	void Start () {
        pathGen = GetComponent<PathGenerator>();
        roomList = pathGen.RoomList;
        SpawnObjects();
        SpawnPlayer();
        SpawnEnemies(numberOfEnemies);
	}

    private void SpawnObjects() {
        doorInstance = new List<GameObject>();
        for (int i = 0; i < roomList.Count; i++) {
            List<Vector2Int> doorOpenings = DungeonUtilities.FindRoomOpenings(pathGen.map, roomList[i]);
            foreach(Vector2Int opening in doorOpenings) {
                Vector3 doorPosition = new Vector3(opening.x, opening.y);
                GameObject door = Instantiate(doorObject, doorPosition, Quaternion.identity);
                doorInstance.Add(door);
            }
            
        }
        stairInstance = Instantiate(stairObject);
        stairInstance.transform.position = new Vector3(roomList[roomList.Count - 1].posX + Random.Range(0, roomList[roomList.Count - 1].width), (roomList[roomList.Count - 1].posY + Random.Range(0, roomList[roomList.Count-1].height)), 0);
    }
    private void SpawnPlayer() {
        playerInstance = Instantiate(playerObject);
        playerInstance.transform.position = new Vector3(roomList[0].posX + (roomList[0].width / 2), (roomList[0].posY + (roomList[0].height / 2)), 0);
    }

    private void SpawnEnemies(int numberOfEnemies) {
        enemyInstance = new GameObject[numberOfEnemies];
        for(int i = 0; i < numberOfEnemies; i++) {
            enemyInstance[i] = Instantiate(enemyObject);
            int room = Random.Range(0,roomList.Count-1) + 1;
            enemyInstance[i].transform.position = new Vector3(roomList[room].posX + Random.Range(0, roomList[room].width), (roomList[room].posY + Random.Range(0, roomList[room].height)), 0);
        }
    }

}
