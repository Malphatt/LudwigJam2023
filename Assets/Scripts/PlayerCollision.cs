using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {

    public Player _Player;

    bool sloped;

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject != _Player.gameObject) {
            if (collision.gameObject.tag != "Balls 0.0") {
                if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") && collision.gameObject.tag == "Slope") {
                    sloped = true;
                    _Player.Sloped(sloped);
                }
                if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") && collision.gameObject.tag != "Slope") {
                    if (!_Player.IsGounded()) {
                        if (collision.gameObject.tag != "Cloud") {
                            _Player.OnBonk();
                        }
                    }
                }
            } else {

                _Player.HitPlayer(1.0f);
                _Player.HitPower(7.5f, 1);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject != _Player.gameObject) {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") && collision.gameObject.tag == "Slope") {
                sloped = false;
                _Player.Sloped(sloped);
            }
        }
    }
}
