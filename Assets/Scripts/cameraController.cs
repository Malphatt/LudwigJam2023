using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        //Move up with the player
        transform.position = new Vector3(transform.position.x, player.transform.position.y+3f, transform.position.z);
    }
}
