using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision_Landing : MonoBehaviour {
    
    public Player _Player;

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject != _Player.gameObject) {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") && collision.gameObject.tag != "Slope") {
                _Player.OnLand();
            }
        }
    }
}
