using System.Collections.Generic;
using UnityEngine;

public class PlaceEntities : MonoBehaviour {
    private List<DungeonRoom> roomList = new List<DungeonRoom>();
    public int numberOfEnemies = 5;
    public int numberOfDoors = 10;
    private PathGenerator pathGen;
    public GameObject playerObject;
    private GameObject playerInstance;
    public GameObject enemyObject;
    private GameObject[] enemyInstance;
    public GameObject doorObject;
    private List<GameObject> doorInstance;
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
