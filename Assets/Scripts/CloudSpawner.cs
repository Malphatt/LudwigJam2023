using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour {

    public GameObject CloudPrefab;
    public bool RightMovingSpwaner;
    float spawnTime = 2.5f;

    void Update() {
        if (Time.time % spawnTime < Time.deltaTime) {
            //Spawn a cloud
            GameObject cloud = Instantiate(CloudPrefab, transform.position, Quaternion.identity);
            //Set the cloud's direction
            cloud.GetComponent<cloud>().MovingRight = RightMovingSpwaner;
        }
    }
}
