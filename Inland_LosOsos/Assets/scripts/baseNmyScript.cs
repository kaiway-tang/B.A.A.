using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baseNmyScript : MonoBehaviour
{
    public int function; //the type of enemy; 0:future minion; 1:business Boss 2:america minion 3: business/america boss
    public int maxHP;
    public float hp;
    public Transform hpBar;
    public Transform HpScaler; //the object that controls the visible hp bar
    public Transform hpScalerFX;
    public float hpFx;
    int every3;
    int[] atkDel;//delay before attack can be registered again (triggerStay is much more responsive than triggerEnter)
                 //0:bscAtk1 1:bscAtk2 2:bscAtk3 3:aegis

    public int disabled; //disables such as knockback that prevent normal enemy behavior
    public GameObject bullet; //bullet that will be created to injure boss upon dying if the script is used by future 
    int del;
    Transform playTrans;
    Rigidbody2D rb;
    public AudioSource speaker;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playTrans = manager.playTrans;
        atkDel = new int[5];
        hp = maxHP;
        if (function==1) { hpFx = business.hp; } else
        if (function == 3) { hpFx = hp; }
        if (function==1) { bullet.transform.parent = null; } //if business boss, this will prevent the hp line from reflecting with business boss's left and right movement
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (del > 0) { del--;
            Vector3 direction = transform.position - hpBar.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
            bullet.transform.localRotation = Quaternion.Slerp(transform.rotation, rotation, 100 * Time.deltaTime);
            bullet.transform.localScale = new Vector3(1, Vector3.Distance(transform.position, hpBar.position) / 5f, 1);
            bullet.transform.position = transform.position + new Vector3(0, 0.5f, 0);
            //this scales the health line to always be the distance between the business boss and the center hp bar and makes it rotate to face the hp bar
            if (del == 1) { bullet.SetActive(false); } } //makes the health line follow the business boss around while its visible then disables it after a brief delay
        atkTimers();
        if (hp<=0)
        {
            if (function == 0) { Instantiate(bullet, transform.position, transform.rotation); }
            if (function != 3) { Destroy(gameObject); }
        }
        if (disabled>0) {disabled--;}
        if (function==1)
        {
            if (hpFx > business.hp)
            {
                hpFx -= 2.5f;
                hpScalerFX.transform.localScale = new Vector3(hpFx / 800f, 1, 1);
            }
        }
        else if (function==3)
        {
            if (hpFx>hp)
            {
                hpFx-=2f;
                hpScalerFX.transform.localScale = new Vector3(hpFx / maxHP, 1, 1);
            }
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag=="bscAtk1"&&atkDel[0]<1) { hp -= player.baseDmg; takeDmg(true); atkDel[0]=25; }
        if (col.gameObject.tag == "bscAtk2" && atkDel[1] < 1) { hp -= player.baseDmg; takeDmg(true); atkDel[1] = 25; }
        if (col.gameObject.tag == "bscAtk3" && atkDel[2] < 1) { hp -= player.baseDmg*1.5f; takeDmg(true); atkDel[2] = 25; }
        if ((col.gameObject.tag == "aegis" && atkDel[3] < 1)&&Aegis.del>0) { hp -= player.baseDmg * .67f; takeDmg(false); atkDel[3] = 25; } //aegis only deals damage while dashing
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag=="arrowProj") { hp -= 15; takeDmg(false);Destroy(col.gameObject); }
    }
    void HpBar()
    {
        if (function != 1)
        {
            HpScaler.transform.localScale = new Vector3(hp / maxHP, 1, 1); //if not business boss, scales the visible health bar according to the remaining 
        } else
        {
            if (hp < 1000)
            {
                business.hp -= 1000 - hp;
                hp = 1000;
                HpScaler.transform.localScale = new Vector3(business.hp / 800f, 1, 1);
            }
        }
        //transfers dmg registered by this script to the public health variable shared by both business bosses
    }
    void atkTimers()
    {
        if (atkDel[0]>0) {atkDel[0]--; }
        if (atkDel[1] > 0) { atkDel[1]--; }
        if (atkDel[2] > 0) { atkDel[2]--; }
        if (atkDel[3] > 0) { atkDel[3]--; }
    }
    void takeDmg(bool fromPlayer) //determines if dmg source is player1 or aegis
    {
        if (speaker) { speaker.Play(); }
        if (function == 1) //if its business boss, this creates the green health line to show its siphoning hp from the hp shared by both bosses
        {
            Vector3 direction = transform.position - hpBar.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
            bullet.transform.localRotation = Quaternion.Slerp(transform.rotation, rotation, 100 * Time.deltaTime);
            bullet.transform.localScale = new Vector3(1, Vector3.Distance(transform.position, hpBar.position) / 5f, 1);
            bullet.transform.position = transform.position + new Vector3(0, 0.5f, 0);
            bullet.SetActive(true);
            del = 10;
        }
        HpBar();
        disabled = 10;
        knockback(fromPlayer);
    }
    void knockback(bool fromPlayer) //determines if dmg source is player1 or aegis
    {
        if (fromPlayer)
        {
            if (playTrans.position.x > transform.position.x)
            {
                rb.velocity = new Vector3(-10f, 0, 0);
            }
            else
            {
                rb.velocity = new Vector3(10f, 0, 0);
            }
        } else
        {
            if (Aegis.self.position.x > transform.position.x)
            {
                rb.velocity = new Vector3(-10f, 0, 0);
            }
            else
            {
                rb.velocity = new Vector3(10f, 0, 0);
            }
        }
    }
}
