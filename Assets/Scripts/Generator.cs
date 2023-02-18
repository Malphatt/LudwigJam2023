using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    // Rubber band prefab
    public GameObject Prefab;
    // Rubber band spawn point
    public Transform SpawnPoint;
    // Rubber band spawn rate
    public float SpawnRate = 7f;

    // Every 5 seconds generate a rubber band
    void Start()
    {
        InvokeRepeating("generateRubberBand", 1f, SpawnRate);
    }

    // Generate a rubber band
    void generateRubberBand()
    {
        // Instantiate rubber band
        GameObject ball = Instantiate(Prefab, SpawnPoint.position, SpawnPoint.rotation);

        //Add force to rubber band away from generator
        ball.GetComponent<Rigidbody2D>().AddForce(SpawnPoint.right * 500f);
    }
}
