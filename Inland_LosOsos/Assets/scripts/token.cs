using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class token : MonoBehaviour
{
    public int del;
    public static int tokenNum;
    public Sprite[] tokens;
    public Sprite[] questions;
    public SpriteRenderer tokenFound;
    public Sprite[] tokensFound;
    public Sprite[] powerUp;
    public SpriteRenderer[] sides; //0:front of pin 1: back of pin
    public static int correct;
    public GameObject[] buttons;
    public Color fade;
    public SpriteRenderer screenCover;
    public Sprite[] wrongAns;
    
    // Start is called before the first frame update
    void Start()
    {
        del = -40; //sets a delay for the token flip animation to play
        correct = 0;
        tokenFound.sprite = tokensFound[tokenNum];
        GetComponent<SpriteRenderer>().sprite = tokens[tokenNum];
        tokenNum++;
    }

    void Update()
    {
    }
    void FixedUpdate()
    {
        if (del < 70)
        {
            if (del > 0) {transform.localScale += new Vector3(0.02f, 0.02f, 0); //"zoome in" to the pin
            }
            del++;
            if (del > 10)
            {
                if (del == 69) { buttons[tokenNum - 1].SetActive(true); }
                transform.Rotate(Vector3.up * 3);
                //rotates the token giving the impression it is being flipped over
                if (del == 40)
                {
                    transform.Rotate(Vector3.up * 180); //roates the token 180 so that it itsn't reflected
                    transform.localScale = new Vector3(transform.localScale.x / 9, transform.localScale.x / 9, 1);
                    GetComponent<SpriteRenderer>().sprite = questions[tokenNum - 1];
                }
            }
        }
        if (correct>0)
        {
            if (del < 130)
            {
                del++;
                transform.Rotate(Vector3.up * 3);
                if (del==100) { transform.Rotate(Vector3.up * 180);
                    if (correct == 1)
                    {
                        GetComponent<SpriteRenderer>().sprite = powerUp[tokenNum - 1];
                        if (tokenNum == 1) { player.extraHealth = true; wizard.maxHP= 5; }
                        if (tokenNum == 2) { player.sprint = true; }
                        if (tokenNum == 3) { Aegis.shieldSlam = true; }
                        if (tokenNum == 4) { finalScene.multiplier= 1.1f; }
                    } else
                    {
                        if (tokenNum==1) { wizard.maxHP = 4; }
                        if (tokenNum == 4) { finalScene.multiplier = 1; }
                        GetComponent<SpriteRenderer>().sprite = wrongAns[tokenNum - 1];
                    }
                }
            } else
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    if (tokenNum == 2) { SceneManager.LoadScene("tokenFound"); }
                    del = 131;
                }
            }
        }
        if (del>130)
        {
            del++;
            fade.a += 0.02f;
            screenCover.color = fade;
            if (del==180)
            {
                if (tokenNum == 1) { SceneManager.LoadScene("tweeter"); }
                if (tokenNum == 3) { SceneManager.LoadScene("Recruit"); }
                if (tokenNum == 4) { music.nextSong = true;
                    SceneManager.LoadScene("finalScene"); }
            }
        }
    }
}
