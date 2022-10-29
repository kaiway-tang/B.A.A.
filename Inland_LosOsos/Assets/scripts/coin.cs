using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour
{
    public int atk; //this script controls all of business/leader boss projectiles; 0:coins 1:dollar 2:money bag
    int sine; //timer for wavy moevement pattern for dollar atk;
    bool destroy;
    // Start is called before the first frame update
    void Start()
    {
        if (atk == 0) { transform.Rotate(Vector3.forward * Random.Range(-20, 21)); } //makes the coin turn a random direction to create a shotgun styled attack
        if (atk==1) { sine = -15; }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (atk == 0) { transform.position += transform.right * .1f; }
        else
            if (atk == 1)
        {
            transform.position += transform.right * .2f;
            transform.Rotate(Vector3.forward*(sine));
            if (sine>0) { sine++;if (sine == 20) { sine = -1; } }
            if (sine<0) { sine--; if (sine == -20) { sine = 1; } } //make the dollar oscillate paths (the curve-y zig zag motion)
        }
        if (atk==2) { transform.position += transform.right * .06f;
            Vector3 direction = transform.position - manager.playTrans.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle+180, Vector3.forward);
            transform.localRotation = Quaternion.Slerp(transform.rotation, rotation, 1 * Time.deltaTime);
            //rotate the money bag homing projectile to face the player
        }
        if (destroy) { Destroy(gameObject); }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "hitbox")
        {
            destroy = true;
            //tests if the projectile hit the player; if so, destroys the projectile after a .02 sec delay
            //to ensure that the damage has time to register (the delay too short to be noticeable)
        }
    }
}
