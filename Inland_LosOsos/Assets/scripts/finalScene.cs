using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class finalScene : MonoBehaviour
{
    public static int secOnes;
    public static int secTens;
    public static int secHuns;
    public static int damageOnes;
    public static int damageTens;
    public static int damageHuns;
    public static int deathsOnes;
    public static int deathsTens;
    public SpriteRenderer[] places;
    public SpriteRenderer[] placesDmg;
    public SpriteRenderer[] placesDth;
    public SpriteRenderer[] placesScore;
    public int[] score;
    public Sprite[] numbers;
    public float heroScore;
    int del;
    public SpriteRenderer blackSq;
    public Color fade;
    public int timer;
    public GameObject[] scores;
    public GameObject[] revealFX;
    public ParticleSystem[] revealPtcl;
    int revealDel;
    public Color fade2;
    public SpriteRenderer[] scoreTexts;
    public Transform scrollText;
    public static float multiplier;
    // Start is called before the first frame update
    void Start()
    {
        revealDel = 120; //this variable allows for a flat delay before the scores are revealed
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Escape)) { //returns to the start screen and resets the game
            player.extraHealth = false;
            player.sprint = false;
            Aegis.shieldSlam= false;
            wizard.maxHP = 4; //resets any powerups the player may have received throughout the game
            secOnes = 0;
            secTens= 0;
            secHuns = 0;
            damageOnes = 0;
            damageHuns = 0;
            damageTens = 0;
            deathsOnes = 0;
            deathsTens = 0; //resets all the tracked values (time, total damage taken, total deaths)
            SceneManager.LoadScene("firstScene"); }
        if (timer>430&&timer<478)
        {
            fade2.a -= 0.02f;
            foreach (SpriteRenderer i in places)
            {
                i.color = fade2;
            }
            foreach (SpriteRenderer i in placesDmg)
            {
                i.color = fade2;
            }
            foreach (SpriteRenderer i in placesDth)
            {
                i.color = fade2;
            }
            foreach (SpriteRenderer i in scoreTexts)
            {
                i.color = fade2;
            }
            //fades out the scores and numbers (other than the hero score) to a light transparency to allow the credits to scroll over them visibly
        }
        if (timer>480&&timer<2861)
        {
            scrollText.position += new Vector3(0,0.02f,0);
            //moves the text containing credits, etc. up
        }

        revealScores();

        if (timer<150)
        {
            fade.a -= 1f/150f;
            blackSq.color = fade; //creates the fade in effect when the scene starts
        }
        timer++;
        if (del < 5) {

            if (secOnes<9) { places[0].sprite = numbers[secOnes]; }
            if (secTens < 9) { places[1].sprite = numbers[secTens]; }
            if (secHuns < 9) { places[2].sprite = numbers[secHuns]; }
            if (damageOnes< 9) { placesDmg[0].sprite = numbers[damageOnes]; }
            if (damageTens < 9) { placesDmg[1].sprite = numbers[damageTens]; }
            if (damageHuns < 9) { placesDmg[2].sprite = numbers[damageHuns]; }
            if (deathsOnes < 9) { placesDth[0].sprite = numbers[deathsOnes]; }
            if (deathsTens < 9) { placesDth[1].sprite = numbers[deathsTens]; }
            //sets the sprites of place value objects to numbers corresoping to the value of the place value
            if (del == 1)
            {
                secHuns = secOnes / 100;
                secOnes -= secHuns * 100;
                secTens = secOnes / 10;
                secOnes -= secTens * 10;

                deathsTens = deathsOnes / 10;
                deathsOnes -= deathsTens * 10;

                damageHuns = damageOnes / 100;
                damageOnes -= damageHuns * 100;
                damageTens = damageOnes / 10;
                damageOnes -= damageTens * 10;
            }
            del++; }else
        {
            if (del == 5) { heroScore = 20000f / (secOnes + secTens * 10f + secHuns * 100f) * 60f / (damageOnes + damageTens * 10f +damageHuns*100+ 1f) / (deathsOnes + deathsTens * 10f + 1f)*multiplier; del = 6; }
            //calculates the hero score, 20000 / time in minutes / damage taken + 1 / deaths + 1
            if (del==6)
            {
                if (heroScore > 1000) { heroScore -= 1000; score[3]++; }
                else
                if (heroScore > 100) { heroScore -= 100; score[2]++; }
                else
                    if (heroScore > 10) { heroScore -= 10; score[1]++; }
                else
                    if (heroScore > 1) { heroScore -= 1; score[0]++; }
                else { del = 7; }
                //converts the single hero score value to place values
            }
            if (del==7)
            {
                for (int i = 0; i < 4; i++)
                {
                    placesScore[i].sprite = numbers[score[i]];
                    //sets the sprites of place value objects to numbers corresoping to the value of the place value
                }
            }
        }
    }
    void revealScores()
    {
        for (int i = 0; i < 4; i++)
        {
            if (timer==revealDel+i*70)
            {
                revealFX[i].SetActive(true);
                break;
            }
            if (timer == revealDel + i * 70+10)
            {
                scores[i].SetActive(true);
                break;
            }
            if (timer == revealDel + i * 70 + 20)
            {
                revealPtcl[i*2].loop = false;
                revealPtcl[i * 2 + 1].loop = false;
                break;
            }
        }
    }
}
