using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRandomizer : MonoBehaviour {
    public Sprite[] sprites;
    Sprite currentSprite;

	// Use this for initialization
	void Start () {
        //if(GameObject.Find("GameManager").GetComponent<PathVisualizer>().allowGeneration == true)
        //{
        //Randomize();
        //}
    }

    // Update is called once per frame
    //void Update () {
    //    Debug.Log("Allow Sprite Generation: " + GameObject.Find("GameManager").GetComponent<PathVisualizer>().allowGeneration);

	//}
    public void Randomize()
    {
        int sprite = Random.Range(0, sprites.Length);
        currentSprite = sprites[sprite];
        this.GetComponent<SpriteRenderer>().sprite = currentSprite;
        
    }
}
