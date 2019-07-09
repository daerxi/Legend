using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour 
{
    Vector3 velocity;
    public float speed;
    int frame;
    public int distance;
    public int hp;
    public int damage;
    public int max_hp;
    public float timeStop;
    public GameObject hpBar;

    public float moveD;
    public bool faceLeft = true;
    private Animator anim;
    public bool isAttack;
    public bool isWalk;
    public bool isIdle;
    public float timeLeft;
    public float maxTime;
    public GameObject map;
    // Start is called before the first frame update
    void Start()
    {
        velocity = new Vector3(speed, 0, 0);
        frame = 0;
        hp = max_hp;
        timeLeft = maxTime;
        anim = this.gameObject.GetComponent<Animator>();
        isWalk = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isIdle)
        {
            timeLeft -= Time.deltaTime;
            anim.Play("Boss_Idle");
            if (timeLeft <= 0)
            {
                isWalk = true;
                isIdle = false;
                timeLeft = maxTime;
            }
        }

        if (isAttack)
        {
            timeLeft -= Time.deltaTime;
            anim.Play("Boss_Attack");
            if (timeLeft <= 0)
            {
                isIdle = true;
                isAttack = false;
                timeLeft = maxTime - 3;
            }
        }
        if (isWalk)
        { 
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
            {
                isAttack = true;
                isWalk = false;
                timeLeft = maxTime - 3;
            }
            if (frame == 0)
            {
                flip();
            }
            if (frame < distance)
            {
                transform.position += velocity;
                frame++;
            }
            if (frame >= distance && frame <= 2 * distance)
            {
                if (frame == distance)
                {
                    flip();
                }
                frame++;
                transform.position -= velocity;
            }
            if (frame >= 2 * distance)
            {
                frame = 0;
            }
            anim.Play("Walk");
        }
        if (hp <= 0)
        {
            map.SetActive(true);
            Destroy(this.gameObject);
        }
        float hpPer = ((float)hp / (float)max_hp);
        hpBar.transform.localScale = new Vector3(hpPer, hpBar.transform.localScale.y, hpBar.transform.localScale.z);
    }
   
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "sword")
        {
            hp--;
            timeStop = 2;
            if (timeStop >= 0)
            {
                speed = 0;
                timeStop -= Time.deltaTime;
            }
        }
    }

    void flip()
    {
        faceLeft = !faceLeft;
        Vector2 dir = transform.localScale;
        dir.x *= -1;
        transform.localScale = dir;
    }
}
