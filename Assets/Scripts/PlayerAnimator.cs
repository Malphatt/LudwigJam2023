using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour {

    Player _Player;

    public float _BPM;
    float secondsPerFrame;

    int frame = 0;
    int frameCount = 8;

    public Sprite[] _Idle = new Sprite[8];
    public Sprite[] _Walk = new Sprite[8];

    float idleTimer = 10.0f;
    public Sprite[] _IdleTooLong = new Sprite[8];

    float timeLastMoving;


    void OnEnable() {
        StartCoroutine(IncrementFrame());
    }

    IEnumerator IncrementFrame() {

        _Player = GetComponent<Player>();
        secondsPerFrame = (60 / _BPM) / 4;

        while (true) {
            yield return new WaitForSeconds(secondsPerFrame);
            if (frame == frameCount - 1) {
                frame = 0;
            } else {
                frame++;
            }
        }
    }

    void Update() {
        if (_Player.IsGounded()) {
            if (_Player.IsStationary()) {
                if (Time.time - timeLastMoving > idleTimer) { // If player has been stationary for idleTimer seconds, show idle animation
                    GetComponent<SpriteRenderer>().sprite = _IdleTooLong[frame];
                }
                 else { // If player has been stationary for less than idleTimer seconds, show idle animation
                    GetComponent<SpriteRenderer>().sprite = _Idle[frame];
                 }
            } else {
                timeLastMoving = Time.time;
                GetComponent<SpriteRenderer>().sprite = _Walk[frame];
            }
        } else {
            timeLastMoving = Time.time;
        }
        if (_Player.LastMovingDirection() == 1) { // Make sprite face right
            GetComponent<SpriteRenderer>().flipX = false;
        } else if (_Player.LastMovingDirection() == -1) { // Make sprite face left
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }
}
