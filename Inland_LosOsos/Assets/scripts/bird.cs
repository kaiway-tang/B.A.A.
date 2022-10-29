using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bird : MonoBehaviour
{
    public tweeterMode tweeterMode;
    public GameObject destroy;
    public Color fade;
    public AudioSource speaker;
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag=="stone")
        {
            finalScene.damageOnes++; //failing tweeter mode counts as taking damage when calculating the final hero score
            tweeterMode.ride = false;
            manager.reset = true;
            //reset the scene if the bird crashes into the wrong stone
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag=="stone")
        {
            speaker.Play();
            col.gameObject.AddComponent<Rigidbody2D>().gravityScale = 5; //makes the stone fall if the correct one is hit
            col.gameObject.GetComponent<SpriteRenderer>().color = fade; //makes the slightly transparent and green
        }
    }
}
