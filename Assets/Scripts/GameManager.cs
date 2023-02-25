using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public GameObject timer;
    public float accumulatedTime;
    private blinkPlatform[] blinkPlatforms;
    public int zoneCount;
    public GameObject head;
    public GameObject progressBar;
    private bool zone8reached;
    public GameObject Text;
    
    // Input
    public PlayerInput _PlayerInput;
    InputAction _togMenu;
    public GameObject menu;

    //Zone
    public GameObject zone;
    public void setZone(GameObject zone)
    {
        this.zone = zone;
    }

    void Awake()
    {
        //Add all platform prefabs to the list
        blinkPlatforms = FindObjectsOfType<blinkPlatform>();
        _PlayerInput = new PlayerInput();
        zone.GetComponent<AudioSource>().Play();
    }
    void OnEnable()
    {

        _togMenu = _PlayerInput.Player.ToggleMenu;
        _togMenu.Enable();
    }

    void OnDisable()
    {
        _togMenu.Disable();
    }

    void ToggleMenu()
    {
        menu.SetActive(!menu.activeSelf);
    }
    // Update is called once per frame
    void Update()
    {
        //Toggle menu
        _togMenu.performed += ctx => ToggleMenu();

        //Get last character of zone name
        zoneCount = int.Parse(zone.name[zone.name.Length - 1].ToString());
        head.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 115*(zoneCount-4), 0);

        //Update timer unless menu is open or zone is 8
        if (!menu.activeSelf && zoneCount != 8)
        {
            accumulatedTime += Time.deltaTime;
            //Format time in 00:00:00
            string minutes = Mathf.Floor(accumulatedTime / 60).ToString("00");
            string seconds = (accumulatedTime % 60).ToString("00");

            timer.GetComponent<TextMeshProUGUI>().text = string.Format("{0}:{1}", minutes, seconds);


            //Enable progress bar and head
            head.SetActive(true);
            progressBar.SetActive(true);
        }
        else
        {
            //Disable progress bar and head
            head.SetActive(false);
            progressBar.SetActive(false);
        }

        
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
            if (!zone8reached)
            {
                //Play zone's audio source
                zone.GetComponent<AudioSource>().Play();
            }
            
            if (zoneCount == 8)
            {
                zone8reached = true;
            }

        }

        //Update the text mesh pro depending on how much time has accumulated in minutes
        if (accumulatedTime / 60 > 7)
        {
            Text.GetComponent<TextMeshProUGUI>().text = "Let's gooooo! Coots, already climbing up to chill with me within seven minutes of the stream starting! That's what I call a good start to the day!";
        }
        else if (accumulatedTime / 60 > 10)
        {
            Text.GetComponent<TextMeshProUGUI>().text = "Oh, Coots is here. That was pretty fast. I've seen him get here faster though\"\r\n";
        }
        else if (accumulatedTime / 60 > 15)
        {
            Text.GetComponent<TextMeshProUGUI>().text = "Hey, look who decided to grace us with their presence. Coots finally made it up here.";
        }
        else if (accumulatedTime / 60 > 20)
        {
            Text.GetComponent<TextMeshProUGUI>().text = "Well, it took Coots long enough, but I guess they finally decided to join the stream. This was only supposed to take 10 minutes";
        }
        else if (accumulatedTime / 60 > 25)
        {
            Text.GetComponent<TextMeshProUGUI>().text = "Oh, Coots finally made it up here. I was starting to think they forgot who feeds them. This was only supposed to take 10 minutes";
        }
        else
        {
            Text.GetComponent<TextMeshProUGUI>().text = "Wow you got up here fast coots! Are you some kind of jump king or something?";
        }
    }
}
