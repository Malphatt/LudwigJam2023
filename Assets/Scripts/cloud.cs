using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class cloud : MonoBehaviour
{
    
    public bool MovingRight;
    public Sprite[] CloudSprites;
    public GameObject[] CloudColliders;
    float cloudSpeed = 1.5f;
    float cloudLifetime = 20.0f;
    Rigidbody2D _rb;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        //Pick a random cloud sprite and apply it
        int cloud = Random.Range(0, CloudSprites.Length);
        GetComponent<SpriteRenderer>().sprite = CloudSprites[cloud];

        // Enable the collider for the cloud
        CloudColliders[cloud].SetActive(true);

        //Set the Lifetime of the cloud
        Destroy(gameObject, cloudLifetime);

    }
        
    // Update is called once per frame
    void Update()
    {
        //Move the cloud
        if (MovingRight)
        {
            transform.Translate(Vector2.right * cloudSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * cloudSpeed * Time.deltaTime);
        }
    }
}
