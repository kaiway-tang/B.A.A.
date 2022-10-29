using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blink : MonoBehaviour
{
    public SpriteRenderer sprRend;
    public Color[] shades;
    public int colors;
    public int rate;
    int color;
    int del;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        del++;
        if (del>=rate) { sprRend.color = shades[color];color++;if (color >= colors) { color = 0; }del = 0; }
    }
}
