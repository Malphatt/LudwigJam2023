using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rubberBandGen : MonoBehaviour
{
    // Rubber band prefab
    public GameObject rubberBandPrefab;
    // Rubber band spawn point
    public Transform rubberBandSpawnPoint;
    // Rubber band spawn rate
    public float rubberBandSpawnRate = 7f;

    // Every 5 seconds generate a rubber band
    void Start()
    {
        InvokeRepeating("generateRubberBand", 1f, rubberBandSpawnRate);
    }

    // Generate a rubber band
    void generateRubberBand()
    {
        // Instantiate rubber band
        GameObject rbb = Instantiate(rubberBandPrefab, rubberBandSpawnPoint.position, rubberBandSpawnPoint.rotation);

        //Add force to rubber band away from generator
        rbb.GetComponent<Rigidbody2D>().AddForce(rubberBandSpawnPoint.right * 500f);
    }
}
