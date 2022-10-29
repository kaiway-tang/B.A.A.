using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class portal : MonoBehaviour
{
    public int function; //0: switches between scences; 1: teleports player to location 2: creates the 3 size array for finalScene script 3: america boss blacks out screen
    public bool block;
    public string scene;//scene to load when player enters portal
    public GameObject space;
    public Transform end;
    bool enter;// functions like an onCollisionEnter thing so that the code only runs once
    bool spacePressed;
    public static int del;
    int loadNext;
    public Color fade;
    float fadeVar;
    public SpriteRenderer blueGLow;
    public bool dontChangeSong;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) { spacePressed = true; } //keydown doesnt work in fixed update, this is a workaround
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        fadeVar+=0.02f;
        fade.a = Mathf.Abs(fadeVar);
        blueGLow.color=fade;
        if (fadeVar>=1) { fadeVar = -1; }
        //plays the fade in and out glow effect on the portal

        if (loadNext>0)
        {
            loadNext--;
            if (loadNext==1)
            {
                SceneManager.LoadScene(scene); //loads the next scene indicated by the portal
            }
        }
        if (!block)
        {
            if (Vector3.Distance(transform.position, manager.playTrans.position) < 2) //test if player is close enough to use portal
            {
                if (!enter)
                {
                    space.SetActive(true); //creates the press "space" text above portal
                    enter = true;
                }
                if (spacePressed)
                {
                    if (function == 0)
                    {
                        if (!dontChangeSong) { music.nextSong = true; }
                        GetComponent<AudioSource>().Play(); //plays the portal sound
                        player.end = 25;
                        loadNext = 25; //delay allows for the blacking-out-screen scene transition
                        space.SetActive(false);
                    } else if (function==1&&del<1)
                    {
                        del = 25; //delay preventing a player from entering one portal and traveling instantly back through the end portal
                        manager.playTrans.position = end.position; //teleports the player to the portal's coresponding location
                        manager.Aegis.position = end.position; //teleports aegis to the portal's coresponding location
                    }else if (function==2)
                    {
                        GetComponent<AudioSource>().Play(); //plays the portal sound
                        player.end = 25;
                        if (!dontChangeSong) { music.nextSong = true; }
                        loadNext = 25; //delay allows for the blacking-out-screen scene transition
                        space.SetActive(false);
                        finalScene.secOnes = 0;
                        finalScene.secTens = 0;
                        finalScene.secHuns = 0; //resets the timer after the tutorial stage becase it shouldnt count towards your score
                    } else  if (function==3)
                    {
                        music.nextSong = true;
                        manager.playObj.SetActive(false);
                        loadNext = 100; //delay allows for the blacking-out-screen scene transition
                        manager.outs[0].SetActive(true);
                    }
                }
            }
            else
            {
                if (enter) { space.SetActive(false); enter = false; } //if the player leaves the portal's range, disables the PRESS SPACE text
            }
        }
        if (del>0) { del--; }
        if (spacePressed) { spacePressed = false; }
    }
}
