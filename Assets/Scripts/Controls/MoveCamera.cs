using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour {

    public int speed = 1;
    public int cooldownTime = 10;
    private int cooldown;
    
	// Use this for initialization
	void Start () {
         cooldown = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown == 0)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Translate(new Vector3(speed, 0, 0));
                cooldown += cooldownTime;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Translate(new Vector3(-speed, 0, 0));
                cooldown += cooldownTime;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.Translate(new Vector3(0, -speed, 0));
                cooldown += cooldownTime;
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.Translate(new Vector3(0, speed, 0));
                cooldown += cooldownTime;
            }
        }
        else
        {
            cooldown--;
        }
    }
}
