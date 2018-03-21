using System.Collections.Generic;
using UnityEngine;

public class PlaceEntities : MonoBehaviour {
    private List<DungeonRoom> roomList = new List<DungeonRoom>();
    public GameObject player;
	// Use this for initialization
	void Start () {
        roomList = GetComponent<PathGenerator>().RoomList;
        SpawnPlayer();
	}



    // Update is called once per frame
    void Update () {
		
	}
    private void SpawnPlayer() {
        Instantiate(player);
        player.transform.position = new Vector3Int(roomList[0].posX + (roomList[0].width / 2), roomList[0].posY + (roomList[0].height / 2), 0);
    }
}
