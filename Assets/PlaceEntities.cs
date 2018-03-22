using System.Collections.Generic;
using UnityEngine;

public class PlaceEntities : MonoBehaviour {
    private List<DungeonRoom> roomList = new List<DungeonRoom>();
    private PathGenerator pathGen;
    public GameObject playerObject;
    private GameObject playerInstance;
	// Use this for initialization
	void Start () {
        pathGen = GetComponent<PathGenerator>();
        roomList = pathGen.RoomList;
        SpawnPlayer();
	}



    // Update is called once per frame
    void Update () {
		
	}
    private void SpawnPlayer() {
        playerInstance = Instantiate(playerObject);
        playerInstance.transform.position = new Vector3(roomList[0].posX + (roomList[0].width / 2), (roomList[0].posY + (roomList[0].height / 2)), 0);
    }
}
