using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heal : MonoBehaviour
{
    public hitbox hitbox;
    public GameObject heartFX;
    public GameObject healFX;
    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(0,0,0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += transform.up*-.08f;
        Vector2 direction = transform.position - manager.playTrans.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1 * Time.deltaTime); //makes the hat's bottom face the player
        
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag=="hitbox")
        {
            GameObject HealFX= Instantiate(healFX, hitbox.hearts[hitbox.player.hp].transform.position, transform.rotation);
            HealFX.transform.parent = manager.camTrans;
            hitbox.hearts[hitbox.player.hp].SetActive(true);
            hitbox.player.hp++;
            Instantiate(heartFX, transform.position, heartFX.transform.rotation);
            Destroy(gameObject);
        }
    }
}
