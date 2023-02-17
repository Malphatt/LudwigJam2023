using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject timer;
    public float accumulatedTime;
    private blinkPlatform[] blinkPlatforms;

    void Start()
    {
        //Add all platform prefabs to the list
        blinkPlatforms = FindObjectsOfType<blinkPlatform>();
        
    }
    // Update is called once per frame
    void Update()
    {
        accumulatedTime += Time.deltaTime;
        //Format time in 00:00:00:00
        timer.GetComponent<TextMeshProUGUI>().text = string.Format("{0:00}:{1:00}:{2:00}", accumulatedTime / 3600, (accumulatedTime % 3600) / 60, (accumulatedTime % 60));

        //Every second call function
        if (accumulatedTime % 2 < Time.deltaTime)
        {
            //Call function on all platforms
            foreach (blinkPlatform platform in blinkPlatforms)
            {
                platform.toggleHitbox();
            }
        }
    }
}
