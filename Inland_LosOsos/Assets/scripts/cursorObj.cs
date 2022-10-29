using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cursorObj : MonoBehaviour
{
    public bool pinScene;
    public Color[] color;
    int left;
    public Transform blackClouds;
    public int start;
    public Transform startText;
    public GameObject loading;

    void Start()
    {
        
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) //tests for if left mouse button clicked
        {
            left = 2;
        }
    }

    
    void FixedUpdate()
    {
        if (start>0)
        {
            if (start < 1750)
            {
                if (start==2) { loading.SetActive(true); }
                if (start == 1) {
                    SceneManager.LoadScene("Start"); }
                startText.transform.position += new Vector3(0, 0.013f, 0);
            }
            else
            {
                blackClouds.transform.position += new Vector3(-.7f, 0, 0);
            }
            start--;
        }
        transform.position= Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);
        //moves the cursor object to the mouse position
        if (left>0) { left--; }
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        if (left > 0) //tests for if left click
        {
            if (token.correct == 0)
            {
                if (col.gameObject.name == "correct")
                {
                    token.correct = 1;
                }
                else if (col.gameObject.name == "incorrect")
                {
                    token.correct = 2;
                }
            }
        }
        if (start == 0)
        {
            if (col.gameObject.name == "start")
            {
                col.gameObject.GetComponent<SpriteRenderer>().color = color[0];
                if (left > 0)
                {
                    start = 1830;
                }
            }
            if (col.gameObject.name == "quit")
            {
                col.gameObject.GetComponent<SpriteRenderer>().color = color[0];
                if (left > 0)
                {
                    Application.Quit();
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (!pinScene) { col.gameObject.GetComponent<SpriteRenderer>().color = color[1]; }
    }
}
