using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject timer;
    public float accumulatedTime;
    private blinkPlatform[] blinkPlatforms;
    public int zoneCount;
    public GameObject head;

    public GameObject zone;
    public void setZone(GameObject zone)
    {
        this.zone = zone;
    }

    void Awake()
    {
        //Add all platform prefabs to the list
        blinkPlatforms = FindObjectsOfType<blinkPlatform>();
        zone.GetComponent<AudioSource>().Play();
    }
    // Update is called once per frame
    void Update()
    {
        //Get last character of zone name
        zoneCount = int.Parse(zone.name[zone.name.Length - 1].ToString());
        head.GetComponent<RectTransform>().position = new Vector3(head.transform.position.x, 115*zoneCount + 70, 0);
        
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

        //Every 16 seconds
        if (accumulatedTime % 16 < Time.deltaTime)
        {
            //Play zone's audio source
            zone.GetComponent<AudioSource>().Play();
        }
    }
}
