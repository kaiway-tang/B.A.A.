using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitbox : MonoBehaviour
{
    public player player;
    public GameObject[] hearts;
    public GameObject playerObj;
    public SpriteRenderer playSpr;
    int del;
    public static bool invulnerable;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Tab) && Input.GetKeyDown(KeyCode.G)) //toggles invulnerability for demonstration / testing purposes
        {
            if (invulnerable) { invulnerable = false; }
            else
            {
                invulnerable = true;
            }
        }
    }

    void FixedUpdate()
    {
        if (invulnerable)
        {
            del = 2;
        }
        if (del>0) { del--;
            if (del % 2 == 0) { //makes the player's character blink rapidly after taking damage
                if (del % 4 == 0)
                {
                    playSpr.enabled = true;
                }
                else
                {
                    playSpr.enabled = false;
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.gameObject.tag=="nmyAtk"|| col.gameObject.tag == "bumpNmy") &&del<1)
        {
            if (col.gameObject.tag == "bumpNmy"&&player.del>0) { return; }
            finalScene.damageOnes++;
            //makes the player briefly invulnerable to enemies that deal damage on contact so that the dash on the player's third swipe doesn't harm itself
            del = 40; //makes the player briefly invulnerable after taking damage
            player.disable = 12;//makes the player briefly lose control of the character after taking damage
            player.hp--;
            manager.outs[2].SetActive(true);
            if (player.hp > -1) { hearts[player.hp].SetActive(false); } //disables one of the heart health indiactors
            if (player.hp<=0) { finalScene.deathsOnes++; manager.reset=true; playerObj.SetActive(false); }
        }
    }
}
