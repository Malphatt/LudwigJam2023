using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class ludwigController : MonoBehaviour
{
    public GameObject Text;
    public GameObject End;

    //Endgame coroutine
    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(1);
        Text.SetActive(true);
        End.SetActive(true);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            //Stop the player moving on the x direction
            other.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;

            //Wait 3 seconds then enable the text and end
            StartCoroutine(EndGame());
        }
    }
}
