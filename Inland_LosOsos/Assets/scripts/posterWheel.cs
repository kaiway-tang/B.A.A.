using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class posterWheel : MonoBehaviour
{
    public GameObject brackets; //the selector brackets indicating which piece needs to be found
    public GameObject[] finished; //portal and gameobject blocking the portal
    public Transform[] buttons; //the blinking circles on top of the poster pieces, used to test if player is close enough to "select" the piece
    public GameObject[] blocks; //the gray rectangles covering the poster under construction
    public SpriteRenderer[] pieces; //the spriteRenderers of the puzzle pieces allowing their colors to be changed
    public Color[] color; //the colors to change the puzzle pieces to when in/correctly chosen
    public int piece; //the piece the player is currently trying to find
    public Transform plyr; //the player's position to test if theyre close enough to select a piece
    public GameObject blockUncover; //the particle effects that play when a piece of the poster is solved
    int del;//timer to reset color after wrong piece is chosen
    int del1; //timer to reset color after right piece is chosen
    int del2;//timer to unblock portal after puzzle is finished
    int x; //
    int space;
    public AudioSource[] speaker;
    // Start is called before the first frame update
    void Start()
    {
        plyr = manager.playTrans;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            space = 1;
        }
    }
    void FixedUpdate()
    {
        if (space==1&&piece<6)
        {
            if (Vector3.Distance(plyr.position, buttons[piece].position) < 1.5f)
            {
                speaker[0].Play();
                pieces[piece].color = color[2]; del1 = 30;
                Instantiate(blockUncover, blocks[piece].transform.position, transform.rotation);
                blocks[piece].SetActive(false);
                piece ++;
                if (piece<6){ brackets.transform.position = blocks[piece].transform.position; }
                else { del2 = 250; brackets.SetActive(false); }
            } else
            {
                for (int i = 0; i < 6; i++)
                {
                    if (i>piece&&Vector3.Distance(plyr.position, buttons[i].position) < 1.5f) {
                        speaker[1].Play();
                        pieces[i].color = color[1]; x = i; del = 30;
                    }
                }
            }
        }
        if (del1==1) { pieces[piece - 1].color = color[3]; }
        if (space==1) { space = 0; }
        if (del==1) { pieces[x].color = color[0]; }
        if (del > 0) { del--; }
        if (del1 > 0) { del1--; }
        if (del2==90)
        {
            Instantiate(blockUncover, transform.position, transform.rotation);
        }
        else if (del2 == 60)
        {
            Instantiate(blockUncover, transform.position, transform.rotation);
        }
        else if(del2 == 30)
        {
            Instantiate(blockUncover, transform.position, transform.rotation);
            finished[0].SetActive(false);
            finished[1].GetComponent<portal>().block = false;
        }
        if (del2>0) { del2--; }
    }
}
