using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {

    public Player _Player;
    public bool IsSideCollider;
    bool touchingWall;

    void OnTriggerEnter2D(Collider2D collision) {
        if (IsSideCollider) {
            Debug.Log("Side Collider Entered");
            if (!touchingWall) {
                if (collision.gameObject.layer == LayerMask.NameToLayer("Ground")) {
                    touchingWall = true;
                    _Player.OnBonk();
                }
            }
        } else {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Ground")) {
                _Player.ResetJump();
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision) {
        if (IsSideCollider) {
            Debug.Log("Side Collider Exited");
            if (collision.gameObject.layer == LayerMask.NameToLayer("Ground")) {
                touchingWall = false;
            }
        }
    }
}