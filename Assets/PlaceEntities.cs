using System.Collections.Generic;
using UnityEngine;

public class PlaceEntities : MonoBehaviour {
    private List<DungeonRoom> roomList = new List<DungeonRoom>();
    public int numberOfEnemies = 5;
    private PathGenerator pathGen;
    public GameObject playerObject;
    private GameObject playerInstance;
    public GameObject enemyObject;
    private GameObject[] enemyInstance;
	// Use this for initialization
	void Start () {
        pathGen = GetComponent<PathGenerator>();
        roomList = pathGen.RoomList;
        SpawnPlayer();
        SpawnEnemies(numberOfEnemies);
	}



    // Update is called once per frame
    void Update () {
		
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
