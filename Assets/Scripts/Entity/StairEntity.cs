using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class StairEntity : Entity {

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            Exitgame.ResetGame();
        }
    }
}

