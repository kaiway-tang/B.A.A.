using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fade : MonoBehaviour
{
    public Color fadeColor;
    SpriteRenderer sprRend;
    // Start is called before the first frame update
    void Start()
    {
        sprRend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        fadeColor.a -= 0.01f;
        sprRend.color = fadeColor;
        if (fadeColor.a <= 0) { Destroy(gameObject); }
    }
}
