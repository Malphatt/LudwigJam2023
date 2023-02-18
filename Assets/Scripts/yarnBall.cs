using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yarnBall : MonoBehaviour
{
    //On collision with player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //If player is not dead
        if (collision.gameObject.tag == "Player")
        {
            //Fade away over 2 seconds
            StartCoroutine(fadeAway(2));
        }

        //If hit ball
        if (collision.gameObject.tag == "Balls 0.0")
        {
            //Fade away over 2 seconds
            StartCoroutine(fadeAway(2));
        }
    }

    //Fade away over time
    IEnumerator fadeAway(float time)
    {
        //Disable collider
        GetComponent<Collider2D>().enabled = false;

        //Get sprite renderer
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        //Get current color
        Color color = spriteRenderer.color;

        //Loop over time
        for (float t = 0.01f; t < time; t += Time.deltaTime)
        {
            //Set color to fade
            spriteRenderer.color = new Color(color.r, color.g, color.b, Mathf.Lerp(1, 0, t / time));
            //Wait for next frame
            yield return null;
        }
        //Set color to transparent
        spriteRenderer.color = new Color(color.r, color.g, color.b, 0);
        //Delete self
        Destroy(gameObject);
    }

    //Update
    void Update()
    {
        //Spin
        transform.Rotate(0, 0, 100 * Time.deltaTime * GetComponent<Rigidbody2D>().velocity.magnitude);
    }
}
