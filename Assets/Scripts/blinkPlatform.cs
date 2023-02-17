using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blinkPlatform : MonoBehaviour
{
    public bool startDisabled;
    private Material colRender;
    //Toggle the hitbox of the object
    public void toggleHitbox()
    {
        GetComponent<BoxCollider2D>().enabled = !GetComponent<BoxCollider2D>().enabled;
        //toggle opacity
        if (colRender.color.a == 1)
        {
            colRender.color = new Color(colRender.color.r, colRender.color.g, colRender.color.b, 0.3f);
        }
        else
        {
            colRender.color = new Color(colRender.color.r, colRender.color.g, colRender.color.b, 1f);
        }
    }

    void Awake()
    {
        //Get Material
        colRender = GetComponent<SpriteRenderer>().material;
        if (startDisabled)
        {
            //Turn off hitbox
            GetComponent<BoxCollider2D>().enabled = false;
            colRender.color = new Color(colRender.color.r, colRender.color.g, colRender.color.b, 0.3f);
        }
    }
}
