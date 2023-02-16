using UnityEngine;

public class metronome : MonoBehaviour
{
    public float bpm = 60f;
    public float rotationSpeed = 180f;

    private float timePerBeat;
    private float timer;

    private bool isForward = true;

    void Start()
    {
        timePerBeat = 60f / bpm;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timePerBeat)
        {
            timer -= timePerBeat;
            isForward = !isForward;
            //Play sound effect
            GetComponent<AudioSource>().Play();
        }

        float angle = Mathf.Lerp(135f, -45f, isForward ? timer / timePerBeat : 1 - timer / timePerBeat);
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    }
}