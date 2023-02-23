using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class cloud : MonoBehaviour
{
    // Cloud sprites
    public Sprite[] cloudSprites;
    public Transform end;
    bool cloudmoving;
    // Start is called before the first frame update
    void Start()
    {
        //Pick a random cloud sprite and apply it
        GetComponent<SpriteRenderer>().sprite = cloudSprites[Random.Range(0, cloudSprites.Length)];
        //Resize the collider2D to match sprite hitbox
        GetComponent<BoxCollider2D>().size = GetComponent<SpriteRenderer>().sprite.bounds.size;
        cloudmoving = false;
    }
        
    // Update is called once per frame
    void Update()
    {
        //Call every two seconds
        if (cloudmoving == false)
        {
            StartCoroutine(MoveCloud());
            cloudmoving = false;
        }
    }

    IEnumerator MoveCloud()
    {
        cloudmoving = true;
        //Slowly ove between start and end, then pingpong back again
        transform.position = Vector3.Lerp(transform.position, end.position, Mathf.PingPong(Time.time, 2) / 2);
        yield return new WaitForSeconds(20);
    }
}
