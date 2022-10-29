using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public int function; //0:speak to the player only; 1: allies asking questions 2: prep for final boss npc
    public GameObject[] space; //the "press space bar" indicator, or the (press space) text
    public GameObject[] text;
    public int textNum;//which text block we're on
    public int[] nextDel;//delay for diplaying the (press space) text
    public int del;
    bool enter;// functions like an onCollisionEnter thing so that the code only runs once
    bool spaceDown;//since fixed update doesnt register getKeyDown reliably, this will allow getkeydown to run in fixed update
    public Transform[] options;
    bool click;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spaceDown = true;
        }
        if (Input.GetMouseButtonDown(0))
        {
            click = true;
        }
    }
    void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, manager.playTrans.position) < 4)
        {
            if (!enter)
            {
                space[0].SetActive(true);
                enter = true;
            }

            if (spaceDown)
            {
                if (function == 1)
                {
                    if (textNum != 4 && textNum < 6)
                    {
                        //if true, the player is answering a recruit's question so he shouldnt be able to SPACE through it
                        space[1].SetActive(false);
                        del = nextDel[textNum];
                        if (textNum > 0) { text[textNum - 1].SetActive(false); }
                        if (text[textNum]) { text[textNum].SetActive(true); }
                        textNum++;
                        space[0].SetActive(false);
                    }
                }
                else
                {
                    space[1].SetActive(false);
                    del = nextDel[textNum];
                    if (textNum > 0) { text[textNum - 1].SetActive(false); }
                    if (text[textNum]) { text[textNum].SetActive(true); }
                    textNum++;
                    space[0].SetActive(false);
                }
            }
            if (function == 1)
            {
                if (textNum == 4)
                {
                    if (click)
                    {
                        Vector3 mousePos = manager.cam.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);
                        if (Mathf.Abs(mousePos.y - options[0].position.y) < .35f)
                        //test for if player clicks on right answer (options[0] is always correct), shows correct answer text if true
                        {
                            space[1].SetActive(false);
                            del = nextDel[textNum];
                            if (textNum > 0) { text[textNum - 1].SetActive(false); }
                            if (text[textNum]) { text[textNum].SetActive(true); }
                            textNum++;
                            space[0].SetActive(false);
                            nextDel[10] = 100;
                            manager.recruits++;
                        }
                        else if (Mathf.Abs(mousePos.y - options[1].position.y) < .35f || Mathf.Abs(mousePos.y - options[2].position.y) < .3f)
                        //tests for if clicked on wrong answer, jumps to wrong answer text if true
                        {
                            textNum += 2; //skips the correct answer text
                            space[1].SetActive(false);
                            del = nextDel[textNum];
                            text[3].SetActive(false);
                            text[textNum].SetActive(true);
                            textNum++;
                            space[0].SetActive(false);
                            nextDel[11] = 60;
                        }
                    }
                }
            }
            if (del > 0) { del--; if (del == 1) { space[1].SetActive(true); } }
            if (spaceDown) { spaceDown = false; }
            if (click) { click = false; }
        }
        else
        {
            if (enter)
            {
                del = 0; space[0].SetActive(false); if (textNum > 0 && text[textNum - 1]) { text[textNum - 1].SetActive(false); }
                textNum = 0;
                space[1].SetActive(false);
                enter = false;
                nextDel[11] = 0;
            }
        }
        if (nextDel[10] > 1)
        {
            nextDel[10]--;
        }
        else
        {
            text[6].SetActive(false);
            if (Mathf.Abs(options[3].position.x - transform.position.x) > .2f)
                //determiens if the recruit is close enough to the portal to enter it
            {
                if (options[3].position.x > transform.position.x) //makes the recruit walk to the portal
                {
                    transform.position += new Vector3(0.1f, 0, 0);
                }
                else
                {
                    transform.position -= new Vector3(0.1f, 0, 0);
                }
            }
            else
            {
                Destroy(gameObject);
                //once the recruit is close enough to the portal, it destroys itself giving the impression it entered the portal
            }
        }
        if (nextDel[11]>0)
        {
            if (nextDel[11]==1) //goes back to the recruit's question if the player gets it wrong
            {
                text[6].SetActive(false);
                textNum = 4;
                text[3].SetActive(true); //shows the answer choices again
            }
            nextDel[11]--;
        }
        if (function==2&&manager.recruits==3)
            //when all three allies have been recruited, this npc moves to a new position to talk to player about final boss
        {
            transform.position = new Vector3(43.5f,-1.574f,0);
            manager.recruits = 0; //prevents these lines from running endlessly
        }
    }
}
