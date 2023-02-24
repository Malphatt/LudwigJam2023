using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour {

    public GameObject CloudPrefab;
    public bool RightMovingSpwaner;
    public float SpawnTime;
    public float StartDelay;
    float timeReleasedLast;
    bool releaseClouds;

    void Awake() {
        Invoke("ReleaseClouds", StartDelay);
    }

    void Update() {
        if (releaseClouds) {
            if (Time.time - timeReleasedLast > SpawnTime) {
                //Spawn a cloud
                GameObject cloud = Instantiate(CloudPrefab, transform.position, Quaternion.identity);
                //Set the cloud's direction
                cloud.GetComponent<cloud>().MovingRight = RightMovingSpwaner;
                //Reset the timer
                timeReleasedLast = Time.time;
            }
        }
    }

    void ReleaseClouds() {
        releaseClouds = true;
        timeReleasedLast = Time.time;
    }
}
