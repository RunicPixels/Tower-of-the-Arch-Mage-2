using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exitgame : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
            ExitGame();
        if (Input.GetKey("r"))
        {
            ResetGame();
        }
    }
    public static void ExitGame()
    {
        Application.Quit();
    }
    public static void ResetGame()
    {
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    }
}
