using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class music: MonoBehaviour
{
    public static float musicVol;
    public AudioClip[] songs;
    public static bool nextSong;
    int song;
    public static AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (nextSong) //switches to the next track
        {
            audioSource.clip = songs[song];
            if (song==5) { audioSource.volume = 1; }
            //increases the volume if playing the outro track because no other gameplay sound effects are playing
            audioSource.Play();
            song++;
            nextSong = false;
        }
        if (!audioSource.isPlaying) //if the song ends, restarts the song
        {
            audioSource.Play();
        }
    }
}
